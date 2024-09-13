using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;

public partial class TestHtmlToPdf2 : System.Web.UI.Page
{
  protected bool f_loading;

  /// <summary>
  /// Let IConverter instance be singleton.
  /// </summary>
  protected IConverter PdfConverter
  {
    get
    {
      if (Application["PdfConverter"] == null)
        Application["PdfConverter"] = new SynchronizedConverter(new PdfTools());

      return (IConverter)Application["PdfConverter"];
    }
  }

  protected void Page_Load(object sender, EventArgs e)
  {
    if(this.IsPostBack)
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
      string filename = $"LandscapeSample_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.pdf";
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
    return @"
<html>
<head>
</head>
<body>
  <h1>測試橫印報表 H1</h1>
  <h2>測試橫印報表 H2</h2>
  <h3>測試橫印報表 H3</h3>
  <h4>測試橫印報表 H4</h4>
  <h5>測試橫印報表 H5</h5>
  <h6>測試橫印報表 H6</h6>
  <p>測試橫印報表 paragraph</p>
</body>
</html>
";
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
        Orientation = WkHtmlToPdfDotNet.Orientation.Landscape,
        PaperSize = PaperKind.A4,
        Margins = new MarginSettings() { Top = 10, Left = 10 },
      },
      Objects = {
        new CoverSettings()
        {
          HtmlContent = @"
<!DOCTYPE html>
<html>
<head>
  <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
  <title>This is conver</title>
  <style>
    body { font-family: '微軟正黑體'; }
    .center { text-align: center; }
  </style>
</head>
<body class='center'>  
  <h1>此頁是封面</h1>
  <h1>This is cover</h1>
  <hr style='margin-top:2mm;margin-bottom:2mm;'>
  <img src='https://www.w3schools.com/html/pic_trulli.jpg' width='500' height='333'>
  <hr style='margin-top:2mm;margin-bottom:2mm;'>
</body>
</html>
"
        },
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
    byte[] fileBlob = PdfConverter.Convert(doc);
    return fileBlob;

    //using (var converter = new BasicConverter(new PdfTools()))
    //{
    //  byte[] fileBlob = converter.Convert(doc);
    //  return fileBlob;
    //}
    //※ 當執行多次會不正常！推論是資源沒釋放乾淨？
  }
}