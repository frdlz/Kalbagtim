#pragma checksum "C:\Users\XCV\Documents\GitHub\Kalbagtim\Views\Vaksins\Gea.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "87dd5e11e4b1aecc067aa9902ea82dd0dd0dfe80"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Vaksins_Gea), @"mvc.1.0.view", @"/Views/Vaksins/Gea.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\XCV\Documents\GitHub\Kalbagtim\Views\_ViewImports.cshtml"
using ProjectAlpha;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\XCV\Documents\GitHub\Kalbagtim\Views\_ViewImports.cshtml"
using ProjectAlpha.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\XCV\Documents\GitHub\Kalbagtim\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"87dd5e11e4b1aecc067aa9902ea82dd0dd0dfe80", @"/Views/Vaksins/Gea.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4fd8b69abb645371dabb85351d82e75fe9f32d8a", @"/Views/_ViewImports.cshtml")]
    public class Views_Vaksins_Gea : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ProjectAlpha.Models.Side.VaksinViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<div class=""container-fluid shadow-sm p-3 mb-5 bg-transparent rounded"">
<div class=""col-md-12 mt-2"">

    <div class=""card-deck"">
              <div class=""card bgtheme border-0 shadow-lightblue rounded"">
                <div class=""card-body text-center"">
                  <p class=""card-text text-left"">Total Pegawai</p>
                  <p class=""card-text float-left"" style=""font-size:30px"">");
#nullable restore
#line 10 "C:\Users\XCV\Documents\GitHub\Kalbagtim\Views\Vaksins\Gea.cshtml"
                                                                    Write(Model.TotalPegawaai.ToString("N3"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>
                  <p class=""card-text text-right"" style=""font-size:20px; color:green""><i class=""fas fa-arrow-up"" style=""color:green""></i> 20%</p>
                </div>
              </div>
             <div class=""card bgtheme border-0 shadow-accent-fuchsia rounded"">
                <div class=""card-body text-center"">
                  <p class=""card-text text-left"">Vaksin 1</p>
                  <p class=""card-text float-left"" style=""font-size:30px"">");
#nullable restore
#line 17 "C:\Users\XCV\Documents\GitHub\Kalbagtim\Views\Vaksins\Gea.cshtml"
                                                                    Write(Model.TotalVaksin1.ToString("#"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>
                </div>
              </div>
            <div class=""card bgtheme border-0 shadow-accent-fuchsia rounded"">
                <div class=""card-body text-center"">
                  <p class=""card-text text-left"">Vaksin 2</p>
                  <p class=""card-text float-left"" style=""font-size:30px"">");
#nullable restore
#line 23 "C:\Users\XCV\Documents\GitHub\Kalbagtim\Views\Vaksins\Gea.cshtml"
                                                                    Write(Model.TotalVaksin2.ToString("N3"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>
                </div>
              </div>
              <div class=""card bgtheme border-0 shadow-accent-fuchsia rounded"">
                <div class=""card-body text-center"">
                  <p class=""card-text text-left"">Vaksin 3</p>
                  <p class=""card-text float-left"" style=""font-size:30px"">");
#nullable restore
#line 29 "C:\Users\XCV\Documents\GitHub\Kalbagtim\Views\Vaksins\Gea.cshtml"
                                                                    Write(Model.TotalVaksin3.ToString("N3"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>
                </div>
              </div>
            </div>
</div>
<div class=""col-md-12 mt-2"">

    <div class=""card-deck"">
              <div class=""card bgtheme border-0 shadow-lightblue rounded"">
                <div class=""card-body text-center"">
                  <p class=""card-text text-left"">Total Pegawai</p>
                  <p class=""card-text float-left"" style=""font-size:30px"">");
#nullable restore
#line 40 "C:\Users\XCV\Documents\GitHub\Kalbagtim\Views\Vaksins\Gea.cshtml"
                                                                    Write(Model.TotalPegawaai2.ToString("N3"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>
                  <p class=""card-text text-right"" style=""font-size:20px; color:green""><i class=""fas fa-arrow-up"" style=""color:green""></i> 20%</p>
                </div>
              </div>
             <div class=""card bgtheme border-0 shadow-accent-fuchsia rounded"">
                <div class=""card-body text-center"">
                  <p class=""card-text text-left"">Vaksin 1</p>
                  <p class=""card-text float-left"" style=""font-size:30px"">");
#nullable restore
#line 47 "C:\Users\XCV\Documents\GitHub\Kalbagtim\Views\Vaksins\Gea.cshtml"
                                                                    Write(Model.TotalVaksin1Non.ToString("#"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>
                </div>
              </div>
            <div class=""card bgtheme border-0 shadow-accent-fuchsia rounded"">
                <div class=""card-body text-center"">
                  <p class=""card-text text-left"">Vaksin 2</p>
                  <p class=""card-text float-left"" style=""font-size:30px"">");
#nullable restore
#line 53 "C:\Users\XCV\Documents\GitHub\Kalbagtim\Views\Vaksins\Gea.cshtml"
                                                                    Write(Model.TotalVaksin2Non.ToString("N3"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>
                </div>
              </div>
              <div class=""card bgtheme border-0 shadow-accent-fuchsia rounded"">
                <div class=""card-body text-center"">
                  <p class=""card-text text-left"">Vaksin 3</p>
                  <p class=""card-text float-left"" style=""font-size:30px"">");
#nullable restore
#line 59 "C:\Users\XCV\Documents\GitHub\Kalbagtim\Views\Vaksins\Gea.cshtml"
                                                                    Write(Model.TotalVaksin3Non.ToString("N3"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                </div>\r\n              </div>\r\n            </div>\r\n</div>\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ProjectAlpha.Models.Side.VaksinViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591