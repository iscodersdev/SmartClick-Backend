#pragma checksum "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamosPlanes.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7e7ee30a62c6cc13150abab148fa278242edf23b492cb56fbe217bdf75a6c94e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Core_Views_LineasPrestamos__ListadoLineasPrestamosPlanes), @"mvc.1.0.view", @"/Areas/Core/Views/LineasPrestamos/_ListadoLineasPrestamosPlanes.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Core/Views/LineasPrestamos/_ListadoLineasPrestamosPlanes.cshtml", typeof(AspNetCore.Areas_Core_Views_LineasPrestamos__ListadoLineasPrestamosPlanes))]
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
#line 1 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamosPlanes.cshtml"
 using DAL.Models

    ;
    #line default
    #line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"7e7ee30a62c6cc13150abab148fa278242edf23b492cb56fbe217bdf75a6c94e", @"/Areas/Core/Views/LineasPrestamos/_ListadoLineasPrestamosPlanes.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"83b5709a3654b1cf54d2496b9b40e05da0f18db41523d36d2d3b8f4cc2673171", @"/Areas/Core/Views/_ViewImports.cshtml")]
    public class Areas_Core_Views_LineasPrestamos__ListadoLineasPrestamosPlanes : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<
#line 2 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamosPlanes.cshtml"
       List<LineasPrestamosPlanes>

#line default
#line hidden
    >
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", "Edición el Plan de Linea de Prestamo", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("function", "editLineaPrestamoPlanes", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("load-url", "/Core/LineasPrestamos/_UpdatePlanes/", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", "Nueva Linea de Prestamo", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("function", "nuevaLineaPrestamoPlanes", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("load-url", "/Core/LineasPrestamos/_CreatePlanes/", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", "Importar Excel", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("function", "ImportarRenglones", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("load-url", "/Core/LineasPrestamos/_EditaValores/", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Commons.TagHelpers.ModalTagHelper __Commons_TagHelpers_ModalTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(55, 356, true);
            WriteLiteral(@"<div class=""box box-info"">
    <div class=""box-header with-border filtro-grilla"">
        <span class=""txt-titulo-box"">Listado de Líneas de Préstamos</span>
        <div class=""input-group input-group-sm pull-right"" style=""max-width: 300px;"">
            <div class=""input-group input-group-sm pull-right"" style=""max-width: 300px;"">
                <a");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 411, "\"", 464, 3);
            WriteAttributeValue("", 421, "ImportarRenglones(", 421, 18, true);
            WriteAttributeValue("", 439, 
#line 8 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamosPlanes.cshtml"
                                               ViewBag.LineaPrestamoId

#line default
#line hidden
            , 439, 24, false);
            WriteAttributeValue("", 463, ")", 463, 1, true);
            EndWriteAttribute();
            BeginContext(465, 339, true);
            WriteLiteral(@" class=""btn btn-success btn-sm"">
                    <i class=""fa fa-file-excel-o""></i>&nbsp;<i class=""fa fa-plus""><span class=""hidden-xs""> Importar Excel</span></i>
                </a>
            </div>
            &nbsp;
            <div class=""input-group input-group-sm pull-right"" style=""max-width: 300px;"">
                <a");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 804, "\"", 864, 3);
            WriteAttributeValue("", 814, "nuevaLineaPrestamoPlanes(", 814, 25, true);
            WriteAttributeValue("", 839, 
#line 14 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamosPlanes.cshtml"
                                                      ViewBag.LineaPrestamoId

#line default
#line hidden
            , 839, 24, false);
            WriteAttributeValue("", 863, ")", 863, 1, true);
            EndWriteAttribute();
            BeginContext(865, 365, true);
            WriteLiteral(@" class=""btn btn-info btn-sm"">
                    <i class=""fa fa-plus""><span class=""hidden-xs""> Nuevo</span></i>
                </a>
            </div>
        </div>
    </div>
    <div class=""box-body"">
        <table class=""table table-hover table-bordered table-responsive"" id=""LineasPrestamosPlanesDataTable""></table>
    </div>
</div>


        ");
            EndContext();
            BeginContext(1230, 179, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("modal", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7e7ee30a62c6cc13150abab148fa278242edf23b492cb56fbe217bdf75a6c94e9032", async() => {
            }
            );
            __Commons_TagHelpers_ModalTagHelper = CreateTagHelper<global::Commons.TagHelpers.ModalTagHelper>();
            __tagHelperExecutionContext.Add(__Commons_TagHelpers_ModalTagHelper);
            __Commons_TagHelpers_ModalTagHelper.Title = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Commons_TagHelpers_ModalTagHelper.Callback = 
#line 26 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamosPlanes.cshtml"
                                                                            true

#line default
#line hidden
            ;
            __tagHelperExecutionContext.AddTagHelperAttribute("callback-modal", __Commons_TagHelpers_ModalTagHelper.Callback, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Commons_TagHelpers_ModalTagHelper.Function = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Commons_TagHelpers_ModalTagHelper.Url = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Commons_TagHelpers_ModalTagHelper.Sizes = global::Commons.TagHelpers.ModalSizes.
#line 26 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamosPlanes.cshtml"
                                                                                                                                                                           Medium

#line default
#line hidden
            ;
            __tagHelperExecutionContext.AddTagHelperAttribute("size", __Commons_TagHelpers_ModalTagHelper.Sizes, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1409, 10, true);
            WriteLiteral("\r\n        ");
            EndContext();
            BeginContext(1419, 167, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("modal", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7e7ee30a62c6cc13150abab148fa278242edf23b492cb56fbe217bdf75a6c94e11759", async() => {
            }
            );
            __Commons_TagHelpers_ModalTagHelper = CreateTagHelper<global::Commons.TagHelpers.ModalTagHelper>();
            __tagHelperExecutionContext.Add(__Commons_TagHelpers_ModalTagHelper);
            __Commons_TagHelpers_ModalTagHelper.Title = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Commons_TagHelpers_ModalTagHelper.Callback = 
#line 27 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamosPlanes.cshtml"
                                                               true

#line default
#line hidden
            ;
            __tagHelperExecutionContext.AddTagHelperAttribute("callback-modal", __Commons_TagHelpers_ModalTagHelper.Callback, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Commons_TagHelpers_ModalTagHelper.Function = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __Commons_TagHelpers_ModalTagHelper.Url = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __Commons_TagHelpers_ModalTagHelper.Sizes = global::Commons.TagHelpers.ModalSizes.
#line 27 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamosPlanes.cshtml"
                                                                                                                                                               Medium

#line default
#line hidden
            ;
            __tagHelperExecutionContext.AddTagHelperAttribute("size", __Commons_TagHelpers_ModalTagHelper.Sizes, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1586, 10, true);
            WriteLiteral("\r\n        ");
            EndContext();
            BeginContext(1596, 151, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("modal", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7e7ee30a62c6cc13150abab148fa278242edf23b492cb56fbe217bdf75a6c94e14462", async() => {
            }
            );
            __Commons_TagHelpers_ModalTagHelper = CreateTagHelper<global::Commons.TagHelpers.ModalTagHelper>();
            __tagHelperExecutionContext.Add(__Commons_TagHelpers_ModalTagHelper);
            __Commons_TagHelpers_ModalTagHelper.Title = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            __Commons_TagHelpers_ModalTagHelper.Callback = 
#line 28 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamosPlanes.cshtml"
                                                      true

#line default
#line hidden
            ;
            __tagHelperExecutionContext.AddTagHelperAttribute("callback-modal", __Commons_TagHelpers_ModalTagHelper.Callback, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Commons_TagHelpers_ModalTagHelper.Function = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            __Commons_TagHelpers_ModalTagHelper.Url = (string)__tagHelperAttribute_8.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
            __Commons_TagHelpers_ModalTagHelper.Sizes = global::Commons.TagHelpers.ModalSizes.
#line 28 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamosPlanes.cshtml"
                                                                                                                                               Medium

#line default
#line hidden
            ;
            __tagHelperExecutionContext.AddTagHelperAttribute("size", __Commons_TagHelpers_ModalTagHelper.Sizes, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1747, 242, true);
            WriteLiteral("\r\n        <script>\r\n\r\n        $(function () {\r\n            var table = $(\'#LineasPrestamosPlanesDataTable\').DataTable({\r\n                serverSide: true,\r\n                processing: true,\r\n                ajax: {\r\n                    url: \'");
            EndContext();
            BeginContext(1990, 44, false);
            Write(
#line 36 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamosPlanes.cshtml"
                           Url.Action("LineasPrestamosPlanesDataTable")

#line default
#line hidden
            );
            EndContext();
            BeginContext(2034, 97, true);
            WriteLiteral("\',\r\n                    type: \"POST\",\r\n                    data: {\r\n                        id: \'");
            EndContext();
            BeginContext(2132, 34, false);
            Write(
#line 39 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamosPlanes.cshtml"
                              Html.Raw(@ViewBag.LineaPrestamoId)

#line default
#line hidden
            );
            EndContext();
            BeginContext(2166, 1021, true);
            WriteLiteral(@"',
                    }
                },
                rowId: 'Id',
                columns: [
                    { data: ""MontoPrestado"", title: ""Monto Prestado""},
                    { data: ""CantidadCuotas"", title: ""Cantidad Cuotas"" },
                    { data: ""MontoCuota"", title: ""Monto Cuotas"" },
                    //{ data: ""MargenDisponible"", title: ""Margen Disponible"" },
                    {
                        ""title"": ""Acciones"",
                        ""sortable"": false,
                        ""render"": function(data, type, row) {
                            var action = '';
                            action = action + `<a datatoggle='tooltip' title='Editar' onclick=""editLineaPrestamoPlanes('${row['Id']}')"" class=""btn btn-primary btn-xs""><i class="" fa fa-file-text-o""></i></a> `;
                            
                            return action;
                        }
                    }
                ]

               });
})
        </script>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<LineasPrestamosPlanes>> Html { get; private set; }
    }
}
#pragma warning restore 1591
