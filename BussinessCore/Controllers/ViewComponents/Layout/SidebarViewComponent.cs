﻿using Castle.Core.Internal;
using Commons.Extensions;
using Commons.Helpers;
using Commons.Identity.Extensions;
using Commons.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using DAL.Data;

namespace SmartClickCore.Controllers.ViewComponents.Layout
{
    public class SidebarViewComponent : ViewComponent
    {
        //internal ApiRecursosComunes _api;
        private readonly SmartClickContext _context;

        public SidebarViewComponent(SmartClickContext context)
        {
            //_api = new ApiRecursosComunes();
            _context = context;
        }
        public IViewComponentResult Invoke(string filter)
        {

            bool userIsAdmin = ((ClaimsPrincipal)User).IsAdmin();
            string selectedUnidadid = ((ClaimsPrincipal)User).GetWorkSpaceId();
            //var currentUser = _context.Users.FirstOrDefault(x => x.UserName == HttpContext.User.GetUserName());
            System.Security.Principal.IPrincipal currentUser = this.User;
            var sidebars = new List<SidebarMenu>();
            sidebars.Add(MenuHelpers.AddHeader("Menú Principal"));
            var datos = MenuHelpers.AddTree("Datos", "fa fa-database");
            var generales = MenuHelpers.AddTree("Generales", "fa fa-circle-o text-green");
            var especificos = MenuHelpers.AddTree("Específicos", "fa fa-circle-o text-green");
            var gestion = MenuHelpers.AddTree("Gestión", "fa fa-cubes");
            var reportes = MenuHelpers.AddTree("Reportes", "fa fa-print text-white");
            var sesion = MenuHelpers.AddTree("Sesión", "fa fa-key");
            var sistema = MenuHelpers.AddTree("Sistema", "fa fa-gear");

            var rolesusuario = _context.AspNetFunctions.Where(x => x.Name == "gestionprestamos").FirstOrDefault();
            if (userIsAdmin == true)
            {
                generales.TreeChild = new List<SidebarMenu>()
                {
                    MenuHelpers.AddModule("Tipos de Documento", "/Core/TiposDocumentos/"),
                    MenuHelpers.AddModule("Tipos de Personas", "/Core/TiposPersonas/"),
                    MenuHelpers.AddModule("Estados Civiles", "/Core/EstadosCiviles/"),
                    MenuHelpers.AddModule("Estados Prestamos", "/Core/EstadosPrestamos/"),
                    MenuHelpers.AddModule("Destinos Fondos", "/Core/DestinosFondos/"),
                    MenuHelpers.AddModule("Monedas", "/Core/Monedas/"),
                    MenuHelpers.AddModule("Paises", "/Core/Paises/"),
                    MenuHelpers.AddModule("Provincias", "/Core/Provincias/"),
                    MenuHelpers.AddModule("Localidad", "/Core/Localidad/"),
                    MenuHelpers.AddModule("Sistemas de Financiación", "/Core/SistemasFinanciacion/"),
                    MenuHelpers.AddModule("Tipos de Clientes", "/Core/TiposClientes/"),
                    MenuHelpers.AddModule("Tipos de Movimientos", "/Core/TiposMovimientos/"),
                    MenuHelpers.AddModule("Grupos", "/Core/Grupos/"),
                    MenuHelpers.AddModule("Empresas", "/Core/Empresas/"),
                    MenuHelpers.AddModule("Formas de Pago", "/Core/FormasPago/"),
                    MenuHelpers.AddModule("Lineas de Prestamos", "/Core/LineasPrestamos/"),
                    MenuHelpers.AddModule("Conceptos", "/Core/Conceptos/"),
                    MenuHelpers.AddModule("Cuentas Corrientes", "/Core/CuentasCorrientes/"),
                    MenuHelpers.AddModule("Tipos Movimientos Billetera", "/Core/TipoMovimientoBilletera/"),
                    MenuHelpers.AddModule("Movimientos de Billetera", "/Core/MovimientoBilletera/"),
                    MenuHelpers.AddModule("Billetera", "/Core/Billetera/"),
                    MenuHelpers.AddModule("Organismos", "/Core/Organismo/"),
                    MenuHelpers.AddModule("Inversores", "/Core/Inversores/")
                };
                datos.TreeChild.Add(generales);
                especificos.TreeChild = new List<SidebarMenu>()
                {
                    MenuHelpers.AddModule("Matriz - Probabilidades", "/Core/Probabilidades/"),
                    MenuHelpers.AddModule("Matriz - Consecuencias", "/Core/Consecuencias/"),
                    MenuHelpers.AddModule("Proveedor - Rubros", "/Core/Rubro/")                    
                };
                datos.TreeChild.Add(especificos);

                gestion.TreeChild = new List<SidebarMenu>()
                {
                    MenuHelpers.AddModule("Clientes", "/Core/Clientes"),
                    MenuHelpers.AddModule("Vendedores", "/Core/Vendedores/"),
                    MenuHelpers.AddModule("Matriz Riesgo", "/Core/MatrizRiesgo/"),
                    MenuHelpers.AddModule("Novedades", "/Core/Novedades"),
                    MenuHelpers.AddModule("Scoring", "/Core/Scoring"),
                    MenuHelpers.AddModule("Campañas", "/Core/Campanas"),
                    MenuHelpers.AddModule("Proveedor", "/Core/Proveedor"),
                   // MenuHelpers.AddModule("Prestamos", "/Core/Prestamos"),
                    MenuHelpers.AddModule("Bandeja De Aprobación", "/Core/BandejaDeAprobacion/"),
                    MenuHelpers.AddModule("Bandeja De Ventas", "/Core/BandejaDeVentas/")
                };
                reportes.TreeChild = new List<SidebarMenu>()
                {
                    MenuHelpers.AddModule("Prestamos por App SmartClick", "/Core/PrestamosApp/"),
                    MenuHelpers.AddModule("Prestamos Aprobados", "/Core/PrestamosAprobados/"),
                    MenuHelpers.AddModule("Solicitudes Sin Disponible", "/Core/CreditoSinDisponible/")
                };
                sesion.TreeChild = new List<SidebarMenu>()
                {
                    // Sesión: Logout
                    MenuHelpers.AddModule("Cierra Sesión", "javascript:document.getElementById('logoutForm').submit()", "fa fa-sign-out"),
                };
                sistema.TreeChild = new List<SidebarMenu>()
                {
                    MenuHelpers.AddModule("Usuarios", "/Administracion/Usuarios", "fa fa-circle-o text-green"),
                    MenuHelpers.AddModule("Funciones", "/SecurityFunctions", "fa fa-circle-o text-green"),
                    MenuHelpers.AddModule("Rol", "/SecurityRoles", "fa fa-circle-o text-green"),
                };
                //sidebars.Add(unidades);
                sidebars.Add(datos);
                sidebars.Add(gestion);
                sidebars.Add(reportes);
                sidebars.Add(sistema);
                sidebars.Add(sesion);
            }
            else
            {
                datos.TreeChild = new List<SidebarMenu>();


                if (HttpContext.UserHasRoute("/Core/TiposDocumentos/Index"))
                    generales.TreeChild.Add(MenuHelpers.AddModule("Tipos de Documento", "/Core/TiposDocumentos/"));

                if (HttpContext.UserHasRoute("/Core/TiposPersonas/Index"))
                    generales.TreeChild.Add(MenuHelpers.AddModule("Tipos de Personas", "/Core/TiposPersonas/"));


                if (HttpContext.UserHasRoute("/Core/EstadosCiviles/Index"))
                    generales.TreeChild.Add(MenuHelpers.AddModule("Estados Civiles", "/Core/EstadosCiviles/"));


                if (HttpContext.UserHasRoute("/Core/EstadosPrestamos/Index"))
                    generales.TreeChild.Add(MenuHelpers.AddModule("Estados Prestamos", "/Core/EstadosPrestamos/"));


                if (HttpContext.UserHasRoute("/Core/DestinosFondos/Index"))
                    generales.TreeChild.Add(MenuHelpers.AddModule("Destinos Fondos", "/Core/DestinosFondos"));


                if (HttpContext.UserHasRoute("/Core/Monedas/Index"))
                    generales.TreeChild.Add(MenuHelpers.AddModule("Monedas", "/Core/Monedas"));

                if (HttpContext.UserHasRoute("/Core/Paises/Index"))
                    generales.TreeChild.Add(MenuHelpers.AddModule("Paises", "/Core/Paises"));

                if (HttpContext.UserHasRoute("/Core/Provincias/Index"))
                    generales.TreeChild.Add(MenuHelpers.AddModule("Provincias", "/Core/Provincias/"));

                if (HttpContext.UserHasRoute("/Core/Localidad/Index"))
                    generales.TreeChild.Add(MenuHelpers.AddModule("Localidad", "/Core/Localidad/"));

                if (HttpContext.UserHasRoute("/Core/SistemasFinanciacion/Index"))
                    generales.TreeChild.Add(MenuHelpers.AddModule("Sistemas de Financiación", "/Core/SistemasFinanciacion/"));

                if (HttpContext.UserHasRoute("/Core/TiposClientes/Index"))
                    generales.TreeChild.Add(MenuHelpers.AddModule("Tipos de Clientes", "/Core/TiposClientes/"));

                if (HttpContext.UserHasRoute("/Core/TiposMovimientos/Index"))
                    generales.TreeChild.Add(MenuHelpers.AddModule("Tipos de Movimientos", "/Core/TiposMovimientos/"));

                if (HttpContext.UserHasRoute("/Core/Grupos/Index"))
                    generales.TreeChild.Add(MenuHelpers.AddModule("Grupos", "/Core/Grupos/"));

                if (HttpContext.UserHasRoute("/Core/Empresas/Index"))
                    generales.TreeChild.Add(MenuHelpers.AddModule("Empresas", "/Core/Empresas/"));

                if (HttpContext.UserHasRoute("/Core/FormasPago/Index"))
                    generales.TreeChild.Add(MenuHelpers.AddModule("Formas de Pago", "/Core/FormasPago/"));

                if (HttpContext.UserHasRoute("/Core/LineasPrestamos/Index"))
                    generales.TreeChild.Add(MenuHelpers.AddModule("Lineas de Prestamos", "/Core/LineasPrestamos/"));

                if (HttpContext.UserHasRoute("/Core/Conceptos/Index"))
                    generales.TreeChild.Add(MenuHelpers.AddModule("Conceptos", "/Core/Conceptos/"));

                if (HttpContext.UserHasRoute("/Core/CuentasCorrientes/Index"))
                    generales.TreeChild.Add(MenuHelpers.AddModule("Cuentas Corrientes", "/Core/CuentasCorrientes/"));

                if (HttpContext.UserHasRoute("/Core/TipoMovimientoBilletera/Index"))
                    generales.TreeChild.Add(MenuHelpers.AddModule("Tipos Movimientos Billetera", "/Core/TipoMovimientoBilletera/"));

                if (HttpContext.UserHasRoute("/Core/MovimientoBilletera/Index"))
                    generales.TreeChild.Add(MenuHelpers.AddModule("Movimientos de Billetera", "/Core/MovimientoBilletera/"));

                if (HttpContext.UserHasRoute("/Core/Billetera/Index"))
                    generales.TreeChild.Add(MenuHelpers.AddModule("Billetera", "/Core/Billetera/"));

                if (HttpContext.UserHasRoute("/Core/Organismo/Index"))
                    generales.TreeChild.Add(MenuHelpers.AddModule("Organismos", "/Core/Organismo/"));
                
                if (HttpContext.UserHasRoute("/Core/Inversores/Index"))
                    generales.TreeChild.Add(MenuHelpers.AddModule("Inversores", "/Core/Inversores/"));
               

                if (generales.TreeChild.Count != 0)
                    datos.TreeChild.Add(generales);

                //////////////////////////////////////////////
                ///////////       Especificos         ////////////
                //////////////////////////////////////////////
                #region ESPECIFICOS

                //ESPECIFICOS:

                especificos.TreeChild = new List<SidebarMenu>();

                if (HttpContext.UserHasRoute("/Core/Probabilidades/Index"))
                    especificos.TreeChild.Add(MenuHelpers.AddModule("Matriz - Probabilidades", "/Core/Probabilidades/"));

                if (HttpContext.UserHasRoute("/Core/Consecuencias/Index"))
                    especificos.TreeChild.Add(MenuHelpers.AddModule("Matriz - Consecuencias", "/Core/Consecuencias/"));

                if (HttpContext.UserHasRoute("/Core/Rubro/Index"))
                    especificos.TreeChild.Add(MenuHelpers.AddModule("Proveedor - Rubros", "/Core/Rubro/"));


                if (especificos.TreeChild.Count != 0)
                    datos.TreeChild.Add(especificos);


                if (datos.TreeChild.Count != 0)
                    sidebars.Add(datos);
                #endregion



                //////////////////////////////////////////////
                ///////////       Gestion         ////////////
                //////////////////////////////////////////////
                #region GESTION

                //GESTIÓN: especificos

                gestion.TreeChild = new List<SidebarMenu>();

                if (HttpContext.UserHasRoute("/Core/Clientes/Index"))
                    gestion.TreeChild.Add(MenuHelpers.AddModule("Clientes", "/Core/Clientes"));

                if (HttpContext.UserHasRoute("/Core/Vendedores/Index"))
                    gestion.TreeChild.Add(MenuHelpers.AddModule("Vendedores", "/Core/Vendedores/"));

                if (HttpContext.UserHasRoute("/Core/MatrizRiesgo/Index"))
                    gestion.TreeChild.Add(MenuHelpers.AddModule("Matriz Riesgo", "/Core/MatrizRiesgo/"));

                if (HttpContext.UserHasRoute("/Core/Novedades/Index"))
                    gestion.TreeChild.Add(MenuHelpers.AddModule("Novedades", "/Core/Novedades"));

                if (HttpContext.UserHasRoute("/Core/Scoring/Index"))
                    gestion.TreeChild.Add(MenuHelpers.AddModule("Scoring", "/Core/Scoring"));

                if (HttpContext.UserHasRoute("/Core/Campanas/Index"))
                    gestion.TreeChild.Add(MenuHelpers.AddModule("Campañas", "/Core/Campanas"));

                if (HttpContext.UserHasRoute("/Core/Proveedor/Index"))
                    gestion.TreeChild.Add(MenuHelpers.AddModule("Proveedor", "/Core/Proveedor"));

                //if (HttpContext.UserHasRoute("/Core/Prestamos/Index"))
                //    gestion.TreeChild.Add(MenuHelpers.AddModule("Prestamos", "/Core/Prestamos"));


                if (HttpContext.UserHasRoute("/Core/BandejaDeAprobacion/Index"))
                    gestion.TreeChild.Add(MenuHelpers.AddModule("Bandeja De Aprobación", "/Core/BandejaDeAprobacion"));
                
                if (HttpContext.UserHasRoute("/Core/BandejaDeVentas/Index"))
                    gestion.TreeChild.Add(MenuHelpers.AddModule("Bandeja De Ventas", "/Core/BandejaDeVentas"));

                if (gestion.TreeChild.Count != 0)
                    sidebars.Add(gestion);

                #endregion


                //////////////////////////////////////////////
                ///////////         REPORTES       ////////////
                //////////////////////////////////////////////
                #region REPORTES

                reportes.TreeChild = new List<SidebarMenu>();

                if (HttpContext.UserHasRoute("/Core/PrestamosApp/Index"))
                    reportes.TreeChild.Add(MenuHelpers.AddModule("Prestamos por App SmartClick", "/Core/PrestamosApp/"));
                if (HttpContext.UserHasRoute("/Core/PrestamosAprobados/Index"))
                    reportes.TreeChild.Add(MenuHelpers.AddModule("Prestamos Aprobados", "/Core/PrestamosAprobados/"));

                if (HttpContext.UserHasRoute("/Core/CreditoSinDisponible/Index"))
                    reportes.TreeChild.Add(MenuHelpers.AddModule("Solicitudes Sin Disponible", "/Core/CreditoSinDisponible/"));


                if (reportes.TreeChild.Count != 0)
                    sidebars.Add(reportes);

                #endregion


                //////////////////////////////////////////////
                ///////////        SISTEMA        ////////////
                //////////////////////////////////////////////
                #region SISTEMA
                                   
                sistema.TreeChild = new List<SidebarMenu>();
                // Sistema: Usuarios
                if (HttpContext.UserHasRoute("/Administracion/Usuarios/Index"))
                    sistema.TreeChild.Add(MenuHelpers.AddModule("Usuarios", "/Administracion/Usuarios/", "fa fa-users text-red", new Tuple<int, int, int>(0, 0, 0)));
                //Sistema: Funciones
                if (HttpContext.UserHasRoute("/Securityfunctions/Index"))
                    sistema.TreeChild.Add(MenuHelpers.AddModule("Funciones", "/SecurityFunctions", "fa fa-key text-red", new Tuple<int, int, int>(0, 0, 0)));
                // Sistema: SecurityRoles
                if (HttpContext.UserHasRoute("/SecurityRoles/Index"))
                    sistema.TreeChild.Add(MenuHelpers.AddModule("Roles", "/SecurityRoles", "fa fa-key text-red", new Tuple<int, int, int>(0, 0, 0)));

                if (!sistema.TreeChild.IsNullOrEmpty()) sidebars.Add(sistema);
                #endregion
                //////////////////////////////////////////////
                ///////////        SESION         ////////////
                //////////////////////////////////////////////
                #region SESION

                sesion.TreeChild = new List<SidebarMenu>();

                // Sesión: Logout
                sesion.TreeChild.Add(MenuHelpers.AddModule("Cierra Sesion", "javascript:document.getElementById('logoutForm').submit()", "fa fa-sign-out"));

                sidebars.Add(sesion);
                #endregion
            }
            return View("LayoutSidebar", sidebars);
        }
    }
}