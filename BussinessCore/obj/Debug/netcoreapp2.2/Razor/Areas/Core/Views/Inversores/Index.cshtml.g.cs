#pragma checksum "D:\SmartClick\BussinessCore\Areas\Core\Views\Inversores\Index.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "097f3803a7406cab228b48c98c591ad0226a86fca5669c09cf909717dc553ade"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Core_Views_Inversores_Index), @"mvc.1.0.view", @"/Areas/Core/Views/Inversores/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Core/Views/Inversores/Index.cshtml", typeof(AspNetCore.Areas_Core_Views_Inversores_Index))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"097f3803a7406cab228b48c98c591ad0226a86fca5669c09cf909717dc553ade", @"/Areas/Core/Views/Inversores/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"83b5709a3654b1cf54d2496b9b40e05da0f18db41523d36d2d3b8f4cc2673171", @"/Areas/Core/Views/_ViewImports.cshtml")]
    public class Areas_Core_Views_Inversores_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("onclick", new global::Microsoft.AspNetCore.Html.HtmlString("nuevoInversor()"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", "Nuevo Inversor", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("pull-right btn btn-info btn-sm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", "Edición de Inversor", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("function", "editInversor", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("load-url", "/Core/Inversores/_Update/", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", "Edición de Tasas de Inversor", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("function", "editTasas", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("load-url", "/Core/Inversores/_Tasas/", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", "Logo Inversor", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("function", "editLogo", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_11 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("load-url", "/Core/Inversores/_Image/", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_12 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("function", "nuevoInversor", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_13 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("load-url", "/Core/Inversores/_Create/", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_14 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", "Borrar Inversor", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_15 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("function", "deleteInversor", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_16 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("load-url", "/Core/Inversores/_Delete/", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Commons.TagHelpers.ModalTagHelper __Commons_TagHelpers_ModalTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "D:\SmartClick\BussinessCore\Areas\Core\Views\Inversores\Index.cshtml"
  
    ViewData["Title"] = "Inversores";
    ViewData["BackArrow"] = "/Home/Index";

#line default
#line hidden

            BeginContext(90, 331, true);
            WriteLiteral(@"<div class=""row"">
    <div class=""col-md-12"">
        <div class=""box box-info"">
            <div class=""box-header with-border filtro-grilla"">
                <div class=""row"">
                    <div class=""col-md-3""><h4>Listado de Inversores</h4></div>
                    <div class=""col-md-9"">
                        ");
            EndContext();
            BeginContext(421, 219, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "097f3803a7406cab228b48c98c591ad0226a86fca5669c09cf909717dc553ade9269", async() => {
                BeginContext(517, 119, true);
                WriteLiteral("\r\n                            <i class=\"fa fa-plus\"><span class=\"hidden-xs\"> Nuevo</span></i>\r\n                        ");
                EndContext();
            }
            );
            __Commons_TagHelpers_AToolipTagHelper = CreateTagHelper<global::Commons.TagHelpers.AToolipTagHelper>();
            __tagHelperExecutionContext.Add(__Commons_TagHelpers_AToolipTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Commons_TagHelpers_AToolipTagHelper.Title = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            BeginWriteTagHelperAttribute();
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __tagHelperExecutionContext.AddHtmlAttribute("asp-", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(640, 294, true);
            WriteLiteral(@"
                    </div>
                </div>
            </div>
            <div class=""box-body"">
                <table class=""table table-hover table-bordered table-responsive borde_interno"" id=""InversorDataTable""></table>
            </div>
        </div>
    </div>
</div>
");
            EndContext();
            BeginContext(934, 140, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("modal", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "097f3803a7406cab228b48c98c591ad0226a86fca5669c09cf909717dc553ade11493", async() => {
            }
            );
            __Commons_TagHelpers_ModalTagHelper = CreateTagHelper<global::Commons.TagHelpers.ModalTagHelper>();
            __tagHelperExecutionContext.Add(__Commons_TagHelpers_ModalTagHelper);
            __Commons_TagHelpers_ModalTagHelper.Title = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Commons_TagHelpers_ModalTagHelper.Callback = 
#line 24 "D:\SmartClick\BussinessCore\Areas\Core\Views\Inversores\Index.cshtml"
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
#line 24 "D:\SmartClick\BussinessCore\Areas\Core\Views\Inversores\Index.cshtml"
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
            BeginContext(1074, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(1076, 145, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("modal", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "097f3803a7406cab228b48c98c591ad0226a86fca5669c09cf909717dc553ade14082", async() => {
            }
            );
            __Commons_TagHelpers_ModalTagHelper = CreateTagHelper<global::Commons.TagHelpers.ModalTagHelper>();
            __tagHelperExecutionContext.Add(__Commons_TagHelpers_ModalTagHelper);
            __Commons_TagHelpers_ModalTagHelper.Title = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            __Commons_TagHelpers_ModalTagHelper.Callback = 
#line 25 "D:\SmartClick\BussinessCore\Areas\Core\Views\Inversores\Index.cshtml"
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
#line 25 "D:\SmartClick\BussinessCore\Areas\Core\Views\Inversores\Index.cshtml"
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
            BeginContext(1221, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(1223, 129, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("modal", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "097f3803a7406cab228b48c98c591ad0226a86fca5669c09cf909717dc553ade16685", async() => {
            }
            );
            __Commons_TagHelpers_ModalTagHelper = CreateTagHelper<global::Commons.TagHelpers.ModalTagHelper>();
            __tagHelperExecutionContext.Add(__Commons_TagHelpers_ModalTagHelper);
            __Commons_TagHelpers_ModalTagHelper.Title = (string)__tagHelperAttribute_9.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_9);
            __Commons_TagHelpers_ModalTagHelper.Callback = 
#line 26 "D:\SmartClick\BussinessCore\Areas\Core\Views\Inversores\Index.cshtml"
                                             true

#line default
#line hidden
            ;
            __tagHelperExecutionContext.AddTagHelperAttribute("callback-modal", __Commons_TagHelpers_ModalTagHelper.Callback, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Commons_TagHelpers_ModalTagHelper.Function = (string)__tagHelperAttribute_10.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_10);
            __Commons_TagHelpers_ModalTagHelper.Url = (string)__tagHelperAttribute_11.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_11);
            __Commons_TagHelpers_ModalTagHelper.Sizes = global::Commons.TagHelpers.ModalSizes.
#line 26 "D:\SmartClick\BussinessCore\Areas\Core\Views\Inversores\Index.cshtml"
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
            BeginContext(1352, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(2354, 136, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("modal", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "097f3803a7406cab228b48c98c591ad0226a86fca5669c09cf909717dc553ade19261", async() => {
            }
            );
            __Commons_TagHelpers_ModalTagHelper = CreateTagHelper<global::Commons.TagHelpers.ModalTagHelper>();
            __tagHelperExecutionContext.Add(__Commons_TagHelpers_ModalTagHelper);
            __Commons_TagHelpers_ModalTagHelper.Title = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Commons_TagHelpers_ModalTagHelper.Callback = 
#line 33 "D:\SmartClick\BussinessCore\Areas\Core\Views\Inversores\Index.cshtml"
                                              true

#line default
#line hidden
            ;
            __tagHelperExecutionContext.AddTagHelperAttribute("callback-modal", __Commons_TagHelpers_ModalTagHelper.Callback, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Commons_TagHelpers_ModalTagHelper.Function = (string)__tagHelperAttribute_12.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_12);
            __Commons_TagHelpers_ModalTagHelper.Url = (string)__tagHelperAttribute_13.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_13);
            __Commons_TagHelpers_ModalTagHelper.Sizes = global::Commons.TagHelpers.ModalSizes.
#line 33 "D:\SmartClick\BussinessCore\Areas\Core\Views\Inversores\Index.cshtml"
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
            BeginContext(2490, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(2492, 138, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("modal", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "097f3803a7406cab228b48c98c591ad0226a86fca5669c09cf909717dc553ade21845", async() => {
            }
            );
            __Commons_TagHelpers_ModalTagHelper = CreateTagHelper<global::Commons.TagHelpers.ModalTagHelper>();
            __tagHelperExecutionContext.Add(__Commons_TagHelpers_ModalTagHelper);
            __Commons_TagHelpers_ModalTagHelper.Title = (string)__tagHelperAttribute_14.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_14);
            __Commons_TagHelpers_ModalTagHelper.Callback = 
#line 34 "D:\SmartClick\BussinessCore\Areas\Core\Views\Inversores\Index.cshtml"
                                               true

#line default
#line hidden
            ;
            __tagHelperExecutionContext.AddTagHelperAttribute("callback-modal", __Commons_TagHelpers_ModalTagHelper.Callback, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Commons_TagHelpers_ModalTagHelper.Function = (string)__tagHelperAttribute_15.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_15);
            __Commons_TagHelpers_ModalTagHelper.Url = (string)__tagHelperAttribute_16.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_16);
            __Commons_TagHelpers_ModalTagHelper.Sizes = global::Commons.TagHelpers.ModalSizes.
#line 34 "D:\SmartClick\BussinessCore\Areas\Core\Views\Inversores\Index.cshtml"
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
            BeginContext(2630, 226, true);
            WriteLiteral("\r\n<script type=\"text/javascript\">\r\n$(function () {\r\n    var table = $(\'#InversorDataTable\').DataTable({\r\n                serverSide: true,\r\n                processing: true,\r\n                ajax: {\r\n                    url: \'");
            EndContext();
            BeginContext(2857, 33, false);
            Write(
#line 41 "D:\SmartClick\BussinessCore\Areas\Core\Views\Inversores\Index.cshtml"
                           Url.Action("InversoresDataTable")

#line default
#line hidden
            );
            EndContext();
            BeginContext(2890, 3243, true);
            WriteLiteral(@"',
                    type: ""POST"",
                    },
                rowId: 'Id',
                columns: [
                    { data: ""Nombre"", title: ""Inversor""},
                    { data: ""Domicilio"", title: ""Domicilio Legal""},
                    { data: ""CUIT"", title: ""CUIT""},
                    { data: ""Tasa"", title: ""Tasa de Inversor""},
                    { data: ""TasaNominalAnual"", title: ""Tasa Nominal Anual""},
                    { data: ""TasaEfectivaAnual"", title: ""Tasa Efectiva Anual""},
                    { data: ""TasaEfectivaMensual"", title: ""Tasa Efectiva Mensual""},
                    { data: ""CFTSinImpuesto"", title: ""CFT Sin Impuesto""},
                    { data: ""CFTConImpuesto"", title: ""CFT Con Impuesto""},
                    {
                        ""title"": ""Acciones"",
                        ""sortable"": false,
                        ""render"": function(data, type, row) {
                            var action = '';
                            action = act");
            WriteLiteral(@"ion + `<a datatoggle='tooltip' title='Editar Inversor' onclick=""editInversor('${row['Id']}')"" class=""btn btn-primary btn-xs""><i class="" fa fa-file-text-o""></i></a> `;
                            action = action + `<a datatoggle='tooltip' title='Editar Tasa' onclick=""editTasas('${row['Id']}')"" class=""btn btn-warning btn-xs""><i class="" fa fa-dollar""></i></a> `;
                            action = action + `<a datatoggle='tooltip' title='Logo Inversor' onclick=""editLogo('${row['Id']}')"" class=""btn btn-info btn-xs""><i class="" fa fa-file-image-o""></i></a> `;
                            /*action = action + `<a datatoggle='tooltip' title='Editar Tasa' onclick=""editTasa('${row['Id']}')"" class=""btn btn-warning btn-xs""><i class="" fa fa-dollar""></i></a> `;
                            action = action + `<a datatoggle='tooltip' title='Editar Tasa Nominal Anual' onclick=""editTasaNominalAnual('${row['Id']}')"" class=""btn btn-warning btn-xs""><i class="" fa fa-dollar""></i></a> `;
                            action = actio");
            WriteLiteral(@"n + `<a datatoggle='tooltip' title='Editar Tasa Efectiva Anual' onclick=""editTasaEfectivaAnual('${row['Id']}')"" class=""btn btn-warning btn-xs""><i class="" fa fa-dollar""></i></a> `;
                            action = action + `<a datatoggle='tooltip' title='Editar Tasa Efectiva Mensual' onclick=""editTasaEfectivaMensual('${row['Id']}')"" class=""btn btn-warning btn-xs""><i class="" fa fa-dollar""></i></a> `;
                            action = action + `<a datatoggle='tooltip' title='Edición del CFT Sin Impuesto' onclick=""editCFTSinImpuesto('${row['Id']}')"" class=""btn btn-warning btn-xs""><i class="" fa fa-dollar""></i></a> `;
                            action = action + `<a datatoggle='tooltip' title='Edición del CFT Con Impuesto' onclick=""editCFTConImpuesto('${row['Id']}')"" class=""btn btn-warning btn-xs""><i class="" fa fa-dollar""></i></a> `;*/
                            action = action + `<a datatoggle='tooltip' title='Borrar Inversor' onclick=""deleteInversor('${row['Id']}')"" class=""btn btn-danger btn-xs""><i c");
            WriteLiteral("lass=\"fa fa-trash-o\"></i></a> `;\r\n                            return action;\r\n                        }\r\n                    }\r\n                ]\r\n    });\r\n})\r\n</script>\r\n");
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
