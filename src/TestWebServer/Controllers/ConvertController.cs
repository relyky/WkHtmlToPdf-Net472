using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WkHtmlToPdfDotNet.Contracts;
using System.IO;

namespace WkHtmlToPdfDotNet.TestWebServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConvertController : ControllerBase
    {
        private readonly IConverter converter;

        public ConvertController(IConverter converter)
        {
            this.converter = converter;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    PaperSize = PaperKind.A3,
                    Orientation = Orientation.Landscape,
                },

                Objects = {
                    new ObjectSettings()
                    {
                        Page = "http://google.com/",
                    },
                     new ObjectSettings()
                    {
                        Page = "https://github.com/",

                    }
                }
            };

            byte[] pdf = this.converter.Convert(doc);

            return File(pdf, "application/pdf", "Test.pdf");
        }
    }
}
