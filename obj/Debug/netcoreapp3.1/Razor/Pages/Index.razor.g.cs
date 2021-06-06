#pragma checksum "C:\Users\ivan1\Source\Repos\Add-Blazor-PDF-Viewer-to-a-Blazor-Server-Application\Pages\Index.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ba6cccb3b7cb0c10fd1dfb1e92725a8ec1d7202f"
// <auto-generated/>
#pragma warning disable 1591
namespace MyBlazorServerApp.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\ivan1\Source\Repos\Add-Blazor-PDF-Viewer-to-a-Blazor-Server-Application\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\ivan1\Source\Repos\Add-Blazor-PDF-Viewer-to-a-Blazor-Server-Application\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\ivan1\Source\Repos\Add-Blazor-PDF-Viewer-to-a-Blazor-Server-Application\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\ivan1\Source\Repos\Add-Blazor-PDF-Viewer-to-a-Blazor-Server-Application\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\ivan1\Source\Repos\Add-Blazor-PDF-Viewer-to-a-Blazor-Server-Application\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\ivan1\Source\Repos\Add-Blazor-PDF-Viewer-to-a-Blazor-Server-Application\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\ivan1\Source\Repos\Add-Blazor-PDF-Viewer-to-a-Blazor-Server-Application\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\ivan1\Source\Repos\Add-Blazor-PDF-Viewer-to-a-Blazor-Server-Application\_Imports.razor"
using MyBlazorServerApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\ivan1\Source\Repos\Add-Blazor-PDF-Viewer-to-a-Blazor-Server-Application\_Imports.razor"
using MyBlazorServerApp.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\ivan1\Source\Repos\Add-Blazor-PDF-Viewer-to-a-Blazor-Server-Application\Pages\Index.razor"
using Syncfusion.Blazor.PdfViewerServer;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\ivan1\Source\Repos\Add-Blazor-PDF-Viewer-to-a-Blazor-Server-Application\Pages\Index.razor"
using Syncfusion.Blazor.Buttons;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\ivan1\Source\Repos\Add-Blazor-PDF-Viewer-to-a-Blazor-Server-Application\Pages\Index.razor"
using Microsoft.Azure.Storage;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\ivan1\Source\Repos\Add-Blazor-PDF-Viewer-to-a-Blazor-Server-Application\Pages\Index.razor"
using Microsoft.Azure.Storage.Blob;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\ivan1\Source\Repos\Add-Blazor-PDF-Viewer-to-a-Blazor-Server-Application\Pages\Index.razor"
using System.IO;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/")]
    public partial class Index : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenComponent<Syncfusion.Blazor.PdfViewerServer.SfPdfViewerServer>(0);
            __builder.AddAttribute(1, "DocumentPath", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 10 "C:\Users\ivan1\Source\Repos\Add-Blazor-PDF-Viewer-to-a-Blazor-Server-Application\Pages\Index.razor"
                                  Path

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(2, "Height", "700px");
            __builder.AddAttribute(3, "Width", "1060px");
            __builder.AddComponentReferenceCapture(4, (__value) => {
#nullable restore
#line 10 "C:\Users\ivan1\Source\Repos\Add-Blazor-PDF-Viewer-to-a-Blazor-Server-Application\Pages\Index.razor"
                                              PdfViewer = (Syncfusion.Blazor.PdfViewerServer.SfPdfViewerServer)__value;

#line default
#line hidden
#nullable disable
            }
            );
            __builder.CloseComponent();
        }
        #pragma warning restore 1998
#nullable restore
#line 12 "C:\Users\ivan1\Source\Repos\Add-Blazor-PDF-Viewer-to-a-Blazor-Server-Application\Pages\Index.razor"
      

    SfPdfViewerServer PdfViewer;
    public string Path { get; set; } = @"D:\Samples\PDF_Succinctly.pdf";
    string connectionString = Connections.ConnectionString;
    string blobContainerName = "document";
    string pdfFileName = "PDF_Succinctly.pdf";

    public void OnOpen()
    {
        using (MemoryStream memoryStream = new MemoryStream())
        {
            CloudStorageAccount.Parse(connectionString)
            .CreateCloudBlobClient()
            .GetContainerReference(blobContainerName)
            .GetBlockBlobReference(pdfFileName)
            .DownloadToStream(memoryStream);

            Path = "data:application/pdf;base64," + Convert.ToBase64String(memoryStream.ToArray());
        }
    }

    public async void OnSave()
    {
        object base64Data = await PdfViewer.SaveAsBlob();
        Dictionary<string, string> documentContent = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(base64Data.ToString());
        byte[] data = Convert.FromBase64String(documentContent["data"]);
        using (MemoryStream memoryStream = new MemoryStream(data))
        {
            CloudStorageAccount.Parse(connectionString)
            .CreateCloudBlobClient()
            .GetContainerReference(blobContainerName)
            .GetBlockBlobReference(pdfFileName)
            .UploadFromStream(memoryStream);
        }
    }

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591