#pragma checksum "D:\SmartClick\BussinessCore\Areas\Administracion\Views\Usuarios\Index.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "0e8bafec59a02df30ce1d3bf7c65609c7f225d6f762bbe22162f725e1104384b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Administracion_Views_Usuarios_Index), @"mvc.1.0.view", @"/Areas/Administracion/Views/Usuarios/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Administracion/Views/Usuarios/Index.cshtml", typeof(AspNetCore.Areas_Administracion_Views_Usuarios_Index))]
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
#line 1 "D:\SmartClick\BussinessCore\Areas\Administracion\Views\_ViewImports.cshtml"
using SmartClickCore

    ;
#line 2 "D:\SmartClick\BussinessCore\Areas\Administracion\Views\_ViewImports.cshtml"
using DAL.Models

    ;
#line 1 "D:\SmartClick\BussinessCore\Areas\Administracion\Views\Usuarios\Index.cshtml"
 using Commons.Extensions

    ;
    #line default
    #line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"0e8bafec59a02df30ce1d3bf7c65609c7f225d6f762bbe22162f725e1104384b", @"/Areas/Administracion/Views/Usuarios/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"234e1ec0143285c371aefe10cd1bebab2b843b9750c887ecf25823a4842b6c55", @"/Areas/Administracion/Views/_ViewImports.cshtml")]
    public class Areas_Administracion_Views_Usuarios_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("onclick", new global::Microsoft.AspNetCore.Html.HtmlString("validarUsuariosCGE()"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", "Crear Usuario", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary btn-sm btn-warning"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("onclick", new global::Microsoft.AspNetCore.Html.HtmlString("NuevoUsuario()"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary btn-sm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", "Usuario", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("function", "verUsuario", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("load-url", "/Admin/Usuario/_DetalleUsuario", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", "Creacion de Usuario", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("function", "crearUsuarioNumeroDocumento", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("load-url", "/Admin/Usuario/_BuscarPersonaPorDocumento", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_11 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", "Modificar roles", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_12 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("load-url", "/SecurityRoles/_Assign", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_13 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("function", "Roles", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_14 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", "Reportes", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_15 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("load-url", "/Admin/Usuario/_ReportesUsuario", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_16 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("function", "Reportes", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_17 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", "Nuevo Usuario", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_18 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("load-url", "/Administracion/Usuarios/Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_19 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("function", "NuevoUsuario", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_20 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", "Editar Usuario", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_21 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("function", "editUser", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_22 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("load-url", "/Administracion/Usuarios/Update/", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_23 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", "Cambiar Password Web", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_24 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("function", "UpdatePassword", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_25 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("load-url", "/Administracion/Usuarios/UpdatePasswordWeb/", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            BeginContext(27, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "D:\SmartClick\BussinessCore\Areas\Administracion\Views\Usuarios\Index.cshtml"
  
    ViewData["Title"] = "Usuarios";
    ViewData["BackArrow"] = "/Home/Index";

#line default
#line hidden

            BeginContext(117, 348, true);
            WriteLiteral(@"

<div class=""box box-info"">
    <div class=""box-header with-border filtro-grilla"">
        <div class=""container-fluid"">
            <div class=""row"">
                <div class=""col-md-9 label-form""><label for=""estadoId"">Listado de Usuarios</label></div>
                <div class=""col-md-3"">
                    <div class=""col-md-6"">
");
            EndContext();
#line 16 "D:\SmartClick\BussinessCore\Areas\Administracion\Views\Usuarios\Index.cshtml"
                         if (User.Identity.Name == "pruebasSmartClick@SmartClick.com")
                        {

#line default
#line hidden

            BeginContext(580, 28, true);
            WriteLiteral("                            ");
            EndContext();
            BeginContext(608, 161, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0e8bafec59a02df30ce1d3bf7c65609c7f225d6f762bbe22162f725e1104384b13009", async() => {
                BeginContext(707, 58, true);
                WriteLiteral("<i class=\"fa fa-wrench\"></i>&nbsp Validar Usuarios con CGE");
                EndContext();
            }
            );
            __Commons_TagHelpers_AToolipTagHelper = CreateTagHelper<global::Commons.TagHelpers.AToolipTagHelper>();
            __tagHelperExecutionContext.Add(__Commons_TagHelpers_AToolipTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Commons_TagHelpers_AToolipTagHelper.Title = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(769, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 19 "D:\SmartClick\BussinessCore\Areas\Administracion\Views\Usuarios\Index.cshtml"
                        }

#line default
#line hidden

            BeginContext(798, 98, true);
            WriteLiteral("\r\n                    </div>\r\n                    <div class=\"col-md-6\">\r\n                        ");
            EndContext();
            BeginContext(896, 130, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0e8bafec59a02df30ce1d3bf7c65609c7f225d6f762bbe22162f725e1104384b14915", async() => {
                BeginContext(977, 45, true);
                WriteLiteral("<i class=\"fa fa-user\"></i>&nbsp Crear Usuario");
                EndContext();
            }
            );
            __Commons_TagHelpers_AToolipTagHelper = CreateTagHelper<global::Commons.TagHelpers.AToolipTagHelper>();
            __tagHelperExecutionContext.Add(__Commons_TagHelpers_AToolipTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __Commons_TagHelpers_AToolipTagHelper.Title = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1026, 267, true);
            WriteLiteral(@"
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class=""box-body"">
        <table class=""table table-hover table-bordered table-responsive borde_interno"" id=""usersTable""></table>
    </div>
</div>


");
            EndContext();
            BeginContext(1293, 108, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("modal", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0e8bafec59a02df30ce1d3bf7c65609c7f225d6f762bbe22162f725e1104384b16727", async() => {
            }
            );
            __Commons_TagHelpers_ModalTagHelper = CreateTagHelper<global::Commons.TagHelpers.ModalTagHelper>();
            __tagHelperExecutionContext.Add(__Commons_TagHelpers_ModalTagHelper);
            __Commons_TagHelpers_ModalTagHelper.Title = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __Commons_TagHelpers_ModalTagHelper.Function = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            __Commons_TagHelpers_ModalTagHelper.Url = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            __Commons_TagHelpers_ModalTagHelper.Sizes = global::Commons.TagHelpers.ModalSizes.
#line 35 "D:\SmartClick\BussinessCore\Areas\Administracion\Views\Usuarios\Index.cshtml"
                                                                                             Large

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
            BeginContext(1401, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(1403, 171, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("modal", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0e8bafec59a02df30ce1d3bf7c65609c7f225d6f762bbe22162f725e1104384b18835", async() => {
            }
            );
            __Commons_TagHelpers_ModalTagHelper = CreateTagHelper<global::Commons.TagHelpers.ModalTagHelper>();
            __tagHelperExecutionContext.Add(__Commons_TagHelpers_ModalTagHelper);
            __Commons_TagHelpers_ModalTagHelper.Callback = 
#line 36 "D:\SmartClick\BussinessCore\Areas\Administracion\Views\Usuarios\Index.cshtml"
                       true

#line default
#line hidden
            ;
            __tagHelperExecutionContext.AddTagHelperAttribute("callback-modal", __Commons_TagHelpers_ModalTagHelper.Callback, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Commons_TagHelpers_ModalTagHelper.Title = (string)__tagHelperAttribute_8.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
            __Commons_TagHelpers_ModalTagHelper.Function = (string)__tagHelperAttribute_9.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_9);
            __Commons_TagHelpers_ModalTagHelper.Url = (string)__tagHelperAttribute_10.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_10);
            __Commons_TagHelpers_ModalTagHelper.Sizes = global::Commons.TagHelpers.ModalSizes.
#line 36 "D:\SmartClick\BussinessCore\Areas\Administracion\Views\Usuarios\Index.cshtml"
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
            BeginContext(1574, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(1576, 90, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("modal", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0e8bafec59a02df30ce1d3bf7c65609c7f225d6f762bbe22162f725e1104384b21444", async() => {
            }
            );
            __Commons_TagHelpers_ModalTagHelper = CreateTagHelper<global::Commons.TagHelpers.ModalTagHelper>();
            __tagHelperExecutionContext.Add(__Commons_TagHelpers_ModalTagHelper);
            __Commons_TagHelpers_ModalTagHelper.Title = (string)__tagHelperAttribute_11.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_11);
            __Commons_TagHelpers_ModalTagHelper.Url = (string)__tagHelperAttribute_12.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_12);
            __Commons_TagHelpers_ModalTagHelper.Function = (string)__tagHelperAttribute_13.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_13);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1666, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(1668, 130, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("modal", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0e8bafec59a02df30ce1d3bf7c65609c7f225d6f762bbe22162f725e1104384b23028", async() => {
            }
            );
            __Commons_TagHelpers_ModalTagHelper = CreateTagHelper<global::Commons.TagHelpers.ModalTagHelper>();
            __tagHelperExecutionContext.Add(__Commons_TagHelpers_ModalTagHelper);
            __Commons_TagHelpers_ModalTagHelper.Callback = 
#line 38 "D:\SmartClick\BussinessCore\Areas\Administracion\Views\Usuarios\Index.cshtml"
                       true

#line default
#line hidden
            ;
            __tagHelperExecutionContext.AddTagHelperAttribute("callback-modal", __Commons_TagHelpers_ModalTagHelper.Callback, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Commons_TagHelpers_ModalTagHelper.Title = (string)__tagHelperAttribute_14.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_14);
            __Commons_TagHelpers_ModalTagHelper.Url = (string)__tagHelperAttribute_15.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_15);
            __Commons_TagHelpers_ModalTagHelper.Function = (string)__tagHelperAttribute_16.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_16);
            __Commons_TagHelpers_ModalTagHelper.Sizes = global::Commons.TagHelpers.ModalSizes.
#line 38 "D:\SmartClick\BussinessCore\Areas\Administracion\Views\Usuarios\Index.cshtml"
                                                                                                                   Large

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
            BeginContext(1798, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(1800, 140, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("modal", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0e8bafec59a02df30ce1d3bf7c65609c7f225d6f762bbe22162f725e1104384b25601", async() => {
            }
            );
            __Commons_TagHelpers_ModalTagHelper = CreateTagHelper<global::Commons.TagHelpers.ModalTagHelper>();
            __tagHelperExecutionContext.Add(__Commons_TagHelpers_ModalTagHelper);
            __Commons_TagHelpers_ModalTagHelper.Callback = 
#line 39 "D:\SmartClick\BussinessCore\Areas\Administracion\Views\Usuarios\Index.cshtml"
                       true

#line default
#line hidden
            ;
            __tagHelperExecutionContext.AddTagHelperAttribute("callback-modal", __Commons_TagHelpers_ModalTagHelper.Callback, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Commons_TagHelpers_ModalTagHelper.Title = (string)__tagHelperAttribute_17.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_17);
            __Commons_TagHelpers_ModalTagHelper.Url = (string)__tagHelperAttribute_18.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_18);
            __Commons_TagHelpers_ModalTagHelper.Function = (string)__tagHelperAttribute_19.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_19);
            __Commons_TagHelpers_ModalTagHelper.Sizes = global::Commons.TagHelpers.ModalSizes.
#line 39 "D:\SmartClick\BussinessCore\Areas\Administracion\Views\Usuarios\Index.cshtml"
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
            BeginContext(1940, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
            BeginContext(1944, 138, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("modal", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0e8bafec59a02df30ce1d3bf7c65609c7f225d6f762bbe22162f725e1104384b28188", async() => {
            }
            );
            __Commons_TagHelpers_ModalTagHelper = CreateTagHelper<global::Commons.TagHelpers.ModalTagHelper>();
            __tagHelperExecutionContext.Add(__Commons_TagHelpers_ModalTagHelper);
            __Commons_TagHelpers_ModalTagHelper.Title = (string)__tagHelperAttribute_20.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_20);
            __Commons_TagHelpers_ModalTagHelper.Callback = 
#line 41 "D:\SmartClick\BussinessCore\Areas\Administracion\Views\Usuarios\Index.cshtml"
                                              true

#line default
#line hidden
            ;
            __tagHelperExecutionContext.AddTagHelperAttribute("callback-modal", __Commons_TagHelpers_ModalTagHelper.Callback, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Commons_TagHelpers_ModalTagHelper.Function = (string)__tagHelperAttribute_21.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_21);
            __Commons_TagHelpers_ModalTagHelper.Url = (string)__tagHelperAttribute_22.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_22);
            __Commons_TagHelpers_ModalTagHelper.Sizes = global::Commons.TagHelpers.ModalSizes.
#line 41 "D:\SmartClick\BussinessCore\Areas\Administracion\Views\Usuarios\Index.cshtml"
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
            BeginContext(2082, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(2084, 161, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("modal", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0e8bafec59a02df30ce1d3bf7c65609c7f225d6f762bbe22162f725e1104384b30792", async() => {
            }
            );
            __Commons_TagHelpers_ModalTagHelper = CreateTagHelper<global::Commons.TagHelpers.ModalTagHelper>();
            __tagHelperExecutionContext.Add(__Commons_TagHelpers_ModalTagHelper);
            __Commons_TagHelpers_ModalTagHelper.Title = (string)__tagHelperAttribute_23.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_23);
            __Commons_TagHelpers_ModalTagHelper.Callback = 
#line 42 "D:\SmartClick\BussinessCore\Areas\Administracion\Views\Usuarios\Index.cshtml"
                                                    true

#line default
#line hidden
            ;
            __tagHelperExecutionContext.AddTagHelperAttribute("callback-modal", __Commons_TagHelpers_ModalTagHelper.Callback, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Commons_TagHelpers_ModalTagHelper.Function = (string)__tagHelperAttribute_24.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_24);
            __Commons_TagHelpers_ModalTagHelper.Url = (string)__tagHelperAttribute_25.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_25);
            __Commons_TagHelpers_ModalTagHelper.Sizes = global::Commons.TagHelpers.ModalSizes.
#line 42 "D:\SmartClick\BussinessCore\Areas\Administracion\Views\Usuarios\Index.cshtml"
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
            BeginContext(2245, 214, true);
            WriteLiteral("\r\n\r\n<script>\r\n    $(function() {\r\n            var usersTable = $(\'#usersTable\').DataTable({\r\n                serverSide: true,\r\n                processing: true,\r\n                ajax: {\r\n                    url: \'");
            EndContext();
            BeginContext(2460, 31, false);
            Write(
#line 50 "D:\SmartClick\BussinessCore\Areas\Administracion\Views\Usuarios\Index.cshtml"
                           Url.Action("UsuariosDataTable")

#line default
#line hidden
            );
            EndContext();
            BeginContext(2491, 1799, true);
            WriteLiteral(@"',
                    type: ""POST""
                },
                rowId: 'Id',
                columns: [
                    {
                        data: ""Usuario"", title: ""Usuario"", responsivePriority: 1
                    },
                    {
                        data: ""Nombre"", title: ""Nombre"", responsivePriority: 2
                    },
                    {
                        data: ""Empresa"", title: ""Empresa"", responsivePriority: 3
                    },
                    {
                        ""title"": ""Actions"",
                        ""sortable"": false,
                        responsivePriority: 1,
                        ""render"": function (data, type, row) {
                            var action = '';
                            action = action + `<a onclick=""Roles('${row['Id']}')"" title=""Asignar Roles"" class=""btn btn-primary btn-xs""><i class=""fa fa-plus""></i></a> &nbsp`;
                            action = action +
                               ");
            WriteLiteral(@" `<a onclick=""editUser('${row['Id']}')"" title=""Editar Usuario"" class=""btn btn-info btn-xs""><i class=""fa fa-pencil""></i></a> &nbsp`;
                            //action = action +
                             //   `<a onclick=""UpdatePassword('${row['Id']}')"" title=""Cambiar Password Web"" class=""btn btn-warning btn-xs""><i class=""fa fa-pencil""></i></a> &nbsp`;
                            return action;
                        }
                    }

                ]
            });
        }
    )

    function validarUsuariosCGE() {
        $.ajax({
            type: ""POST"",
            url: ""Usuarios/validarUsuariosCGE"",
            success: function (datos) {
                alert(datos);
            },
        });
    }



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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
