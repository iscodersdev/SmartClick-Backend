#pragma checksum "D:\SmartClick\BussinessCore\Areas\Core\Views\MatrizRiesgo\_ListarMatrizRenglones.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "e99c94efc115f60b76bd1b8b953ee83b4ad4b25da936e74c9d0dfe04bb44e25c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Core_Views_MatrizRiesgo__ListarMatrizRenglones), @"mvc.1.0.view", @"/Areas/Core/Views/MatrizRiesgo/_ListarMatrizRenglones.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Core/Views/MatrizRiesgo/_ListarMatrizRenglones.cshtml", typeof(AspNetCore.Areas_Core_Views_MatrizRiesgo__ListarMatrizRenglones))]
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
#line 1 "D:\SmartClick\BussinessCore\Areas\Core\Views\MatrizRiesgo\_ListarMatrizRenglones.cshtml"
 using DAL.Models

    ;
    #line default
    #line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"e99c94efc115f60b76bd1b8b953ee83b4ad4b25da936e74c9d0dfe04bb44e25c", @"/Areas/Core/Views/MatrizRiesgo/_ListarMatrizRenglones.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"83b5709a3654b1cf54d2496b9b40e05da0f18db41523d36d2d3b8f4cc2673171", @"/Areas/Core/Views/_ViewImports.cshtml")]
    public class Areas_Core_Views_MatrizRiesgo__ListarMatrizRenglones : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<
#line 2 "D:\SmartClick\BussinessCore\Areas\Core\Views\MatrizRiesgo\_ListarMatrizRenglones.cshtml"
       IEnumerable<MatrizRiesgoRenglones>

#line default
#line hidden
    >
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", "Editar Renglon", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary btn-xs"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "DeleteRenglon", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("confirm-no", "Cancelar", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("confirm-yes", "Borrar", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("confirm", "¿Esta seguro de borrar este renglon de la Matriz de Riesgo?", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-danger btn-xs"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", "Edición de Renglones de Matriz de Riesgo", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("function", "editRenglon", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("load-url", "/Core/MatrizRiesgo/_UpdateRenglon/", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", "Nuevo Renglon de Matriz de Riesgo", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_11 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("function", "nuevoRenglon", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_12 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("load-url", "/Core/MatrizRiesgo/_CreateRenglon/", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Commons.TagHelpers.AToolipTagHelper __Commons_TagHelpers_AToolipTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Commons.TagHelpers.AConfirmTagHelper __Commons_TagHelpers_AConfirmTagHelper;
        private global::Commons.TagHelpers.ModalTagHelper __Commons_TagHelpers_ModalTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(62, 167, true);
            WriteLiteral("\r\n<div class=\"box box-info\">\r\n    <div class=\"box-header with-border filtro-grilla\">\r\n        <span class=\"txt-titulo-box\">Listado de Renglones de Matriz de Riesgo de ");
            EndContext();
            BeginContext(230, 20, false);
            Write(
#line 6 "D:\SmartClick\BussinessCore\Areas\Core\Views\MatrizRiesgo\_ListarMatrizRenglones.cshtml"
                                                                                  ViewBag.MatrizNombre

#line default
#line hidden
            );
            EndContext();
            BeginContext(250, 110, true);
            WriteLiteral("</span>\r\n        <div class=\"input-group input-group-sm pull-right\" style=\"max-width: 300px;\">\r\n            <a");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 360, "\"", 401, 3);
            WriteAttributeValue("", 370, "nuevoRenglon(", 370, 13, true);
            WriteAttributeValue("", 383, 
#line 8 "D:\SmartClick\BussinessCore\Areas\Core\Views\MatrizRiesgo\_ListarMatrizRenglones.cshtml"
                                      ViewBag.MatrizId

#line default
#line hidden
            , 383, 17, false);
            WriteAttributeValue("", 400, ")", 400, 1, true);
            EndWriteAttribute();
            BeginContext(402, 675, true);
            WriteLiteral(@" class=""btn btn-info btn-sm"">
                <i class=""fa fa-plus""><span class=""hidden-xs""> Nuevo</span></i>
            </a>
        </div>
    </div>
    <div class=""box-body"">
        <table id=""listadoRenglones"" class=""table table-hover table-bordered table-responsive borde_interno"">
            <thead>
                <tr>
                    <th>
                        Probabilidad
                    </th>
                    <th>
                        Consecuencia
                    </th>
                    <th>
                        Acciones
                    </th>
                </tr>
            </thead>
            <tbody>
");
            EndContext();
#line 29 "D:\SmartClick\BussinessCore\Areas\Core\Views\MatrizRiesgo\_ListarMatrizRenglones.cshtml"
                 foreach (var item in Model.ToList())
                {

#line default
#line hidden

            BeginContext(1151, 72, true);
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1224, 54, false);
            Write(
#line 33 "D:\SmartClick\BussinessCore\Areas\Core\Views\MatrizRiesgo\_ListarMatrizRenglones.cshtml"
                         Html.DisplayFor(modelItem => item.Probabilidad.Nombre)

#line default
#line hidden
            );
            EndContext();
            BeginContext(1278, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1358, 54, false);
            Write(
#line 36 "D:\SmartClick\BussinessCore\Areas\Core\Views\MatrizRiesgo\_ListarMatrizRenglones.cshtml"
                         Html.DisplayFor(modelItem => item.Consecuencia.Nombre)

#line default
#line hidden
            );
            EndContext();
            BeginContext(1412, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1491, 126, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e99c94efc115f60b76bd1b8b953ee83b4ad4b25da936e74c9d0dfe04bb44e25c11650", async() => {
                BeginContext(1580, 33, true);
                WriteLiteral("<i class=\"fa fa-file-text-o\"></i>");
                EndContext();
            }
            );
            __Commons_TagHelpers_AToolipTagHelper = CreateTagHelper<global::Commons.TagHelpers.AToolipTagHelper>();
            __tagHelperExecutionContext.Add(__Commons_TagHelpers_AToolipTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "onclick", 3, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1503, "editRenglon(", 1503, 12, true);
            AddHtmlAttributeValue("", 1515, 
#line 39 "D:\SmartClick\BussinessCore\Areas\Core\Views\MatrizRiesgo\_ListarMatrizRenglones.cshtml"
                                                 item.Id

#line default
#line hidden
            , 1515, 8, false);
            AddHtmlAttributeValue("", 1523, ")", 1523, 1, true);
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __Commons_TagHelpers_AToolipTagHelper.Title = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1617, 26, true);
            WriteLiteral("\r\n                        ");
            EndContext();
            BeginContext(1643, 252, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e99c94efc115f60b76bd1b8b953ee83b4ad4b25da936e74c9d0dfe04bb44e25c13770", async() => {
                BeginContext(1862, 29, true);
                WriteLiteral("<i class=\"fa fa-trash-o\"></i>");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Commons_TagHelpers_AConfirmTagHelper = CreateTagHelper<global::Commons.TagHelpers.AConfirmTagHelper>();
            __tagHelperExecutionContext.Add(__Commons_TagHelpers_AConfirmTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
            WriteLiteral(
#line 40 "D:\SmartClick\BussinessCore\Areas\Core\Views\MatrizRiesgo\_ListarMatrizRenglones.cshtml"
                                                                     item.Id

#line default
#line hidden
            );
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Commons_TagHelpers_AConfirmTagHelper.Color = global::Commons.TagHelpers.UiColor.
#line 40 "D:\SmartClick\BussinessCore\Areas\Core\Views\MatrizRiesgo\_ListarMatrizRenglones.cshtml"
                                                                                            Danger

#line default
#line hidden
            ;
            __tagHelperExecutionContext.AddTagHelperAttribute("confirm-icon", __Commons_TagHelpers_AConfirmTagHelper.Color, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Commons_TagHelpers_AConfirmTagHelper.ConfirmNo = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Commons_TagHelpers_AConfirmTagHelper.ConfirmYes = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __Commons_TagHelpers_AConfirmTagHelper.ConfirmText = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1895, 52, true);
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n");
            EndContext();
#line 43 "D:\SmartClick\BussinessCore\Areas\Core\Views\MatrizRiesgo\_ListarMatrizRenglones.cshtml"
                }

#line default
#line hidden

            BeginContext(1966, 62, true);
            WriteLiteral("            </tbody>\r\n        </table>\r\n    </div>\r\n</div>\r\n\r\n");
            EndContext();
            BeginContext(2028, 169, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("modal", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e99c94efc115f60b76bd1b8b953ee83b4ad4b25da936e74c9d0dfe04bb44e25c18009", async() => {
            }
            );
            __Commons_TagHelpers_ModalTagHelper = CreateTagHelper<global::Commons.TagHelpers.ModalTagHelper>();
            __tagHelperExecutionContext.Add(__Commons_TagHelpers_ModalTagHelper);
            __Commons_TagHelpers_ModalTagHelper.Title = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            __Commons_TagHelpers_ModalTagHelper.Callback = 
#line 49 "D:\SmartClick\BussinessCore\Areas\Core\Views\MatrizRiesgo\_ListarMatrizRenglones.cshtml"
                                                                        true

#line default
#line hidden
            ;
            __tagHelperExecutionContext.AddTagHelperAttribute("callback-modal", __Commons_TagHelpers_ModalTagHelper.Callback, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Commons_TagHelpers_ModalTagHelper.Function = (string)__tagHelperAttribute_8.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
            __Commons_TagHelpers_ModalTagHelper.Url = (string)__tagHelperAttribute_9.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_9);
            __Commons_TagHelpers_ModalTagHelper.Sizes = global::Commons.TagHelpers.ModalSizes.
#line 49 "D:\SmartClick\BussinessCore\Areas\Core\Views\MatrizRiesgo\_ListarMatrizRenglones.cshtml"
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
            BeginContext(2197, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(2199, 163, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("modal", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e99c94efc115f60b76bd1b8b953ee83b4ad4b25da936e74c9d0dfe04bb44e25c20686", async() => {
            }
            );
            __Commons_TagHelpers_ModalTagHelper = CreateTagHelper<global::Commons.TagHelpers.ModalTagHelper>();
            __tagHelperExecutionContext.Add(__Commons_TagHelpers_ModalTagHelper);
            __Commons_TagHelpers_ModalTagHelper.Title = (string)__tagHelperAttribute_10.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_10);
            __Commons_TagHelpers_ModalTagHelper.Callback = 
#line 50 "D:\SmartClick\BussinessCore\Areas\Core\Views\MatrizRiesgo\_ListarMatrizRenglones.cshtml"
                                                                 true

#line default
#line hidden
            ;
            __tagHelperExecutionContext.AddTagHelperAttribute("callback-modal", __Commons_TagHelpers_ModalTagHelper.Callback, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Commons_TagHelpers_ModalTagHelper.Function = (string)__tagHelperAttribute_11.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_11);
            __Commons_TagHelpers_ModalTagHelper.Url = (string)__tagHelperAttribute_12.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_12);
            __Commons_TagHelpers_ModalTagHelper.Sizes = global::Commons.TagHelpers.ModalSizes.
#line 50 "D:\SmartClick\BussinessCore\Areas\Core\Views\MatrizRiesgo\_ListarMatrizRenglones.cshtml"
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
            BeginContext(2362, 346, true);
            WriteLiteral(@"


<script>
    $(document).ready(function () {
        $('#listadoRenglones').DataTable(
        {
            ""columnDefs"": [{ ""width"": ""40%"", ""targets"": 0 }, { ""width"": ""40%"", ""targets"": 1 }, { ""width"": ""20%"",class:""text-center"",""searchable"": false,""orderable"": false,""targets"": 2}],
            ""language"": {
                'url': '");
            EndContext();
            BeginContext(2709, 29, false);
            Write(
#line 59 "D:\SmartClick\BussinessCore\Areas\Core\Views\MatrizRiesgo\_ListarMatrizRenglones.cshtml"
                         Url.Content("~/lib/arg.json")

#line default
#line hidden
            );
            EndContext();
            BeginContext(2738, 55, true);
            WriteLiteral("\'\r\n            }\r\n        }\r\n    );\r\n    });\r\n</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<MatrizRiesgoRenglones>> Html { get; private set; }
    }
}
#pragma warning restore 1591
