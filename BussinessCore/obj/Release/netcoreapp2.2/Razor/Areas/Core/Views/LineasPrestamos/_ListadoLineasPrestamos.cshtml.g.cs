#pragma checksum "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamos.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "2104786c78adef2aa45815a2adff3491b528bb09800bfc3da895a4b1784603a4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Core_Views_LineasPrestamos__ListadoLineasPrestamos), @"mvc.1.0.view", @"/Areas/Core/Views/LineasPrestamos/_ListadoLineasPrestamos.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Core/Views/LineasPrestamos/_ListadoLineasPrestamos.cshtml", typeof(AspNetCore.Areas_Core_Views_LineasPrestamos__ListadoLineasPrestamos))]
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
#line 1 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamos.cshtml"
 using DAL.Models

    ;
    #line default
    #line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"2104786c78adef2aa45815a2adff3491b528bb09800bfc3da895a4b1784603a4", @"/Areas/Core/Views/LineasPrestamos/_ListadoLineasPrestamos.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"83b5709a3654b1cf54d2496b9b40e05da0f18db41523d36d2d3b8f4cc2673171", @"/Areas/Core/Views/_ViewImports.cshtml")]
    public class Areas_Core_Views_LineasPrestamos__ListadoLineasPrestamos : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<
#line 2 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamos.cshtml"
       Commons.Models.Page<LineasPrestamos>

#line default
#line hidden
    >
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", "Editar Linea de Prestamo", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary btn-xs"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-ajax", new global::Microsoft.AspNetCore.Html.HtmlString("true"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-ajax-update", new global::Microsoft.AspNetCore.Html.HtmlString("#opciones"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-ajax-mode", new global::Microsoft.AspNetCore.Html.HtmlString("replace"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "_ListadoLineasPrestamosPlanes", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", "Planes de Prestamos", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-warning btn-xs"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "_ListadoLineasPrestamosTipoPersona", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", "Tipo de Personas", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-info btn-xs"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_11 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_12 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("confirm-no", "Cancelar", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_13 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("confirm-yes", "Borrar", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_14 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("confirm", "¿Esta seguro de borrar esta Linea de Prestamo?", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_15 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-danger btn-xs"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_16 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", "Edición de Linea de Prestamo", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_17 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("function", "editLineaPrestamo", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_18 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("load-url", "/Core/LineasPrestamos/_Update/", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_19 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", "Nueva Linea de Prestamo", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_20 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("function", "nuevaLineaPrestamo", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_21 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("load-url", "/Core/LineasPrestamos/_Create/", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            BeginContext(64, 1725, true);
            WriteLiteral(@"
<div class=""box box-info"">
    <div class=""box-header with-border filtro-grilla"">
        <span class=""txt-titulo-box"">Listado de Líneas de Préstamos</span>
        <div class=""input-group input-group-sm pull-right"" style=""max-width: 300px;"">
            <a onclick=""nuevaLineaPrestamo()"" class=""btn btn-info btn-sm"">
                <i class=""fa fa-plus""><span class=""hidden-xs""> Nuevo</span></i>
            </a>
        </div>
    </div>
    <div class=""box-body"">
        <table id=""listadoLineasPrestamos"" class=""table table-hover table-bordered table-responsive borde_interno"">
            <thead>
                <tr>
                    <th>
                        Nombre
                    </th>
                    <th>
                        Capital Mínimo
                    </th>
                    <th>
                        Capital Máximo
                    </th>
                    <th>
                        Cuota Mínimo
                    </th>
                    <t");
            WriteLiteral(@"h>
                        Cuota Máximo
                    </th>
                    <th>
                        Cantidad de Cuotas Mínimo
                    </th>
                    <th>
                        Cantidad de Cuotas Máximo
                    </th>
                    <th>
                        SistemaFinanciacion
                    </th>
                    <th>
                        Intervalo
                    </th>
                    <th>
                        Moneda
                    </th>
                    <th>
                        Acciones
                    </th>
                </tr>
            </thead>
            <tbody>
");
            EndContext();
#line 53 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamos.cshtml"
                 foreach (var item in Model.Items)
                {

#line default
#line hidden

            BeginContext(1860, 72, true);
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1933, 41, false);
            Write(
#line 57 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamos.cshtml"
                         Html.DisplayFor(modelItem => item.Nombre)

#line default
#line hidden
            );
            EndContext();
            BeginContext(1974, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(2054, 48, false);
            Write(
#line 60 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamos.cshtml"
                         Html.DisplayFor(modelItem => item.CapitalMinimo)

#line default
#line hidden
            );
            EndContext();
            BeginContext(2102, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(2182, 48, false);
            Write(
#line 63 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamos.cshtml"
                         Html.DisplayFor(modelItem => item.CapitalMaximo)

#line default
#line hidden
            );
            EndContext();
            BeginContext(2230, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(2310, 46, false);
            Write(
#line 66 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamos.cshtml"
                         Html.DisplayFor(modelItem => item.CuotaMinima)

#line default
#line hidden
            );
            EndContext();
            BeginContext(2356, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(2436, 46, false);
            Write(
#line 69 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamos.cshtml"
                         Html.DisplayFor(modelItem => item.CuotaMaxima)

#line default
#line hidden
            );
            EndContext();
            BeginContext(2482, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(2562, 55, false);
            Write(
#line 72 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamos.cshtml"
                         Html.DisplayFor(modelItem => item.CantidadCuotasMinima)

#line default
#line hidden
            );
            EndContext();
            BeginContext(2617, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(2697, 55, false);
            Write(
#line 75 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamos.cshtml"
                         Html.DisplayFor(modelItem => item.CantidadCuotasMaxima)

#line default
#line hidden
            );
            EndContext();
            BeginContext(2752, 55, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n");
            EndContext();
#line 78 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamos.cshtml"
                         if (item.SistemaFinanciacion != null)
                        {
                            

#line default
#line hidden

            BeginContext(2927, 61, false);
            Write(
#line 80 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamos.cshtml"
                             Html.DisplayFor(modelItem => item.SistemaFinanciacion.Nombre)

#line default
#line hidden
            );
            EndContext();
#line 80 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamos.cshtml"
                                                                                          
                        }

#line default
#line hidden

            BeginContext(3017, 77, true);
            WriteLiteral("                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(3095, 44, false);
            Write(
#line 84 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamos.cshtml"
                         Html.DisplayFor(modelItem => item.Intervalo)

#line default
#line hidden
            );
            EndContext();
            BeginContext(3139, 55, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n");
            EndContext();
#line 87 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamos.cshtml"
                         if (item.Moneda != null)
                        {
                            

#line default
#line hidden

            BeginContext(3301, 48, false);
            Write(
#line 89 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamos.cshtml"
                             Html.DisplayFor(modelItem => item.Moneda.Nombre)

#line default
#line hidden
            );
            EndContext();
#line 89 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamos.cshtml"
                                                                             
                        }

#line default
#line hidden

            BeginContext(3378, 79, true);
            WriteLiteral("                    </td>\r\n\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(3457, 142, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2104786c78adef2aa45815a2adff3491b528bb09800bfc3da895a4b1784603a419458", async() => {
                BeginContext(3562, 33, true);
                WriteLiteral("<i class=\"fa fa-file-text-o\"></i>");
                EndContext();
            }
            );
            __Commons_TagHelpers_AToolipTagHelper = CreateTagHelper<global::Commons.TagHelpers.AToolipTagHelper>();
            __tagHelperExecutionContext.Add(__Commons_TagHelpers_AToolipTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "onclick", 3, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 3469, "editLineaPrestamo(", 3469, 18, true);
            AddHtmlAttributeValue("", 3487, 
#line 94 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamos.cshtml"
                                                       item.Id

#line default
#line hidden
            , 3487, 8, false);
            AddHtmlAttributeValue("", 3495, ")", 3495, 1, true);
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
            BeginContext(3599, 26, true);
            WriteLiteral("\r\n                        ");
            EndContext();
            BeginContext(3625, 456, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2104786c78adef2aa45815a2adff3491b528bb09800bfc3da895a4b1784603a421594", async() => {
                BeginContext(3993, 84, true);
                WriteLiteral("\r\n                            <i class=\"fa fa-server\"></i>\r\n                        ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Commons_TagHelpers_AToolipTagHelper = CreateTagHelper<global::Commons.TagHelpers.AToolipTagHelper>();
            __tagHelperExecutionContext.Add(__Commons_TagHelpers_AToolipTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __Commons_TagHelpers_AToolipTagHelper.Title = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
            WriteLiteral(
#line 100 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamos.cshtml"
                                          item.Id

#line default
#line hidden
            );
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(4081, 28, true);
            WriteLiteral("\r\n\r\n                        ");
            EndContext();
            BeginContext(4109, 455, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2104786c78adef2aa45815a2adff3491b528bb09800bfc3da895a4b1784603a424825", async() => {
                BeginContext(4476, 84, true);
                WriteLiteral("\r\n                            <i class=\"fa fa-server\"></i>\r\n                        ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Commons_TagHelpers_AToolipTagHelper = CreateTagHelper<global::Commons.TagHelpers.AToolipTagHelper>();
            __tagHelperExecutionContext.Add(__Commons_TagHelpers_AToolipTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_8.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
            __Commons_TagHelpers_AToolipTagHelper.Title = (string)__tagHelperAttribute_9.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_9);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
            WriteLiteral(
#line 110 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamos.cshtml"
                                          item.Id

#line default
#line hidden
            );
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_10);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(4564, 26, true);
            WriteLiteral("\r\n                        ");
            EndContext();
            BeginContext(4590, 232, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2104786c78adef2aa45815a2adff3491b528bb09800bfc3da895a4b1784603a428053", async() => {
                BeginContext(4789, 29, true);
                WriteLiteral("<i class=\"fa fa-trash-o\"></i>");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Commons_TagHelpers_AConfirmTagHelper = CreateTagHelper<global::Commons.TagHelpers.AConfirmTagHelper>();
            __tagHelperExecutionContext.Add(__Commons_TagHelpers_AConfirmTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_11.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_11);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
            WriteLiteral(
#line 114 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamos.cshtml"
                                                              item.Id

#line default
#line hidden
            );
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Commons_TagHelpers_AConfirmTagHelper.Color = global::Commons.TagHelpers.UiColor.
#line 114 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamos.cshtml"
                                                                                     Danger

#line default
#line hidden
            ;
            __tagHelperExecutionContext.AddTagHelperAttribute("confirm-icon", __Commons_TagHelpers_AConfirmTagHelper.Color, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Commons_TagHelpers_AConfirmTagHelper.ConfirmNo = (string)__tagHelperAttribute_12.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_12);
            __Commons_TagHelpers_AConfirmTagHelper.ConfirmYes = (string)__tagHelperAttribute_13.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_13);
            __Commons_TagHelpers_AConfirmTagHelper.ConfirmText = (string)__tagHelperAttribute_14.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_14);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_15);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(4822, 52, true);
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n");
            EndContext();
#line 117 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamos.cshtml"
                }

#line default
#line hidden

            BeginContext(4893, 62, true);
            WriteLiteral("            </tbody>\r\n        </table>\r\n    </div>\r\n</div>\r\n\r\n");
            EndContext();
            BeginContext(4955, 159, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("modal", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2104786c78adef2aa45815a2adff3491b528bb09800bfc3da895a4b1784603a432302", async() => {
            }
            );
            __Commons_TagHelpers_ModalTagHelper = CreateTagHelper<global::Commons.TagHelpers.ModalTagHelper>();
            __tagHelperExecutionContext.Add(__Commons_TagHelpers_ModalTagHelper);
            __Commons_TagHelpers_ModalTagHelper.Title = (string)__tagHelperAttribute_16.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_16);
            __Commons_TagHelpers_ModalTagHelper.Callback = 
#line 123 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamos.cshtml"
                                                            true

#line default
#line hidden
            ;
            __tagHelperExecutionContext.AddTagHelperAttribute("callback-modal", __Commons_TagHelpers_ModalTagHelper.Callback, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Commons_TagHelpers_ModalTagHelper.Function = (string)__tagHelperAttribute_17.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_17);
            __Commons_TagHelpers_ModalTagHelper.Url = (string)__tagHelperAttribute_18.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_18);
            __Commons_TagHelpers_ModalTagHelper.Sizes = global::Commons.TagHelpers.ModalSizes.
#line 123 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamos.cshtml"
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
            BeginContext(5114, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(5116, 155, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("modal", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2104786c78adef2aa45815a2adff3491b528bb09800bfc3da895a4b1784603a434973", async() => {
            }
            );
            __Commons_TagHelpers_ModalTagHelper = CreateTagHelper<global::Commons.TagHelpers.ModalTagHelper>();
            __tagHelperExecutionContext.Add(__Commons_TagHelpers_ModalTagHelper);
            __Commons_TagHelpers_ModalTagHelper.Title = (string)__tagHelperAttribute_19.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_19);
            __Commons_TagHelpers_ModalTagHelper.Callback = 
#line 124 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamos.cshtml"
                                                       true

#line default
#line hidden
            ;
            __tagHelperExecutionContext.AddTagHelperAttribute("callback-modal", __Commons_TagHelpers_ModalTagHelper.Callback, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Commons_TagHelpers_ModalTagHelper.Function = (string)__tagHelperAttribute_20.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_20);
            __Commons_TagHelpers_ModalTagHelper.Url = (string)__tagHelperAttribute_21.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_21);
            __Commons_TagHelpers_ModalTagHelper.Sizes = global::Commons.TagHelpers.ModalSizes.
#line 124 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamos.cshtml"
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
            BeginContext(5271, 304, true);
            WriteLiteral(@"

<script>
    $(document).ready(function () {
        $('#listadoLineasPrestamos').DataTable({
            ""columnDefs"": [{
                ""searchable"": false,
                ""orderable"": false,
                ""targets"": 5
            }],
            ""language"": {
                'url': '");
            EndContext();
            BeginContext(5576, 29, false);
            Write(
#line 135 "D:\SmartClick\BussinessCore\Areas\Core\Views\LineasPrestamos\_ListadoLineasPrestamos.cshtml"
                         Url.Content("~/lib/arg.json")

#line default
#line hidden
            );
            EndContext();
            BeginContext(5605, 51, true);
            WriteLiteral("\'\r\n            }\r\n        });\r\n    });\r\n</script>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Commons.Models.Page<LineasPrestamos>> Html { get; private set; }
    }
}
#pragma warning restore 1591
