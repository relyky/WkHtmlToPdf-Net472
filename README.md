# WkHtmlToPdf-Net472 [![NuGet Version](http://img.shields.io/nuget/v/WkHtmlToPdf-Net472.svg?style=flat)](https://www.nuget.org/packages/WkHtmlToPdf-Net472/)
   
To wrapper [wkhtmltopdf](https://wkhtmltopdf.org/) library to convert HTML pages to PDF. 
Has been successfully tested on Windows/NET Framework 4.7.2/WebForm website project only.

### !Important
All the code is not changed, just to re-compile in NET Framework 4.7.2. 
And the target application is the WebForm website project. 
For the syntax and instructions manual to development, please refer to the source branch: [Haukcode.WkHtmlToPdfDotNet](https://github.com/HakanL/WkHtmlToPdf-DotNet).

### Install 

Library can be installed through Nuget. Run command below from the package manager console:

```
PM> Install-Package WkHtmlToPdf-Net472
```

### Fork
This library is forked from [Haukcode.WkHtmlToPdfDotNet v1.5.93](https://www.nuget.org/packages/Haukcode.WkHtmlToPdfDotNet/). 
The main modification is to allow execution on the .NET Framework 4.7.2 platform.

### Basic converter
Use this converter in single threaded applications.

Create converter:
```csharp
var converter = new BasicConverter(new PdfTools());
```

### Synchronized converter
Use this converter in multi threaded applications and web servers. Conversion tasks are saved to blocking collection and executed on a single thread.

```csharp
var converter = new SynchronizedConverter(new PdfTools());
```

### Define document to convert
```csharp
var doc = new HtmlToPdfDocument()
{
    GlobalSettings = {
        ColorMode = ColorMode.Color,
        Orientation = Orientation.Landscape,
        PaperSize = PaperKind.A4Plus,
    },
    Objects = {
        new ObjectSettings() {
            PagesCount = true,
            HtmlContent = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. In consectetur mauris eget ultrices  iaculis. Ut                               odio viverra, molestie lectus nec, venenatis turpis.",
            WebSettings = { DefaultEncoding = "utf-8" },
            HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
        }
    }
};

```

### Convert
If Out property is empty string (defined in GlobalSettings) result is saved in byte array. 
```csharp
byte[] pdf = converter.Convert(doc);
```

If Out property is defined in document then file is saved to disk:
```csharp
var doc = new HtmlToPdfDocument()
{
    GlobalSettings = {
        ColorMode = ColorMode.Color,
        Orientation = Orientation.Portrait,
        PaperSize = PaperKind.A4,
        Margins = new MarginSettings() { Top = 10 },
        Out = @"C:\WkHtmlToPdf-DotNet\src\TestThreadSafe\test.pdf",
    },
    Objects = {
        new ObjectSettings()
        {
            Page = "http://google.com/",
        },
    }
};
```
```csharp
converter.Convert(doc);
```


#### Note
For any other linux distro choose the correct package from the [wkhtmltopdf releases](https://github.com/wkhtmltopdf/wkhtmltopdf/releases) 

### Recommendations
Do not use wkhtmltopdf with any untrusted HTML – be sure to sanitize any user-supplied HTML/JS, otherwise it can lead to complete takeover of the server it is running on!
