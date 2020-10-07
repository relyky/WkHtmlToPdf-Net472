using WkHtmlToPdfDotNet.Contracts;
using System;
using System.Threading;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WkHtmlToPdfDotNet
{
    public class SynchronizedConverter : BasicConverter
    {
        private Thread conversionThread;
        private readonly BlockingCollection<Task> conversions = new BlockingCollection<Task>();
        private bool kill = false;
        private readonly object startLock = new object();

        public SynchronizedConverter(ITools tools) : base(tools)
        {
        }

        public override byte[] Convert(IDocument document)
        {
            return Invoke(() => base.Convert(document));
        }

        public TResult Invoke<TResult>(Func<TResult> @delegate)
        {
            StartThread();

            var task = new Task<TResult>(@delegate);

            lock (task)
            {
                //add task to blocking collection
                this.conversions.Add(task);

                //wait for task to be processed by conversion thread 
                Monitor.Wait(task);
            }

            //throw exception that happened during conversion
            if (task.Exception != null)
            {
                throw task.Exception;
            }

            return task.Result;
        }

        private void StartThread()
        {
            lock (this.startLock)
            {
                if (this.conversionThread == null)
                {
                    this.conversionThread = new Thread(Run)
                    {
                        IsBackground = true,
                        Name = "wkhtmltopdf worker thread"
                    };

                    this.kill = false;

                    this.conversionThread.Start();
                }
            }
        }

        private void StopThread()
        {
            lock (this.startLock)
            {
                if (this.conversionThread != null)
                {
                    this.kill = true;

                    while (this.conversionThread.ThreadState != ThreadState.Stopped)
                    {
                        Thread.Sleep(1);
                    }

                    this.conversionThread = null;
                }
            }
        }

        private void Run()
        {
            while (!this.kill)
            {
                //get next conversion taks from blocking collection
                Task task = this.conversions.Take();

                lock (task)
                {
                    //run taks on thread that called RunSynchronously method
                    task.RunSynchronously();

                    //notify caller thread that task is completed
                    Monitor.Pulse(task);
                }
            }
        }
    }
}
