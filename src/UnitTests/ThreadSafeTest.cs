using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WkHtmlToPdfDotNet.Contracts;

namespace WkHtmlToPdfDotNet.UnitTests
{
    [TestClass]
    public class ThreadSafeTest
    {
        private static IConverter Converter;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Converter = new SynchronizedConverter(new PdfTools());
        }

        [TestMethod]
        public void ConvertToPdf()
        {
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Grayscale,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings() { Top = 10 },
                },
                Objects = {
                    new ObjectSettings() {
                        Page = "http://www.color-hex.com/"
                    }
                }
            };

            var task1 = Task.Run(() => Action(doc));

            var doc2 = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    PaperSize = PaperKind.A4Small
                },

                Objects = {
                    new ObjectSettings()
                    {
                        Page = "http://google.com/",
                    }
                }
            };


            var task2 = Task.Run(() => Action(doc2));

            Task.WhenAll(task1, task2);
        }

        private void Action(HtmlToPdfDocument doc)
        {
            byte[] pdf = Converter.Convert(doc);

            if (!Directory.Exists("Files"))
            {
                Directory.CreateDirectory("Files");
            }

            using (var stream = new FileStream(Path.Combine("Files", DateTime.UtcNow.Ticks.ToString() + ".pdf"), FileMode.Create))
            {
                stream.Write(pdf, 0, pdf.Length);
            }
        }
    }
}
