using WkHtmlToPdfDotNet.Contracts;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace WkHtmlToPdfDotNet
{
    public sealed class PdfTools : ITools
    {
        private IWkHtmlModule module;

        public bool IsLoaded { get; private set; }

        //used to maintain a reference to delegates to prevent them being garbage collected...
        private List<object> delegates = new List<object>();

        public PdfTools()
        {
            IsLoaded = false;
        }

        public void Load()
        {
            if (IsLoaded)
            {
                return;
            }

            this.module = new WkHtmlModule();

            if (this.module.wkhtmltopdf_init(0) == 1)
            {
                IsLoaded = true;
            }
        }

        public bool ExtendedQt()
        {
            return this.module.wkhtmltopdf_extended_qt() == 1 ? true : false;
        }

        public string GetLibraryVersion()
        {
            return Marshal.PtrToStringAnsi(this.module.wkhtmltopdf_version());
        }

        public IntPtr CreateGlobalSettings()
        {
            return this.module.wkhtmltopdf_create_global_settings();
        }

        public int SetGlobalSetting(IntPtr settings, string name, string value)
        {
            return this.module.wkhtmltopdf_set_global_setting(settings, name, value);
        }

        public string GetGlobalSetting(IntPtr settings, string name)
        {
            //default const char * size is 2048 bytes 
            byte[] buffer = new byte[2048];

            this.module.wkhtmltopdf_get_global_setting(settings, name, buffer);

            return GetString(buffer);
        }

        public void DestroyGlobalSetting(IntPtr settings)
        {
            this.module.wkhtmltopdf_destroy_global_settings(settings);
        }

        public IntPtr CreateObjectSettings()
        {
            return this.module.wkhtmltopdf_create_object_settings();
        }

        public int SetObjectSetting(IntPtr settings, string name, string value)
        {
            return this.module.wkhtmltopdf_set_object_setting(settings, name, value);
        }

        public string GetObjectSetting(IntPtr settings, string name)
        {
            //default const char * size is 2048 bytes 
            byte[] buffer = new byte[2048];

            this.module.wkhtmltopdf_get_object_setting(settings, name, buffer);

            return GetString(buffer);
        }

        public void DestroyObjectSetting(IntPtr settings)
        {
            this.module.wkhtmltopdf_destroy_object_settings(settings);
        }

        public IntPtr CreateConverter(IntPtr globalSettings)
        {
            return this.module.wkhtmltopdf_create_converter(globalSettings);
        }

        public void AddObject(IntPtr converter, IntPtr objectSettings, byte[] data)
        {
            this.module.wkhtmltopdf_add_object(converter, objectSettings, data);
        }

        public void AddObject(IntPtr converter, IntPtr objectSettings, string data)
        {
            this.module.wkhtmltopdf_add_object(converter, objectSettings, data);
        }

        public bool DoConversion(IntPtr converter)
        {
            return this.module.wkhtmltopdf_convert(converter);
        }

        public void DestroyConverter(IntPtr converter)
        {
            this.module.wkhtmltopdf_destroy_converter(converter);
        }

        public byte[] GetConversionResult(IntPtr converter)
        {
            IntPtr resultPointer;

            int length = this.module.wkhtmltopdf_get_output(converter, out resultPointer);
            var result = new byte[length];
            Marshal.Copy(resultPointer, result, 0, length);

            return result;
        }

        public int SetPhaseChangedCallback(IntPtr converter, VoidCallback callback)
        {
            this.delegates.Add(callback);

            return this.module.wkhtmltopdf_set_phase_changed_callback(converter, callback);
        }

        public int SetProgressChangedCallback(IntPtr converter, VoidCallback callback)
        {
            this.delegates.Add(callback);

            return this.module.wkhtmltopdf_set_progress_changed_callback(converter, callback);
        }

        public int SetFinishedCallback(IntPtr converter, IntCallback callback)
        {
            this.delegates.Add(callback);

            return this.module.wkhtmltopdf_set_finished_callback(converter, callback);
        }

        public int SetWarningCallback(IntPtr converter, StringCallback callback)
        {
            this.delegates.Add(callback);

            return this.module.wkhtmltopdf_set_warning_callback(converter, callback);
        }

        public int SetErrorCallback(IntPtr converter, StringCallback callback)
        {
            this.delegates.Add(callback);

            return this.module.wkhtmltopdf_set_error_callback(converter, callback);
        }

        public int GetPhaseCount(IntPtr converter)
        {
            return this.module.wkhtmltopdf_phase_count(converter);
        }

        public int GetCurrentPhase(IntPtr converter)
        {
            return this.module.wkhtmltopdf_current_phase(converter);
        }

        public string GetPhaseDescription(IntPtr converter, int phase)
        {
            return Marshal.PtrToStringAnsi(this.module.wkhtmltopdf_phase_description(converter, phase));
        }

        public string GetProgressString(IntPtr converter)
        {
            return Marshal.PtrToStringAnsi(this.module.wkhtmltopdf_progress_string(converter));
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                //free unmanaged resources (unmanaged objects) and override a finalizer below.
                this.module?.wkhtmltopdf_deinit();
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~PdfTools()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }

        #endregion

        private string GetString(byte[] buffer)
        {
            string value = "";

            int walk = 0;

            while (walk < buffer.Length && buffer[walk] != 0)
            {
                walk++;
            }

            value = Encoding.UTF8.GetString(buffer, 0, walk);

            return value;
        }
    }
}
