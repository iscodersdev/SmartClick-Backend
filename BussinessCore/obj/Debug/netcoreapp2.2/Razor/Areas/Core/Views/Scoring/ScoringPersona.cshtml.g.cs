#pragma checksum "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "bdd70ba8962c126dd9d72b21fb9f8204b843f79c9d74ef0ba294777ad52eff82"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Core_Views_Scoring_ScoringPersona), @"mvc.1.0.view", @"/Areas/Core/Views/Scoring/ScoringPersona.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Core/Views/Scoring/ScoringPersona.cshtml", typeof(AspNetCore.Areas_Core_Views_Scoring_ScoringPersona))]
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
#line 1 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
 using Commons.HtmlHelpers

    ;
    #line default
    #line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"bdd70ba8962c126dd9d72b21fb9f8204b843f79c9d74ef0ba294777ad52eff82", @"/Areas/Core/Views/Scoring/ScoringPersona.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"83b5709a3654b1cf54d2496b9b40e05da0f18db41523d36d2d3b8f4cc2673171", @"/Areas/Core/Views/_ViewImports.cshtml")]
    public class Areas_Core_Views_Scoring_ScoringPersona : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<
#line 2 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
       DAL.DTOs.ScoringDTO

#line default
#line hidden
    >
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(56, 72, true);
            WriteLiteral("<div class=\"col-md-6\">\r\n    <div class=\"box box-widget widget-user-2\">\r\n");
            EndContext();
#line 5 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
         if (Model.Scoring >= 7)
        {

#line default
#line hidden

            BeginContext(173, 214, true);
            WriteLiteral("            <div class=\"widget-user-header bg-green\">\r\n                <div class=\"widget-user-header bg-green\">\r\n                    <div class=\"widget-user-image\">\r\n                        <img class=\"img-circle\"");
            EndContext();
            BeginWriteAttribute("src", " src=\"", 387, "\"", 404, 1);
            WriteAttributeValue("", 393, 
#line 10 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
                                                      Model.Foto

#line default
#line hidden
            , 393, 11, false);
            EndWriteAttribute();
            BeginContext(405, 102, true);
            WriteLiteral(" alt=\"User Avatar\">\r\n                    </div>\r\n                    <h3 class=\"widget-user-username\">");
            EndContext();
            BeginContext(508, 14, false);
            Write(
#line 12 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
                                                      Model.Apellido

#line default
#line hidden
            );
            EndContext();
            BeginContext(522, 1, true);
            WriteLiteral(",");
            EndContext();
            BeginContext(524, 13, false);
            Write(
#line 12 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
                                                                      Model.Nombres

#line default
#line hidden
            );
            EndContext();
            BeginContext(537, 56, true);
            WriteLiteral("</h3>\r\n                    <h5 class=\"widget-user-desc\">");
            EndContext();
            BeginContext(594, 17, false);
            Write(
#line 13 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
                                                  Model.TipoPersona

#line default
#line hidden
            );
            EndContext();
            BeginContext(611, 8, true);
            WriteLiteral(" - DNI: ");
            EndContext();
            BeginContext(620, 9, false);
            Write(
#line 13 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
                                                                            Model.DNI

#line default
#line hidden
            );
            EndContext();
            BeginContext(629, 51, true);
            WriteLiteral("</h5>\r\n                </div>\r\n            </div>\r\n");
            EndContext();
#line 16 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
        }

#line default
#line hidden

#line 17 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
         if (Model.Scoring >= 4 && Model.Scoring <= 6)
        {

#line default
#line hidden

            BeginContext(758, 218, true);
            WriteLiteral("            <div class=\"widget-user-header bg-warning\">\r\n                <div class=\"widget-user-header bg-warning\">\r\n                    <div class=\"widget-user-image\">\r\n                        <img class=\"img-circle\"");
            EndContext();
            BeginWriteAttribute("src", " src=\"", 976, "\"", 993, 1);
            WriteAttributeValue("", 982, 
#line 22 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
                                                      Model.Foto

#line default
#line hidden
            , 982, 11, false);
            EndWriteAttribute();
            BeginContext(994, 102, true);
            WriteLiteral(" alt=\"User Avatar\">\r\n                    </div>\r\n                    <h3 class=\"widget-user-username\">");
            EndContext();
            BeginContext(1097, 14, false);
            Write(
#line 24 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
                                                      Model.Apellido

#line default
#line hidden
            );
            EndContext();
            BeginContext(1111, 1, true);
            WriteLiteral(",");
            EndContext();
            BeginContext(1113, 13, false);
            Write(
#line 24 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
                                                                      Model.Nombres

#line default
#line hidden
            );
            EndContext();
            BeginContext(1126, 56, true);
            WriteLiteral("</h3>\r\n                    <h5 class=\"widget-user-desc\">");
            EndContext();
            BeginContext(1183, 17, false);
            Write(
#line 25 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
                                                  Model.TipoPersona

#line default
#line hidden
            );
            EndContext();
            BeginContext(1200, 8, true);
            WriteLiteral(" - DNI: ");
            EndContext();
            BeginContext(1209, 9, false);
            Write(
#line 25 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
                                                                            Model.DNI

#line default
#line hidden
            );
            EndContext();
            BeginContext(1218, 51, true);
            WriteLiteral("</h5>\r\n                </div>\r\n            </div>\r\n");
            EndContext();
#line 28 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
        }

#line default
#line hidden

#line 29 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
         if (Model.Scoring <= 3)
        {

#line default
#line hidden

            BeginContext(1325, 210, true);
            WriteLiteral("            <div class=\"widget-user-header bg-red\">\r\n                <div class=\"widget-user-header bg-red\">\r\n                    <div class=\"widget-user-image\">\r\n                        <img class=\"img-circle\"");
            EndContext();
            BeginWriteAttribute("src", " src=\"", 1535, "\"", 1552, 1);
            WriteAttributeValue("", 1541, 
#line 34 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
                                                      Model.Foto

#line default
#line hidden
            , 1541, 11, false);
            EndWriteAttribute();
            BeginContext(1553, 102, true);
            WriteLiteral(" alt=\"User Avatar\">\r\n                    </div>\r\n                    <h3 class=\"widget-user-username\">");
            EndContext();
            BeginContext(1656, 14, false);
            Write(
#line 36 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
                                                      Model.Apellido

#line default
#line hidden
            );
            EndContext();
            BeginContext(1670, 1, true);
            WriteLiteral(",");
            EndContext();
            BeginContext(1672, 13, false);
            Write(
#line 36 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
                                                                      Model.Nombres

#line default
#line hidden
            );
            EndContext();
            BeginContext(1685, 56, true);
            WriteLiteral("</h3>\r\n                    <h5 class=\"widget-user-desc\">");
            EndContext();
            BeginContext(1742, 17, false);
            Write(
#line 37 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
                                                  Model.TipoPersona

#line default
#line hidden
            );
            EndContext();
            BeginContext(1759, 8, true);
            WriteLiteral(" - DNI: ");
            EndContext();
            BeginContext(1768, 9, false);
            Write(
#line 37 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
                                                                            Model.DNI

#line default
#line hidden
            );
            EndContext();
            BeginContext(1777, 51, true);
            WriteLiteral("</h5>\r\n                </div>\r\n            </div>\r\n");
            EndContext();
#line 40 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
        }

#line default
#line hidden

            BeginContext(1839, 79, true);
            WriteLiteral("    <div class=\"box-footer no-padding\">\r\n        <ul class=\"nav nav-stacked\">\r\n");
            EndContext();
#line 43 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
             if (Model.Baja == false)
            {

#line default
#line hidden

            BeginContext(1972, 77, true);
            WriteLiteral("                <li><a href=\"#\">Baja <span class=\"pull-right badge bg-green\">");
            EndContext();
            BeginContext(2050, 21, false);
            Write(
#line 45 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
                                                                              Model.DescripcionBaja

#line default
#line hidden
            );
            EndContext();
            BeginContext(2071, 18, true);
            WriteLiteral("</span></a></li>\r\n");
            EndContext();
#line 46 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
            }
            else
            {

#line default
#line hidden

            BeginContext(2137, 75, true);
            WriteLiteral("                <li><a href=\"#\">Baja <span class=\"pull-right badge bg-red\">");
            EndContext();
            BeginContext(2213, 21, false);
            Write(
#line 49 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
                                                                            Model.DescripcionBaja

#line default
#line hidden
            );
            EndContext();
            BeginContext(2234, 18, true);
            WriteLiteral("</span></a></li>\r\n");
            EndContext();
#line 50 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
            }

#line default
#line hidden

            BeginContext(2267, 53, true);
            WriteLiteral("        </ul>\r\n        <ul class=\"nav nav-stacked\">\r\n");
            EndContext();
#line 53 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
             if (Model.Quiebra == false)
            {

#line default
#line hidden

            BeginContext(2377, 80, true);
            WriteLiteral("                <li><a href=\"#\">Quiebra <span class=\"pull-right badge bg-green\">");
            EndContext();
            BeginContext(2458, 24, false);
            Write(
#line 55 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
                                                                                 Model.DescripcionQuiebra

#line default
#line hidden
            );
            EndContext();
            BeginContext(2482, 18, true);
            WriteLiteral("</span></a></li>\r\n");
            EndContext();
#line 56 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
            }
            else
            {

#line default
#line hidden

            BeginContext(2548, 78, true);
            WriteLiteral("                <li><a href=\"#\">Quiebra <span class=\"pull-right badge bg-red\">");
            EndContext();
            BeginContext(2627, 24, false);
            Write(
#line 59 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
                                                                               Model.DescripcionQuiebra

#line default
#line hidden
            );
            EndContext();
            BeginContext(2651, 18, true);
            WriteLiteral("</span></a></li>\r\n");
            EndContext();
#line 60 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
            }

#line default
#line hidden

            BeginContext(2684, 53, true);
            WriteLiteral("        </ul>\r\n        <ul class=\"nav nav-stacked\">\r\n");
            EndContext();
#line 63 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
             if (Model.SituacionEspecial == false)
            {

#line default
#line hidden

            BeginContext(2804, 91, true);
            WriteLiteral("                <li><a href=\"#\">Situacion Especial <span class=\"pull-right badge bg-green\">");
            EndContext();
            BeginContext(2896, 34, false);
            Write(
#line 65 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
                                                                                            Model.DescripcionSituacionEspecial

#line default
#line hidden
            );
            EndContext();
            BeginContext(2930, 18, true);
            WriteLiteral("</span></a></li>\r\n");
            EndContext();
#line 66 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
            }
            else
            {

#line default
#line hidden

            BeginContext(2996, 89, true);
            WriteLiteral("                <li><a href=\"#\">Situacion Especial <span class=\"pull-right badge bg-red\">");
            EndContext();
            BeginContext(3086, 34, false);
            Write(
#line 69 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
                                                                                          Model.DescripcionSituacionEspecial

#line default
#line hidden
            );
            EndContext();
            BeginContext(3120, 18, true);
            WriteLiteral("</span></a></li>\r\n");
            EndContext();
#line 70 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
            }

#line default
#line hidden

            BeginContext(3153, 53, true);
            WriteLiteral("        </ul>\r\n        <ul class=\"nav nav-stacked\">\r\n");
            EndContext();
#line 73 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
             if (Model.Embargos == false)
            {

#line default
#line hidden

            BeginContext(3264, 81, true);
            WriteLiteral("                <li><a href=\"#\">Embargos <span class=\"pull-right badge bg-green\">");
            EndContext();
            BeginContext(3346, 25, false);
            Write(
#line 75 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
                                                                                  Model.DescripcionEmbargos

#line default
#line hidden
            );
            EndContext();
            BeginContext(3371, 18, true);
            WriteLiteral("</span></a></li>\r\n");
            EndContext();
#line 76 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
            }
            else
            {

#line default
#line hidden

            BeginContext(3437, 86, true);
            WriteLiteral("                <li><a href=\"#\">Embargos <span class=\"pull-right badge label-warning\">");
            EndContext();
            BeginContext(3524, 25, false);
            Write(
#line 79 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
                                                                                       Model.DescripcionEmbargos

#line default
#line hidden
            );
            EndContext();
            BeginContext(3549, 18, true);
            WriteLiteral("</span></a></li>\r\n");
            EndContext();
#line 80 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
            }

#line default
#line hidden

            BeginContext(3582, 53, true);
            WriteLiteral("        </ul>\r\n        <ul class=\"nav nav-stacked\">\r\n");
            EndContext();
#line 83 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
             if (Model.OtrosPrestamos == false)
            {

#line default
#line hidden

            BeginContext(3699, 88, true);
            WriteLiteral("                <li><a href=\"#\">Otros Prestamos <span class=\"pull-right badge bg-green\">");
            EndContext();
            BeginContext(3788, 31, false);
            Write(
#line 85 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
                                                                                         Model.DescripcionOtrosPrestamos

#line default
#line hidden
            );
            EndContext();
            BeginContext(3819, 18, true);
            WriteLiteral("</span></a></li>\r\n");
            EndContext();
#line 86 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
            }
            else
            {

#line default
#line hidden

            BeginContext(3885, 93, true);
            WriteLiteral("                <li><a href=\"#\">Otros Prestamos <span class=\"pull-right badge label-warning\">");
            EndContext();
            BeginContext(3979, 31, false);
            Write(
#line 89 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
                                                                                              Model.DescripcionOtrosPrestamos

#line default
#line hidden
            );
            EndContext();
            BeginContext(4010, 18, true);
            WriteLiteral("</span></a></li>\r\n");
            EndContext();
#line 90 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
            }

#line default
#line hidden

            BeginContext(4043, 53, true);
            WriteLiteral("        </ul>\r\n        <ul class=\"nav nav-stacked\">\r\n");
            EndContext();
#line 93 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
             if (Model.Disponible > 0)
            {

#line default
#line hidden

            BeginContext(4151, 83, true);
            WriteLiteral("                <li><a href=\"#\">Disponible <span class=\"pull-right badge bg-green\">");
            EndContext();
            BeginContext(4235, 27, false);
            Write(
#line 95 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
                                                                                    Model.DescripcionDisponible

#line default
#line hidden
            );
            EndContext();
            BeginContext(4262, 18, true);
            WriteLiteral("</span></a></li>\r\n");
            EndContext();
#line 96 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
            }
            else
            {

#line default
#line hidden

            BeginContext(4328, 92, true);
            WriteLiteral("                <li><a href=\"#\">Sin Disponible <span class=\"pull-right badge label-warning\">");
            EndContext();
            BeginContext(4421, 27, false);
            Write(
#line 99 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
                                                                                             Model.DescripcionDisponible

#line default
#line hidden
            );
            EndContext();
            BeginContext(4448, 18, true);
            WriteLiteral("</span></a></li>\r\n");
            EndContext();
#line 100 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
            }

#line default
#line hidden

            BeginContext(4481, 71, true);
            WriteLiteral("        </ul>\r\n    </div>\r\n    </div>\r\n</div>\r\n<div class=\"col-md-6\">\r\n");
            EndContext();
#line 106 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
     if (Model.Scoring >= 7)
    {

#line default
#line hidden

            BeginContext(4589, 469, true);
            WriteLiteral(@"        <div class=""box box-success box-solid"">
            <div class=""box-header"">
                <h1 class=""box-title text-center"" style=""font-size:80px;""> </h1>
            </div>
            <div class=""box-header text-center"">
                <h1 class=""box-title text-center"" style=""font-size:80px;"">SCORING</h1>
            </div>
            <div class=""box-body"">
                <h1 style=""color:forestgreen;font-size:140px;"" class=""text-center""><b>");
            EndContext();
            BeginContext(5059, 13, false);
            Write(
#line 116 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
                                                                                       Model.Scoring

#line default
#line hidden
            );
            EndContext();
            BeginContext(5072, 47, true);
            WriteLiteral("</b></h1>\r\n            </div>\r\n        </div>\r\n");
            EndContext();
#line 119 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
    }

#line default
#line hidden

#line 120 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
     if (Model.Scoring >= 4 && Model.Scoring <= 6)
    {

#line default
#line hidden

            BeginContext(5185, 464, true);
            WriteLiteral(@"        <div class=""box box-warning box-solid"">
            <div class=""box-header"">
                <h1 class=""box-title text-center"" style=""font-size:80px;""> </h1>
            </div>
            <div class=""box-header text-center"">
                <h1 class=""box-title text-center"" style=""font-size:80px;"">SCORING</h1>
            </div>
            <div class=""box-body"">
                <h1 style=""color:yellow;font-size:140px;"" class=""text-center""><b>");
            EndContext();
            BeginContext(5650, 13, false);
            Write(
#line 130 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
                                                                                  Model.Scoring

#line default
#line hidden
            );
            EndContext();
            BeginContext(5663, 47, true);
            WriteLiteral("</b></h1>\r\n            </div>\r\n        </div>\r\n");
            EndContext();
#line 133 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
    }

#line default
#line hidden

#line 134 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
     if (Model.Scoring <= 3)
    {

#line default
#line hidden

            BeginContext(5754, 460, true);
            WriteLiteral(@"        <div class=""box box-danger box-solid"">
            <div class=""box-header"">
                <h1 class=""box-title text-center"" style=""font-size:80px;""> </h1>
            </div>
            <div class=""box-header text-center"">
                <h1 class=""box-title text-center"" style=""font-size:80px;"">SCORING</h1>
            </div>
            <div class=""box-body"">
                <h1 style=""color:red;font-size:140px;"" class=""text-center""><b>");
            EndContext();
            BeginContext(6215, 13, false);
            Write(
#line 144 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
                                                                               Model.Scoring

#line default
#line hidden
            );
            EndContext();
            BeginContext(6228, 47, true);
            WriteLiteral("</b></h1>\r\n            </div>\r\n        </div>\r\n");
            EndContext();
#line 147 "D:\SmartClick\BussinessCore\Areas\Core\Views\Scoring\ScoringPersona.cshtml"
    }

#line default
#line hidden

            BeginContext(6282, 8, true);
            WriteLiteral("</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DAL.DTOs.ScoringDTO> Html { get; private set; }
    }
}
#pragma warning restore 1591
