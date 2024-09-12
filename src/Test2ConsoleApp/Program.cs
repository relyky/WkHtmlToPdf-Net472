using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace WkHtmlToPdfDotNet.Test2ConsoleApp
{
  class Program
  {
    static void Main(string[] args)
    {
      //var converter = new BasicConverter(new PdfTools());
      var converter = new SynchronizedConverter(new PdfTools());

      converter.PhaseChanged += Converter_PhaseChanged;
      converter.ProgressChanged += Converter_ProgressChanged;
      converter.Finished += Converter_Finished;
      converter.Warning += Converter_Warning;
      converter.Error += Converter_Error;

      var doc = new HtmlToPdfDocument()
      {
        GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Landscape,
                    PaperSize = PaperKind.A4,
                },
        Objects = {

                    new CoverSettings()
                    {
                        HtmlContent = @"

<!DOCTYPE html>

<html>
<head>
  <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8""/>
  <title>Spoon-Knife</title>
  <LINK href=""styles.css"" rel=""stylesheet"" type=""text/css"">
</head>

<body>

<h1>此頁是封面</h1>
<img src=""forkit.gif"" id=""octocat"" alt="""" />

<!-- Feel free to change this text here -->
<p>
  Fork me? Fork you, @octocat!<br/>
  Sean made a change
</p>

</body>
</html>

"
                    },

                    new ObjectSettings() {
                        PagesCount = true,
                        HtmlContent = @"

<!DOCTYPE html>

<html>
<head>
  <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8""/>
  <title>Spoon-Knife</title>
  <LINK href=""styles.css"" rel=""stylesheet"" type=""text/css"">
</head>

<body>

<img src=""forkit.gif"" id=""octocat"" alt="""" />

<!-- Feel free to change this text here -->
<h1>This is html content, oh YES.</h1>
<p>
  我出運了
</p>

</body>
</html>

",
                        WebSettings = { DefaultEncoding = "utf-8" },
                        HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                        FooterSettings = { FontSize = 9, Right = "Page [page] of [toPage]" }
                    }
                }
      };

      byte[] pdf = converter.Convert(doc);

      if (!Directory.Exists("Files"))
      {
        Directory.CreateDirectory("Files");
      }

      using (var stream = new FileStream(Path.Combine("Files", DateTime.UtcNow.Ticks.ToString() + ".pdf"), FileMode.Create))
      {
        stream.Write(pdf, 0, pdf.Length);
      }
    }

    private static void Converter_Error(object sender, EventDefinitions.ErrorArgs e)
    {
      Console.WriteLine("[ERROR] {0}", e.Message);
    }

    private static void Converter_Warning(object sender, EventDefinitions.WarningArgs e)
    {
      Console.WriteLine("[WARN] {0}", e.Message);
    }

    private static void Converter_Finished(object sender, EventDefinitions.FinishedArgs e)
    {
      Console.WriteLine("Conversion {0} ", e.Success ? "successful" : "unsucessful");
    }

    private static void Converter_ProgressChanged(object sender, EventDefinitions.ProgressChangedArgs e)
    {
      Console.WriteLine("Progress changed {0}", e.Description);
    }

    private static void Converter_PhaseChanged(object sender, EventDefinitions.PhaseChangedArgs e)
    {
      Console.WriteLine("Phase changed {0} - {1}", e.CurrentPhase, e.Description);
    }
  }
}
