using Commons.Identity.Extensions;
using Commons.Identity.Services;
using DAL.Data;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartClickCore.Interface;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using static SmartClickCore.common;

namespace SmartClickCore.Controllers
{
    public class HomeController : SmartClickCoreController
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserService<Usuario> _userManager;
        private readonly PlenarioService _plenarioService;
        private readonly IMailService _mailService;
        public HomeController(SmartClickContext context, UserService<Usuario> userManager, SignInManager<Usuario> signInManager, PlenarioService plenarioService, IMailService mailService) : base(context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _plenarioService = plenarioService;
            _mailService=mailService;
        }
        public IActionResult Index()
        {
            var prestamo = _context.Prestamos.FirstOrDefault();
            string html = "";
            html = "<br/>Estimado: " + prestamo.Cliente.Empresa.RazonSocial + "<br/><br/>";
            html += "Nos Agrada Comunicarle que ha recibido en su bandeja de Haberes 2.0 la siguiente solicitud de descuento por Decreto 14/12 segun detalle:<br/><br/>";
            html += "<b>Persona:</b> " + prestamo.Cliente.Persona.Apellido.Trim() + ", " + prestamo.Cliente.Persona.Nombres.Trim() + " DNI: " + prestamo.Cliente.Persona.NroDocumento + "<br/>";
            html += "<b>Importe Solicitado:</b> " + prestamo.Capital.ToString() + "<br/>";
            html += "<b>Cantidad de Cuotas:</b> " + prestamo.CantidadCuotas.ToString() + "<br/>";
            html += "<b>Monto de Cuota:</b> " + prestamo.MontoCuota.ToString() + "<br/><br/>";
            html += "Sin Otro Particular Saludamos a Ud. Muy Atentamente<br/><br/>";
            var mail = new MailAPI { Mail = "jorge.cutulli@iscoders.com.ar", Titulo = "Aprobación de Descuento Bot - Causante", Html = html };
            _mailService.EnviarAsync(mail);

            //ViewBag.Breadcrumb = breadcumb;
            //ActualizaPersonaUser();
            ////var establecimiento = GetEstablecimiento();
            AddPageAlerts(PageAlertType.Success, $"Bienvenido {User.Identity.Name}!");
            //int PrestamosCGE = _context.Prestamos.Count(x => x.PrestamoCGEId != 0 );
            //int OtrosOrganismos = _context.Prestamos.Count(x => x.PrestamoCGEId == 0 );
            //int Clientes = _context.Clientes.Count(x => x.FechaBaja == null);
            ////int Compras  = _context.Compras.Count();


            //ViewBag.title1 = "Prestamos CGE";
            //ViewBag.title2 = "Prestamos Otros Organismos";
            //ViewBag.title3 = "Cantidad Clientes";
            //ViewBag.title4 = "Cantidad Compras";
            //@ViewBag.Uno = PrestamosCGE.ToString();
            //@ViewBag.Dos = OtrosOrganismos.ToString();
            //@ViewBag.Tres = Clientes.ToString();
            //@ViewBag.Cuatro = "0";
            var usuario = _context.Usuarios.FirstOrDefault(x => x.UserName == User.Identity.Name);
            ViewBag.title1 = "Socios Con App";
            ViewBag.title2 = "Cantidad Prestamos Del Mes";
            ViewBag.title3 = "Total Prestado del Mes";
            ViewBag.title4 = "Cantidad Socios Nuevos del Mes";
            if (usuario.Administradores || ((ClaimsPrincipal)User).IsAdmin())
            {
                @ViewBag.Uno = _context.Clientes.Count().ToString();
                @ViewBag.Dos = _context.Prestamos.Where(x => x.FechaSolicitado >= DateTime.Today.AddDays(-30) && x.FechaAnulacion == null && x.FirmaOlografica != null  ).Count().ToString();
                @ViewBag.Tres = _context.Prestamos.Where(x => x.FechaSolicitado >= DateTime.Today.AddDays(-30) && x.FechaAnulacion == null && x.FirmaOlografica != null && x.EstadoActual.Id != 4 && x.EstadoActual.Id != 5 && x.EstadoActual.Id != 9 && x.EstadoActual.Id != 11 && x.EstadoActual.Id != 13 && x.EstadoActual.Id != 15 && x.EstadoActual.Id != 16).Sum(x => x.Capital).ToString("N", CultureInfo.CreateSpecificCulture("es-ES"));
                @ViewBag.Cuatro = _context.Clientes.Where(x => x.FechaIngreso.Date >= DateTime.Today.AddDays(-30).Date).Count();
            }
            else 
            {
                @ViewBag.Uno = "0";
                @ViewBag.Dos = "0";
                @ViewBag.Tres = "0";
                @ViewBag.Cuatro = "0";
            } 
            return View();

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new DAL.Models.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        

    }
}