#pragma checksum "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\Index.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "c8848237ca269ff16170d621f92f7554e74a7d65af992be457e6a47dd6a243a9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Core_Views_LineasPrestamos_Index), @"mvc.1.0.view", @"/Areas/Core/Views/LineasPrestamos/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Core/Views/LineasPrestamos/Index.cshtml", typeof(AspNetCore.Areas_Core_Views_LineasPrestamos_Index))]
namespace AspNetCore
{
    #line default
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "D:\SmartClick\BussinessCore\Areas\Core\Views\_ViewImports.cshtml"
using BussinessCore

    ;
    #line default
    #line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"c8848237ca269ff16170d621f92f7554e74a7d65af992be457e6a47dd6a243a9", @"/Areas/Core/Views/LineasPrestamos/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"83b5709a3654b1cf54d2496b9b40e05da0f18db41523d36d2d3b8f4cc2673171", @"/Areas/Core/Views/_ViewImports.cshtml")]
    public class Areas_Core_Views_LineasPrestamos_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("tablaLineasPrestamos"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("load-url", "/Core/LineasPrestamos/_ListadoLineasPrestamos", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("tablaLineasPrestamosPlanes"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Commons.TagHelpers.LoadTagHelper __Commons_TagHelpers_LoadTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\Index.cshtml"
  
    ViewData["Title"] = "Listado Lineas de Prestamos";
    ViewData["BackArrow"] = "/Home/Index";

#line default
#line hidden

            BeginContext(107, 98, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("load", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c8848237ca269ff16170d621f92f7554e74a7d65af992be457e6a47dd6a243a94453", async() => {
                BeginContext(196, 2, true);
                WriteLiteral("\r\n");
                EndContext();
            }
            );
            __Commons_TagHelpers_LoadTagHelper = CreateTagHelper<global::Commons.TagHelpers.LoadTagHelper>();
            __tagHelperExecutionContext.Add(__Commons_TagHelpers_LoadTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Commons_TagHelpers_LoadTagHelper.Url = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(205, 25, true);
            WriteLiteral("\r\n\r\n<div id=\"opciones\">\r\n");
            EndContext();
#line 9 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\Index.cshtml"
     if (@ViewBag.LineaPrestamoId != null)
    {

#line default
#line hidden

            BeginContext(281, 8, true);
            WriteLiteral("        ");
            EndContext();
            BeginContext(289, 133, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("load", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c8848237ca269ff16170d621f92f7554e74a7d65af992be457e6a47dd6a243a96158", async() => {
            }
            );
            __Commons_TagHelpers_LoadTagHelper = CreateTagHelper<global::Commons.TagHelpers.LoadTagHelper>();
            __tagHelperExecutionContext.Add(__Commons_TagHelpers_LoadTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            BeginWriteTagHelperAttribute();
            WriteLiteral("/Core/LineasPrestamos/_ListadoLineasPrestamosPlanes/");
            WriteLiteral(
#line 11 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\Index.cshtml"
                                                                                                             ViewBag.LineaPrestamoId

#line default
#line hidden
            );
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Commons_TagHelpers_LoadTagHelper.Url = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("load-url", __Commons_TagHelpers_LoadTagHelper.Url, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(422, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 12 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\Index.cshtml"
    }

#line default
#line hidden

            BeginContext(431, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 14 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\Index.cshtml"
     if (@ViewBag.TipoPersonaLineaPrestamo != null)
    {

#line default
#line hidden

            BeginContext(493, 8, true);
            WriteLiteral("        ");
            EndContext();
            BeginContext(501, 147, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("load", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c8848237ca269ff16170d621f92f7554e74a7d65af992be457e6a47dd6a243a98557", async() => {
            }
            );
            __Commons_TagHelpers_LoadTagHelper = CreateTagHelper<global::Commons.TagHelpers.LoadTagHelper>();
            __tagHelperExecutionContext.Add(__Commons_TagHelpers_LoadTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            BeginWriteTagHelperAttribute();
            WriteLiteral("/Core/LineasPrestamos/_ListadoLineasPrestamosTipoPersona/");
            WriteLiteral(
#line 16 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\Index.cshtml"
                                                                                                                  ViewBag.TipoPersonaLineaPrestamo

#line default
#line hidden
            );
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Commons_TagHelpers_LoadTagHelper.Url = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("load-url", __Commons_TagHelpers_LoadTagHelper.Url, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(648, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 17 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\Index.cshtml"
    }

#line default
#line hidden

            BeginContext(657, 6, true);
            WriteLiteral("</div>");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
