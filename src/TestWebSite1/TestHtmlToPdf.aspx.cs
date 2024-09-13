using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;

public partial class TestHtmlToPdf : System.Web.UI.Page
{
  protected bool f_loading;

  protected IConverter PdfConverter
  {
    get
    {
      if (Application["PdfConverter"] == null)
      {
        Application["PdfConverter"] = new SynchronizedConverter(new PdfTools());
      }

      return (IConverter)Application["PdfConverter"];
    }
  }

  protected void Page_Load(object sender, EventArgs e)
  {
    if (this.IsPostBack)
    {

    }
  }

  protected void Button1_Click(object sender, EventArgs e)
  {
    try
    {
      f_loading = true;
      Label1.Text = "開始";

      //# 產生 PDF 報表檔
      byte[] fileBlob = MakePdfReport();

      //# 下載檔案--存入暫存檔
      string filename = $"ReportSample_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.pdf";
      string filePath = Path.Combine(@"D:\Temp", filename);
      File.WriteAllBytes(filePath, fileBlob);

      Label1.Text = "成功了。PDF 已存入 => " + filePath;
    }
    catch (Exception ex)
    {
      Label1.Text = "出現例外！" + ex.Message;
    }
    finally
    {
      f_loading = false;
    }
  }

  protected byte[] MakePdfReport()
  {
    //# make html with data.
    string html = DoMakeHtmlPage(/* data model */);

    //# Convert
    byte[] pdfBlob = HtmlToPdf(html);
    return pdfBlob;
  }

  protected string DoMakeHtmlPage(/* data model */)
  {
    string htmlTpl = File.ReadAllText(Server.MapPath("~/Template/ReportSampleTpl.html"));
    return htmlTpl;

    //    return @"
    //<html>
    //<head>
    //</head>
    //<body>
    //  <h1>我是報表</h1>
    //</body>
    //</html>
    //";
  }

  /// <summary>
  /// A4彩色直印
  /// </summary>
  public byte[] HtmlToPdf(string html)
  {
    //# Define document to convert
    var doc = new HtmlToPdfDocument()
    {
      GlobalSettings = {
        ColorMode = ColorMode.Color,
        Orientation = WkHtmlToPdfDotNet.Orientation.Portrait,
        PaperSize = PaperKind.A4,
        Margins = new MarginSettings() { Top = 10, Left = 10 },
      },
      Objects = {
        new ObjectSettings() {
          PagesCount = true,
          HtmlContent = html,
          WebSettings = { DefaultEncoding = "utf-8", PrintMediaType = true },
          LoadSettings = new LoadSettings { ZoomFactor = 1.26 }, //1.26
          HeaderSettings = {
            FontSize = 9,
            Right = "Date: [date]",
            Line = false,
            Spacing = 2.812,
            //HtmlUrl = "https://localhost:7248/reportResource/cpaheader.html" // 必需是完整的URL
          },
          FooterSettings = {
            FontSize = 9,
            Right = "Page [page] of [toPage]",
            Line = true,
            Spacing = 2.812,
            HtmlUrl = ""
          },
        }
      }
    };

    //# Convert
    //byte[] fileBlob = PdfConverter.Convert(doc);
    //return fileBlob;

    using (var converter = new BasicConverter(new PdfTools()))
    {
      byte[] fileBlob = converter.Convert(doc);
      return fileBlob;
    }
  }

}