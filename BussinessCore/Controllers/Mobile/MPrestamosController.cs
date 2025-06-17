using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.AspNetCore.Cors;
using DAL.Data;
using DAL.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Commons.Identity.Services;
using Commons.Controllers;
using SmartClickCore;
using System.Net.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Infrastructure;
using Microsoft.Win32;
using System.ComponentModel.DataAnnotations;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Numeric;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MimeKit.Encodings;

namespace SmartClick.Controllers
{
    [Route("api/[controller]")]
    public class MPrestamosController : BaseController
    {
        private readonly UserService<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        public SmartClickContext _context;
        public MPrestamosController(SmartClickContext context, UserService<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("TraePrestamos")]
        [EnableCors("CorsPolicy")]
        [AllowAnonymous]
        public MTraePrestamosDTO TraePrestamos([FromBody] MTraePrestamosDTO uat)
        {
            var Uat = _context.UAT.FirstOrDefault(x => x.Token == uat.UAT);
            if (Uat == null)
            {
                uat.Status = 500;
                uat.Mensaje = "UAT Invalida";
                return uat;
            }
            uat.Status = 200;
            uat.Mensaje = "Prestamos Ok ";
            var Prestamos = _context.Prestamos.Where(x => x.Cliente.Id == Uat.Cliente.Id).OrderByDescending(x => x.FechaSolicitado);
            List<PrestamoDTO> lista = new List<PrestamoDTO>();
            foreach (var prestamo in Prestamos)
            {
                if (Uat.Cliente.Persona.TipoPersona.Organismo.APIEjercito == true)
                {
                    MEstadoPrestamoDTO consulta = new MEstadoPrestamoDTO();
                    consulta.UAT = LoginCGE(prestamo.Cliente.Empresa);
                    using (var client = new HttpClient())
                    {
                        consulta.PrestamoCGEId = prestamo.PrestamoCGEId;
                        client.BaseAddress = new Uri("https://www.cge.mil.ar:81/api/mentidades/");
                        HttpResponseMessage response = client.PostAsJsonAsync("ConsultaEstadoPrestamo", consulta).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var readTask = response.Content.ReadAsAsync<MEstadoPrestamoDTO>();
                            readTask.Wait();
                            consulta = readTask.Result;
                            if (consulta.Status == 200)
                            {
                                var estado = _context.EstadosPrestamos.FirstOrDefault(x => x.EstadoCGEId == consulta.EstadoId);
                                if (estado != null)
                                {
                                    prestamo.EstadoActual = estado;
                                }
                                prestamo.FechaAnulacion = consulta.FechaAnulado;

                            }
                        }
                    }
                }
                var renglon = new PrestamoDTO();
                var renglones = _context.CuentasCorrientes.Where(x => x.Prestamo.Id == prestamo.Id);
                renglon.Id = prestamo.Id;
                renglon.CuotasRestantes = renglones.Count(x => x.TipoMovimiento.Debito == true && x.Saldo > 0);
                renglon.Estado = prestamo.EstadoActual.Nombre;
                var saldo = renglones.OrderByDescending(x => x.Fecha).FirstOrDefault(x => x.TipoMovimiento.Debito == true && x.Saldo > 0);
                if (saldo != null)
                {
                    renglon.Saldo = saldo.Saldo;
                }
                else
                {
                    renglon.Saldo = prestamo.Capital;
                }
                renglon.Fecha = Convert.ToDateTime(prestamo.FechaSolicitado);
                renglon.MontoPrestado = prestamo.Capital;
                renglon.CantidadCuotas = prestamo.CantidadCuotas;
                renglon.MontoCuota = prestamo.MontoCuota;
                renglon.MontoCuotaAmpliado = prestamo.MontoCuotaAmpliacion;
                renglon.Ampliado = prestamo.Ampliado;
                renglon.Producto = prestamo.Linea.Nombre;
                renglon.Color = prestamo.EstadoActual.Color;
                renglon.Anulable = prestamo.EstadoActual.AnulablePersona;
                renglon.Confirmable = prestamo.EstadoActual.ConfirmablePersona;
                renglon.Transferido = prestamo.EstadoActual.Transferido;
                renglon.CFT = prestamo.CFT;
                lista.Add(renglon);
                _context.Prestamos.Update(prestamo);

            }
            _context.SaveChanges();
            uat.Prestamos = lista;
            return uat;
        }
        [HttpPost]
        [Route("TraePrestamosRenglones")]
        [EnableCors("CorsPolicy")]
        [AllowAnonymous]
        public MTraePrestamosRenglonesDTO TraePrestamosRenglones([FromBody] MTraePrestamosRenglonesDTO uat)
        {
            var Uat = _context.UAT.FirstOrDefault(x => x.Token == uat.UAT);
            if (uat == null)
            {
                uat.Status = 500;
                uat.Mensaje = "UAT Invalida";
                return uat;
            }
            uat.Status = 200;
            uat.Mensaje = "Renglones Ok";
            var prestamo = _context.Prestamos.Find(uat.PrestamoId);
            //SmartClickCore.SmartClick.ActualizaRenglones(prestamo, prestamo.PrestamoNumero, _context);
            var Renglones = _context.CuentasCorrientes.Where(x => x.Prestamo.Id == uat.PrestamoId);
            List<PrestamoRenglonDTO> lista = new List<PrestamoRenglonDTO>();
            decimal saldo = 0;
            foreach (var renglon in Renglones)
            {
                saldo = saldo + renglon.Credito - renglon.Debito;
                var nuevo = new PrestamoRenglonDTO();
                nuevo.Concepto = renglon.TipoMovimiento.Nombre;
                nuevo.Credito = renglon.Credito;
                nuevo.Debito = renglon.Debito;
                nuevo.Saldo = saldo;
                nuevo.Fecha = Convert.ToDateTime(renglon.Fecha);
                nuevo.Id = renglon.Id;
                lista.Add(nuevo);
            }
            uat.Renglones = lista;
            return uat;
        }
        [HttpPost]
        [Route("TraeLineasPrestamos")]
        [EnableCors("CorsPolicy")]
        [AllowAnonymous]
        public MTraeLineasPrestamosDTO TraeLineasPrestamos([FromBody] MTraeLineasPrestamosDTO uat)
        {
            var Uat = _context.UAT.FirstOrDefault(x => x.Token == uat.UAT);
            if (uat == null)
            {
                uat.Status = 500;
                uat.Mensaje = "UAT Invalida";
                return uat;
            }
            uat.Status = 200;
            uat.Mensaje = "Lineas Ok";
            if (Uat.Cliente.Persona.TipoPersona.Organismo.Id == 1)
            {
                MTraeDsiponibleCGEDTO disponible = new MTraeDsiponibleCGEDTO();
                disponible.UAT = LoginCGE(Uat.Cliente.Empresa);
                disponible.DNI = Convert.ToInt32(Uat.Cliente.Persona.NroDocumento);
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://www.cge.mil.ar:81/api/mentidades/");
                    HttpResponseMessage response = client.PostAsJsonAsync("TraeDisponible", disponible).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var readTask = response.Content.ReadAsAsync<MTraeDsiponibleCGEDTO>();
                        readTask.Wait();
                        disponible = readTask.Result;
                    }
                    else
                    {
                        return uat;
                    }
                }
                if (Uat.Cliente.Persona.NroDocumento == "26625527")
                {
                    disponible.Disponible = 25000;

                }
                if (disponible.Disponible <= 0 && uat.Ampliado == false)
                {
                    uat.Status = 500;
                    uat.Mensaje = "Sin Disponible";
                    return uat;
                }

                uat.Disponible = disponible.Disponible;
                if (uat.Ampliado)
                {
                    disponible.Disponible += Uat.Cliente.Persona.TipoPersona.MontoAmpliacion;
                }

                if (Uat.Cliente.BloquearPrestamos)
                {
                    uat.Status = 500;
                    uat.Mensaje = "Sin Disponible por Entidad";
                    return uat;
                }

                var Lineas = _context.LineasPrestamosTiposPersonas.Where(x => x.TipoPersona.Id == Uat.Cliente.Persona.TipoPersona.Id && x.LineaPrestamo.FechaBaja == null).OrderBy(x => x.LineaPrestamo.Nombre);
                List<LineasPrestamoDTO> lista = new List<LineasPrestamoDTO>();
                foreach (var linea in Lineas)
                {
                    var nuevo = new LineasPrestamoDTO();
                    nuevo.Nombre = linea.LineaPrestamo.Nombre;
                    if (uat.Ampliado)
                    {
                        var minimo = _context.LineasPrestamosPlanes.Where(x => x.MontoCuota <= (disponible.Disponible - x.MargenDisponible) && x.Linea.Id == linea.LineaPrestamo.Id && (x.MontoPrestado / x.Linea.Intervalo == Math.Round(x.MontoPrestado / x.Linea.Intervalo)) && x.Activo == true).OrderBy(x => x.MontoPrestado).FirstOrDefault();
                        nuevo.MontoMinimo = minimo.MontoPrestado;
                    }
                    else
                    {
                        if (disponible.Disponible >= linea.LineaPrestamo.CuotaMinima)
                        {
                            nuevo.MontoMinimo = linea.LineaPrestamo.CapitalMinimo;
                        }
                        else
                        {
                            uat.Status = 500;
                            uat.Mensaje = "Sin Disponible";
                            return uat;
                        }
                    }

                    nuevo.Intervalo = linea.LineaPrestamo.Intervalo;
                    var limiteCuota = Uat.Cliente.Persona.TipoPersona.LimiteCuotas;
                    if (uat.Ampliado)
                    {
                        limiteCuota = Uat.Cliente.Persona.TipoPersona.TopeCantCuotasAmpliacion;
                    }

                    var maximo = _context.LineasPrestamosPlanes.Where(x => x.MontoCuota <= (disponible.Disponible - x.MargenDisponible) && x.Linea.Id == linea.LineaPrestamo.Id && (x.MontoPrestado / x.Linea.Intervalo == Math.Round(x.MontoPrestado / x.Linea.Intervalo)) && x.MontoPrestado <= Uat.Cliente.Persona.TipoPersona.TopePrestamo && x.CantidadCuotas <= limiteCuota && x.Activo == true).OrderByDescending(x => x.MontoPrestado).FirstOrDefault();
                    if (maximo != null)
                    {
                        nuevo.MontoMaximo = maximo.MontoPrestado;
                        nuevo.Id = linea.LineaPrestamo.Id;
                        lista.Add(nuevo);
                    }
                }
                uat.MontoMaximoAmpliado = Uat.Cliente.Persona.TipoPersona.MontoAmpliacion;
                uat.LineasPrestamos = lista;
            }
            else
            {
                if (uat.Ampliado)
                {
                    uat.Disponible += Uat.Cliente.Persona.TipoPersona.MontoAmpliacion;
                }

                if (Uat.Cliente.BloquearPrestamos)
                {
                    uat.Status = 500;
                    uat.Mensaje = "Sin Disponible por Entidad";
                    return uat;
                }
                var Lineas = _context.LineasPrestamosTiposPersonas.Where(x => x.TipoPersona.Id == Uat.Cliente.Persona.TipoPersona.Id && x.LineaPrestamo.FechaBaja == null).OrderBy(x => x.LineaPrestamo.Nombre);
                List<LineasPrestamoDTO> lista = new List<LineasPrestamoDTO>();
                foreach (var linea in Lineas)
                {
                    var nuevo = new LineasPrestamoDTO();
                    nuevo.Nombre = linea.LineaPrestamo.Nombre;
                    if (uat.Ampliado)
                    {
                        var minimo = _context.LineasPrestamosPlanes.Where(x => x.MontoCuota <= (uat.Disponible - x.MargenDisponible) && x.Linea.Id == linea.LineaPrestamo.Id && (x.MontoPrestado / x.Linea.Intervalo == Math.Round(x.MontoPrestado / x.Linea.Intervalo)) && x.Activo == true).OrderBy(x => x.MontoPrestado).FirstOrDefault();
                        nuevo.MontoMinimo = minimo.MontoPrestado;
                    }
                    else
                    {
                        if (Uat.Cliente.MontoMensualDisponible >= linea.LineaPrestamo.CuotaMinima)
                        {
                            nuevo.MontoMinimo = linea.LineaPrestamo.CapitalMinimo;
                        }
                        else
                        {
                            uat.Status = 500;
                            uat.Mensaje = "Sin Disponible";
                            return uat;
                        }
                    }

                    nuevo.Intervalo = linea.LineaPrestamo.Intervalo;
                    var limiteCuota = Uat.Cliente.Persona.TipoPersona.LimiteCuotas;
                    if (uat.Ampliado)
                    {
                        limiteCuota = Uat.Cliente.Persona.TipoPersona.TopeCantCuotasAmpliacion;
                    }

                    var maximo = _context.LineasPrestamosPlanes.Where(x => x.MontoCuota <= (Uat.Cliente.MontoMensualDisponible - x.MargenDisponible) && x.Linea.Id == linea.LineaPrestamo.Id && (x.MontoPrestado / x.Linea.Intervalo == Math.Round(x.MontoPrestado / x.Linea.Intervalo)) && x.MontoPrestado <= Uat.Cliente.Persona.TipoPersona.TopePrestamo && x.CantidadCuotas <= limiteCuota && x.Activo == true).OrderByDescending(x => x.MontoPrestado).FirstOrDefault();
                    if (maximo != null)
                    {
                        nuevo.MontoMaximo = maximo.MontoPrestado;
                        nuevo.Id = linea.LineaPrestamo.Id;
                        lista.Add(nuevo);
                    }
                }
                uat.MontoMaximoAmpliado = Uat.Cliente.Persona.TipoPersona.MontoAmpliacion;
                uat.LineasPrestamos = lista;
            }
            return uat;
        }
        [HttpPost]
        [Route("SolicitaPrestamo")]
        [EnableCors("CorsPolicy")]
        [AllowAnonymous]
        public MSolicitaPrestamoDTO SolicitaPrestamo([FromBody] MSolicitaPrestamoDTO uat)
        {
            if (uat == null)
            {
                uat.Status = 500;
                uat.Mensaje = "uat Invalida";
                return uat;
            }
            var Uat = _context.UAT.FirstOrDefault(x => x.Token == uat.UAT);
            if (Uat == null)
            {
                uat.Status = 500;
                uat.Mensaje = "UAT Invalida";
                return uat;
            }
            // var verificarPrestamo = _context.Prestamos.FirstOrDefault(x => x.Cliente == Uat.Cliente && (x.EstadoActual.Id == 1 || x.EstadoActual.Id == 9));
            // if (verificarPrestamo != null)
            // {
            //    uat.Status = 500;
            //     uat.Mensaje = "Ya tiene un prestamo Solicitado o rechazado";
            //     return uat;
            // }
            var verificarPrestamo = _context.Prestamos.FirstOrDefault(x => x.Cliente == Uat.Cliente && (x.EstadoActual.Id == 1 ));
            if (verificarPrestamo != null)
            {
                uat.Status = 500;
                uat.Mensaje = "Ya tiene un prestamo Solicitado";
                return uat;
            }

            var tipopersona = _context.TiposPersonas.FirstOrDefault(x => x.Id == uat.TipoPersonaId);

            var linea = _context.LineasPrestamos.Find(uat.LineaPrestamoId);
            if (linea == null)
            {
                uat.Status = 500;
                uat.Mensaje = "Linea de Prestamo Invalida";
                return uat;
            }
            if (uat.FirmaOlografica == null)
            {
                uat.Status = 500;
                uat.Mensaje = "Debe Subir Firma Olografica";
                return uat;
            }
            if (uat.FotoSosteniendoDNI == null)
            {
                uat.Status = 500;
                uat.Mensaje = "Debe Subir Foto Sosteniendo DNI";
                return uat;
            }
            if (uat.FotoDNIAnverso == null)
            {
                uat.Status = 500;
                uat.Mensaje = "Debe Subir DNI Anverso";
                return uat;
            }
            if (uat.FotoDNIReverso == null)
            {
                uat.Status = 500;
                uat.Mensaje = "Debe Subir DNI Reverso";
                return uat;
            }
            if (tipopersona.Organismo.Id != 1)
            {
                if (uat.FotoReciboHaber == null)
                {
                    uat.Status = 500;
                    uat.Mensaje = "Debe Subir Foto Recibo Haber";
                    return uat;
                }
                if (uat.ConstanciaCBU == null)
                {
                    uat.Status = 500;
                    uat.Mensaje = "Debe Subir Foto Constancia CBU";
                    return uat;
                }
                if (uat.FotoCertificadoDescuento == null)
                {
                    uat.Status = 500;
                    uat.Mensaje = "Debe Subir Certificado Descuento";
                    return uat;
                }
                if (RegitrarPrestamoOtroOrganismo(uat, Uat, linea) == true)
                {
                    uat.Status = 200;
                    uat.Mensaje = "Solicitud Realizada Correctamente!!!";
                    return uat;
                }
                else
                {
                    uat.Status = 500;
                    uat.Mensaje = "Error al registrar el Prestamo!";
                    return uat;
                }
            }
            uat.Status = 200;
            uat.Mensaje = "Solicitud Realizada Correctamente!!!";
            using (var client = new HttpClient())
            {

                Prestamos prestamo = new Prestamos();
                prestamo.FechaSolicitado = DateTime.Now;
                prestamo.Cliente = Uat.Cliente;
                prestamo.Linea = linea;
                prestamo.EstadoActual = _context.EstadosPrestamos.Find(1);
                prestamo.FirmaOlografica = uat.FirmaOlografica;
                prestamo.Cliente.FotoDNIAnverso = uat.FotoDNIAnverso;
                prestamo.Cliente.FotoDNIReverso = uat.FotoDNIReverso;
                prestamo.ConstanciaCBU = uat.ConstanciaCBU;
                prestamo.CertificadoDescuento = uat.FotoCertificadoDescuento;
                prestamo.Cliente.FotoSosteniendoDNI = uat.FotoSosteniendoDNI;
                prestamo.CantidadCuotas = uat.CantidadCuotas;
                prestamo.MontoMensualDisponible = Uat.Cliente.MontoMensualDisponible;
                prestamo.Capital = uat.ImporteSolicitado;
                prestamo.CFT = _context.LineasPrestamosPlanes.FirstOrDefault(x => x.Linea.Id == linea.Id && x.Activo == true).CFT;
                prestamo.TEMAmprom = _context.LineasPrestamosPlanes.FirstOrDefault(x => x.Linea.Id == linea.Id && x.Activo == true).TEM;
                prestamo.TNAAmprom = _context.LineasPrestamosPlanes.FirstOrDefault(x => x.Linea.Id == linea.Id && x.Activo == true).TNA;
                prestamo.CapitalAmprom = _context.LineasPrestamosPlanes.FirstOrDefault(x => x.Linea.Id == linea.Id && x.Activo == true && x.MontoPrestado == uat.ImporteSolicitado).CapitalSmartClick;
                prestamo.CBU = uat.CBU;
               
                if (uat.Ampliado)
                {
                    prestamo.Ampliado = true;
                    prestamo.MontoCuotaAmpliacion = uat.MontoCuota - uat.Disponible;
                }
                else
                {
                    prestamo.Ampliado = false;
                    prestamo.MontoCuotaAmpliacion = 0;
                }
                prestamo.MontoCuota = uat.MontoCuota;
                prestamo.PrestamoCGEId = 1; // Valor predeterminado para Prestamos que fallo la respuesta de la ApiCGE
                _context.Prestamos.Add(prestamo);
                _context.SaveChanges();
                if (tipopersona.Organismo.Id == 1)
                {
                    MSolicitaPrestamoCGEDTO solicitud = new MSolicitaPrestamoCGEDTO();
                    solicitud.DNI = Convert.ToInt64(Uat.Cliente.Persona.NroDocumento);
                    solicitud.EntidadId = Uat.Cliente.Empresa.EntidadIdCGE;
                    solicitud.FotoDNIAnverso = uat.FotoDNIAnverso;
                    solicitud.FotoDNIReverso = uat.FotoDNIReverso;
                    solicitud.FotoSosteniendoDNI = uat.FotoSosteniendoDNI;
                    solicitud.TipoPrestamoId = linea.TipoDescuentoCGEId;
                    solicitud.Ampliado = uat.Ampliado;
                    solicitud.MontoCuota = uat.MontoCuota;
                    solicitud.ImporteSolicitado = uat.ImporteSolicitado;
                    solicitud.CantidadCuotas = uat.CantidadCuotas;
                    solicitud.FirmaOlografica = uat.FirmaOlografica;
                    solicitud.UAT = LoginCGE(Uat.Cliente.Empresa);
                    solicitud.EntidadId = Uat.Cliente.Empresa.EntidadIdCGE;
                    solicitud.Precancelaciones = uat.Precancelaciones;
                    solicitud.MontoCuotaAmpliado = Uat.Cliente.Persona.TipoPersona.MontoAmpliacion;
                    client.BaseAddress = new Uri("https://www.cge.mil.ar:81/api/mentidades/");
                    HttpResponseMessage response = client.PostAsJsonAsync("SolicitaPrestamo", solicitud).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var readTask = response.Content.ReadAsAsync<MSolicitaPrestamoCGEDTO>();
                        readTask.Wait();
                        solicitud = readTask.Result;
                        if (solicitud.Status == 200)
                        {
                            prestamo.PrestamoCGEId = solicitud.PrestamoCGEId;
                            _context.Prestamos.Update(prestamo);
                            _context.SaveChanges();
                            string html = "";
                            html = "<br/>Estimado: " + prestamo.Cliente.Empresa.RazonSocial + "<br/><br/>";
                            html += "Nos Agrada Comunicarle que ha recibido en su bandeja de Haberes 2.0 la siguiente solicitud de descuento por Decreto 14/12 segun detalle:<br/><br/>";
                            html += "<b>Persona:</b> " + prestamo.Cliente.Persona.Apellido.Trim() + ", " + prestamo.Cliente.Persona.Nombres.Trim() + " DNI: " + prestamo.Cliente.Persona.NroDocumento + "<br/>";
                            html += "<b>Importe Solicitado:</b> " + prestamo.Capital.ToString() + "<br/>";
                            html += "<b>Cantidad de Cuotas:</b> " + prestamo.CantidadCuotas.ToString() + "<br/>";
                            html += "<b>Monto de Cuota:</b> " + prestamo.MontoCuota.ToString() + "<br/><br/>";
                            html += "<b>Monto de Cuota Ampliación :</b> " + prestamo.MontoCuotaAmpliacion.ToString() + "<br/><br/>";
                            html += "Sin Otro Particular Saludamos a Ud. Muy Atentamente<br/><br/>";
                            common.EnviarMail(prestamo.Cliente.Empresa.Mail, "Solicitud de Descuento - Causante: " + prestamo.Cliente.Persona.Apellido.Trim() + ", " + prestamo.Cliente.Persona.Nombres.Trim(), html, "");
                            common.EnviarMail("acevedoruben@hotmail.com", "Solicitud de Descuento - Causante: " + prestamo.Cliente.Persona.Apellido.Trim() + ", " + prestamo.Cliente.Persona.Nombres.Trim(), html, "");
                        }
                        else
                        {
                            var disponibleCGE = DisponibleCGE(Uat.Cliente.Empresa, Uat.Cliente);
                            if (disponibleCGE < uat.ImporteSolicitado)
                            {
                                ClientesSinDisponible clienteSinD = new ClientesSinDisponible()
                                {
                                    Cliente = Uat.Cliente,
                                    Disponible = disponibleCGE,
                                    Fecha = DateTime.Now
                                };
                                _context.ClientesSinDisponible.Add(clienteSinD);
                                _context.SaveChanges();
                            }
                            else
                            {
                                var estado = _context.EstadosPrestamos.FirstOrDefault(x => x.Id == 9);
                                prestamo.EstadoActual = estado;
                                prestamo.Observaciones = solicitud.Mensaje;
                                _context.Prestamos.Update(prestamo);
                                _context.SaveChanges();
                                uat.Status = 500;
                                uat.Mensaje = solicitud.Mensaje;
                            }
                        }
                    }
                    else
                    {
                        var estado = _context.EstadosPrestamos.FirstOrDefault(x => x.Id == 11);
                        prestamo.EstadoActual = estado;
                        prestamo.Observaciones = "Error de conexion";
                        _context.Prestamos.Update(prestamo);
                        _context.SaveChanges();
                        uat.Mensaje = "Hubo problema de conexión,su prestamo se enviara a la brevedad!";
                        uat.Status = 500;
                    }
                }
                else
                {
                    uat.Status = 200;
                    uat.Mensaje = "Solicitud Registrada Correctamente!!!";
                }
            }
            return uat;
        }
        [HttpPost]
        [Route("SolicitaPrestamoNoCGE")]
        [EnableCors("CorsPolicy")]
        [AllowAnonymous]
        public MSolicitaPrestamoDTO SolicitaPrestamoNoCge([FromBody] MSolicitaPrestamoDTO uat)
        {
            var Uat = _context.UAT.FirstOrDefault(x => x.Token == uat.UAT);
            if (Uat == null)
            {
                uat.Status = 500;
                uat.Mensaje = "Uat Invalida";
                return uat;
            }
            if (uat == null)
            {
                uat.Status = 500;
                uat.Mensaje = "uat Invalida";
                return uat;
            }
            var prestamoBAF = _context.Prestamos.FirstOrDefault(x => x.Cliente.Persona.NroDocumento == Uat.Cliente.Persona.NroDocumento && x.PrestamoCGEId == 1);


            uat.Status = 200;
            uat.Mensaje = "Solicitud Realizada Correctamente!!!";
            using (var client = new HttpClient())
            {
                MSolicitaPrestamoCGEDTO solicitud = new MSolicitaPrestamoCGEDTO();
                solicitud.DNI = Convert.ToInt64(Uat.Cliente.Persona.NroDocumento);
                solicitud.EntidadId = Uat.Cliente.Empresa.EntidadIdCGE;
                solicitud.FotoDNIAnverso = prestamoBAF.Cliente.FotoDNIAnverso;
                solicitud.FotoDNIReverso = prestamoBAF.Cliente.FotoDNIReverso;
                solicitud.FotoSosteniendoDNI = prestamoBAF.Cliente.FotoSosteniendoDNI;
                solicitud.TipoPrestamoId = prestamoBAF.Linea.TipoDescuentoCGEId;
                solicitud.Ampliado = prestamoBAF.Ampliado;
                solicitud.MontoCuota = prestamoBAF.MontoCuota;
                solicitud.ImporteSolicitado = prestamoBAF.Capital;
                solicitud.CantidadCuotas = prestamoBAF.CantidadCuotas;
                solicitud.FirmaOlografica = prestamoBAF.FirmaOlografica;
                solicitud.UAT = LoginCGE(Uat.Cliente.Empresa);
                solicitud.EntidadId = Uat.Cliente.Empresa.EntidadIdCGE;
                solicitud.Precancelaciones = null;
                solicitud.MontoCuotaAmpliado = Uat.Cliente.Persona.TipoPersona.MontoAmpliacion;
                client.BaseAddress = new Uri("https://www.cge.mil.ar:81/api/mentidades/");
                HttpResponseMessage response = client.PostAsJsonAsync("SolicitaPrestamo", solicitud).Result;
                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsAsync<MSolicitaPrestamoCGEDTO>();
                    readTask.Wait();
                    solicitud = readTask.Result;
                    if (solicitud.Status == 200)
                    {
                        prestamoBAF.PrestamoCGEId = solicitud.PrestamoCGEId;
                        _context.Prestamos.Update(prestamoBAF);
                        _context.SaveChanges();
                    }
                    else
                    {
                        uat.Status = 500;
                        uat.Mensaje = solicitud.Mensaje;
                    }
                }
                else
                {
                    uat.Mensaje = response.RequestMessage.ToString();
                    uat.Mensaje = "Error de API";
                    uat.Status = 500;
                }
                string html = "";
                html = "<br/>Estimado: " + prestamoBAF.Cliente.Empresa.RazonSocial + "<br/><br/>";
                html += "Nos Agrada Comunicarle que ha recibido en su bandeja de Haberes 2.0 la siguiente solicitud de descuento por Decreto 14/12 segun detalle:<br/><br/>";
                html += "<b>Persona:</b> " + prestamoBAF.Cliente.Persona.Apellido.Trim() + ", " + prestamoBAF.Cliente.Persona.Nombres.Trim() + " DNI: " + prestamoBAF.Cliente.Persona.NroDocumento + "<br/>";
                html += "<b>Importe Solicitado:</b> " + prestamoBAF.Capital.ToString() + "<br/>";
                html += "<b>Cantidad de Cuotas:</b> " + prestamoBAF.CantidadCuotas.ToString() + "<br/>";
                html += "<b>Monto de Cuota:</b> " + prestamoBAF.MontoCuota.ToString() + "<br/><br/>";
                html += "<b>Monto de Cuota Ampliación :</b> " + prestamoBAF.MontoCuotaAmpliacion.ToString() + "<br/><br/>";
                html += "Sin Otro Particular Saludamos a Ud. Muy Atentamente<br/><br/>";
                common.EnviarMail(prestamoBAF.Cliente.Empresa.Mail, "Solicitud de Descuento - Causante: " + prestamoBAF.Cliente.Persona.Apellido.Trim() + ", " + prestamoBAF.Cliente.Persona.Nombres.Trim(), html, "");
                common.EnviarMail("acevedoruben@hotmail.com", "Solicitud de Descuento - Causante: " + prestamoBAF.Cliente.Persona.Apellido.Trim() + ", " + prestamoBAF.Cliente.Persona.Nombres.Trim(), html, "");
            }
            return uat;
        }

        public bool RegitrarPrestamoOtroOrganismo(MSolicitaPrestamoDTO uat, UAT Uat, LineasPrestamos linea)
        {
            try
            {
                Prestamos prestamo = new Prestamos();
                prestamo.FechaSolicitado = DateTime.Now;
                prestamo.Capital = uat.ImporteSolicitado;
                prestamo.Cliente = Uat.Cliente;
                prestamo.Linea = linea;
                prestamo.EstadoActual = _context.EstadosPrestamos.Find(1);
                prestamo.PrestamoCGEId = 0;
                prestamo.FirmaOlografica = uat.FirmaOlografica;
                prestamo.Cliente.FotoDNIAnverso = uat.FotoDNIAnverso;
                prestamo.Cliente.FotoDNIReverso = uat.FotoDNIReverso;
                prestamo.Cliente.FotoSosteniendoDNI = uat.FotoSosteniendoDNI;
                prestamo.FotoReciboHaber = uat.FotoReciboHaber;
                prestamo.ConstanciaCBU = uat.ConstanciaCBU;
                prestamo.CertificadoDescuento = uat.FotoCertificadoDescuento;
                prestamo.CBU = uat.CBU;
                //prestamo.FotoCertificadoDescuento = uat.FotoCertificadoDescuento;
                prestamo.CantidadCuotas = uat.CantidadCuotas;
                prestamo.MontoCuota = uat.MontoCuota;
                prestamo.MontoMensualDisponible = Uat.Cliente.MontoMensualDisponible;
                prestamo.CFT = _context.LineasPrestamosPlanes.FirstOrDefault(x => x.Linea.Id == linea.Id).CFT;
                prestamo.TEMAmprom = _context.LineasPrestamosPlanes.FirstOrDefault(x => x.Linea.Id == linea.Id && x.Activo == true).TEM;
                prestamo.TNAAmprom = _context.LineasPrestamosPlanes.FirstOrDefault(x => x.Linea.Id == linea.Id && x.Activo == true).TNA;
                prestamo.CapitalAmprom = _context.LineasPrestamosPlanes.FirstOrDefault(x => x.Linea.Id == linea.Id && x.Activo == true && x.MontoPrestado == uat.ImporteSolicitado).CapitalSmartClick;
                if (uat.Ampliado)
                {
                    prestamo.Ampliado = true;
                    prestamo.MontoCuotaAmpliacion = uat.MontoCuotaAmpliado;
                }
                else
                {
                    prestamo.Ampliado = false;
                }
                //prestamo.CFT = common.CalculaCFT(Convert.ToDouble(uat.ImporteSolicitado), uat.CantidadCuotas, Convert.ToDouble(uat.MontoCuota));


                Clientes cliente = new Clientes();
                cliente = prestamo.Cliente;

                cliente.CBU = uat.CBU;

                _context.Prestamos.Add(prestamo);
                _context.Clientes.Update(cliente);
                _context.SaveChanges();
                string html = "";
                html = "<br/>Estimado: " + prestamo.Cliente.Empresa.RazonSocial + "<br/><br/>";
                html += "Nos Agrada Comunicarle que ha recibido en su bandeja de Aprobación la siguiente solicitud de Prestamo :<br/><br/>";
                html += "<b>Organismo:</b> " + prestamo.Cliente.Persona.TipoPersona.Organismo.Descripcion.Trim() + "<br/>";
                html += "<b>Persona:</b> " + prestamo.Cliente.Persona.Apellido.Trim() + ", " + prestamo.Cliente.Persona.Nombres.Trim() + " DNI: " + prestamo.Cliente.Persona.NroDocumento + "<br/>";
                html += "<b>Importe Solicitado:</b> " + prestamo.Capital.ToString() + "<br/>";
                html += "<b>Cantidad de Cuotas:</b> " + prestamo.CantidadCuotas.ToString() + "<br/>";
                html += "<b>Monto de Cuota:</b> " + prestamo.MontoCuota.ToString() + "<br/><br/>";
                html += "Sin Otro Particular Saludamos a Ud. Muy Atentamente<br/><br/>";
                common.EnviarMail("racingdario@gmail.com", "Solicitud de Prestamo - Causante: " + prestamo.Cliente.Persona.Apellido.Trim() + ", " + prestamo.Cliente.Persona.Nombres.Trim(), html, "");
                common.EnviarMail(prestamo.Cliente.Empresa.Mail, "Solicitud de Descuento - Causante: " + prestamo.Cliente.Persona.Apellido.Trim() + ", " + prestamo.Cliente.Persona.Nombres.Trim(), html, "");
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public string LoginCGE(Empresas empresa)
        {
            using (var client = new HttpClient())
            {
                MLoginEntidadesDTO login = new MLoginEntidadesDTO();
                login.CUIT = empresa.CUIT;
                login.Password = empresa.PasswordCGE;
                login.Token = empresa.TokenCGE;
                client.BaseAddress = new Uri("https://www.cge.mil.ar:81/api/mentidades/");
                HttpResponseMessage response = client.PostAsJsonAsync("Login", login).Result;
                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsAsync<MLoginEntidadesDTO>();
                    readTask.Wait();
                    login = readTask.Result;
                    if (login.Status == 200)
                    {
                        return login.UAT;
                    }
                }
                return response.ToString();
            }
        }
        [HttpPost]
        [Route("AnulaPrestamo")]
        [EnableCors("CorsPolicy")]
        [AllowAnonymous]
        public MAnulaPrestamoDTO AnulaPrestamo([FromBody] MAnulaPrestamoDTO uat)
        {
            var Uat = _context.UAT.FirstOrDefault(x => x.Token == uat.UAT);

            if (uat == null)
            {
                uat.Status = 500;
                uat.Mensaje = "UAT Invalida";
                return uat;
            }
            var prestamo = _context.Prestamos.Find(uat.PrestamoId);
            var e = prestamo.Cliente.Persona.TipoPersona.Organismo.Id;
            if (prestamo == null)
            {
                uat.Status = 500;
                uat.Mensaje = "Prestamo Inexistente";
                return uat;
            }
            if (prestamo.EstadoActual.AnulablePersona == false)
            {
                uat.Status = 500;
                uat.Mensaje = "Prestamo en Estado No Anulable";
                return uat;
            }
            if (prestamo.FechaAnulacion != null)
            {
                uat.Status = 500;
                uat.Mensaje = "Prestamo Ya Anulado!!!";
                return uat;
            }
            prestamo.FechaAnulacion = DateTime.Now;
            prestamo.ObservacionesAnulacion = "Anulado Por Causante en App SmartClick";
            var estado = _context.EstadosPrestamos.Find(5);
            prestamo.EstadoActual = estado;
            _context.Prestamos.Update(prestamo);
            _context.SaveChanges();
            if (e == 1)
            {
                uat.UAT = LoginCGE(Uat.Cliente.Empresa);
                uat.PrestamoId = prestamo.PrestamoCGEId;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://www.cge.mil.ar:81/api/mentidades/");
                    HttpResponseMessage response = client.PostAsJsonAsync("AnulaPrestamo", uat).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var readTask = response.Content.ReadAsAsync<MAnulaPrestamoDTO>();
                        readTask.Wait();
                        uat = readTask.Result;
                        if (uat.Status == 200)
                        {
                            uat.Mensaje = "Prestamo Anulado Correctamente";
                        }
                        else
                        {
                            uat.Status = 500;
                            uat.Mensaje = "Prestamo Ya Anulado en CGE Con Anterioridad";
                        }
                    }
                    else
                    {
                        uat.Mensaje = "Error de API";
                        uat.Status = 500;
                    }
                }
            }
            else
            {
                uat.Status = 200;
                uat.Mensaje = "Prestamo Anulado Correctamente";
            }
            return uat;
        }
        [HttpPost]
        [Route("TraeLegajoElectronico")]
        [EnableCors("CorsPolicy")]
        [AllowAnonymous]
        public MTraeLegajoElectronicoDTO TraeLegajoElectronico([FromBody] MTraeLegajoElectronicoDTO uat)
        {
            var Uat = _context.UAT.FirstOrDefault(x => x.Token == uat.UAT);
            if (uat == null)
            {
                uat.Status = 500;
                uat.Mensaje = "UAT Invalida";
                return uat;
            }
            var prestamo = _context.Prestamos.Find(uat.PrestamoId);
            var e = prestamo.Cliente.Persona.TipoPersona.Organismo.Id;
            if (prestamo == null)
            {
                uat.Status = 500;
                uat.Mensaje = "Prestamo Inexistente";
                return uat;
            }

            uat.Status = 200;
            uat.Mensaje = "Legajo Ok";
            if (e == 1)
            {
                uat.PrestamoId = prestamo.PrestamoCGEId;
                uat.UAT = LoginCGE(Uat.Cliente.Empresa);
                ActualizaLegajoCGE(prestamo);
            }
            else
            {
                uat.PrestamoId = prestamo.Id;
            }
            uat.LegajoElectronico = "data:application/pdf;base64," + Convert.ToBase64String(TraeLegajoMutual(prestamo));

            return uat;
        }
        [HttpPost]
        [Route("TraeLegajoElectronicoCGE")]
        [EnableCors("CorsPolicy")]
        [AllowAnonymous]
        public MTraeLegajoElectronicoDTO TraeLegajoElectronicoCGE([FromBody] MTraeLegajoElectronicoDTO uat)
        {
            var Uat = _context.UAT.FirstOrDefault(x => x.Token == uat.UAT);
            if (uat == null)
            {
                uat.Status = 500;
                uat.Mensaje = "UAT Invalida";
                return uat;
            }
            var prestamo = _context.Prestamos.Find(uat.PrestamoId);
            if (prestamo == null)
            {
                uat.Status = 500;
                uat.Mensaje = "Prestamo Inexistente";
                return uat;
            }
            uat.PrestamoId = prestamo.PrestamoCGEId;
            uat.Status = 200;
            uat.Mensaje = "Legajo Ok";
            uat.UAT = LoginCGE(Uat.Cliente.Empresa);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://www.cge.mil.ar:81/api/mentidades/");
                HttpResponseMessage response = client.PostAsJsonAsync("TraeLegajoElectronico", uat).Result;
                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsAsync<MTraeLegajoElectronicoDTO>();
                    readTask.Wait();
                    uat = readTask.Result;
                }
                else
                {
                    uat.Mensaje = "Error de API";
                    uat.Status = 500;
                }
            }
            return uat;
        }
        [Route("DescargaLegajoElectronicoCGE")]
        [EnableCors("CorsPolicy")]
        [AllowAnonymous]
        public IActionResult DescargaLegajoElectronicoCGE(string uat, int PrestamoId)
        {
            var Uat = _context.UAT.FirstOrDefault(x => x.Token == uat);
            if (uat == null)
            {
                return null;
            }
            var prestamo = _context.Prestamos.Find(PrestamoId);
            if (prestamo == null)
            {
                return null;
            }
            MTraeLegajoElectronicoDTO traelegajo = new MTraeLegajoElectronicoDTO();
            traelegajo.PrestamoId = prestamo.PrestamoCGEId;
            traelegajo.Status = 200;
            traelegajo.Mensaje = "Legajo Ok";
            traelegajo.UAT = LoginCGE(Uat.Cliente.Empresa);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://www.cge.mil.ar:81/api/mentidades/");
                HttpResponseMessage response = client.PostAsJsonAsync("TraeLegajoElectronico", traelegajo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsAsync<MTraeLegajoElectronicoDTO>();
                    readTask.Wait();
                    traelegajo = readTask.Result;
                }
                else
                {
                    return null;
                }
            }
            return File(Convert.FromBase64String(traelegajo.LegajoElectronico.Replace("data:application/pdf;base64,", "")), "application/pdf");
        }
        [Route("DescargaLegajoElectronico")]
        [EnableCors("CorsPolicy")]
        [AllowAnonymous]
        public IActionResult DescargaLegajoElectronico(string uat, int PrestamoId)
        {
            var Uat = _context.UAT.FirstOrDefault(x => x.Token == uat);
            if (uat == null)
            {
                return null;
            }
            var prestamo = _context.Prestamos.Find(PrestamoId);
            if (prestamo == null)
            {
                return null;
            }
            return File(TraeLegajoMutual(prestamo), "application/pdf");
        }
        [HttpPost]
        [Route("SolicitaToken")]
        [EnableCors("CorsPolicy")]
        [AllowAnonymous]
        public MSolicitaTokenDTO SolicitaToken([FromBody] MSolicitaTokenDTO uat)
        {
            var Uat = _context.UAT.FirstOrDefault(x => x.Token == uat.UAT);
            if (uat == null)
            {
                uat.Status = 500;
                uat.Mensaje = "UAT Invalida";
                return uat;
            }
            var prestamo = _context.Prestamos.Find(uat.PrestamoId);
            if (prestamo == null)
            {
                uat.Status = 500;
                uat.Mensaje = "Prestamo Invalido";
                return uat;
            }
            uat.Status = 200;
            uat.Mensaje = "Token Enviado Ok";
            if (prestamo.Cliente.Persona.TipoPersona.Organismo.Id == 1)
            {
                var uatcge = new MEnviaTokenPrestamoDTOCGE();
                uatcge.UAT = LoginCGE(Uat.Cliente.Empresa);
                uatcge.PrestamoId = prestamo.PrestamoCGEId;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://www.cge.mil.ar:81/api/mentidades/");
                    HttpResponseMessage response = client.PostAsJsonAsync("EnviaTokenPrestamoPersona", uatcge).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var readTask = response.Content.ReadAsAsync<MEnviaTokenPrestamoDTOCGE>();
                        readTask.Wait();
                        uatcge = readTask.Result;
                        uat.Status = uatcge.Status;
                        uat.Mensaje = uatcge.Mensaje;
                    }
                    else
                    {
                        uat.Status = 500;
                        uat.Mensaje = "Error de API";
                        return uat;
                    }
                }
            }
            else
            {
                int token = common.NiumeroRandom(100000, 999999);
                prestamo.Cliente.Usuario.Token = token;
                var usu = _context.Usuarios.FirstOrDefault(x => x.UserName == Uat.Cliente.Usuario.UserName);
                _context.Usuarios.Update(usu);
                _context.SaveChanges();
                string html = "";
                html = "<br/>Sr: " + prestamo.Cliente.Usuario.UserName + "<br/><br/>";
                html += "Su Token de Confirmación de prestamo es: " + token.ToString() + "<br/><br/>";
                common.EnviarMail(prestamo.Cliente.Usuario.UserName.Trim(), "Token Confirmación SmartClick", html, "");
                common.EnviarMail("acevedoruben@hotmail.com", "Token Confirmación SmartClick", html, "");
                uat.Mensaje = "Envio email con Token !!";
            }


            return uat;
        }
        [HttpPost]
        [Route("ConfirmarPrestamo")]
        [EnableCors("CorsPolicy")]
        [AllowAnonymous]
        public MConfirmarPrestamoDTO ConfirmarPrestamo([FromBody] MConfirmarPrestamoDTO uat)
        {
            var Uat = _context.UAT.FirstOrDefault(x => x.Token == uat.UAT);
            if (Uat == null)
            {
                uat.Status = 500;
                uat.Mensaje = "UAT Invalida";
                return uat;
            }
            if (uat == null)
            {
                uat.Status = 500;
                uat.Mensaje = "UAT Invalida";
                return uat;
            }
            var prestamo = _context.Prestamos.Find(uat.PrestamoId);
            var e = prestamo.Cliente.Persona.TipoPersona.Organismo.Id;
            if (prestamo == null)
            {
                uat.Status = 500;
                uat.Mensaje = "Prestamo Invalido";
                return uat;
            }
            if (uat.FirmaOlografica == null)
            {
                uat.Status = 500;
                uat.Mensaje = "Debe Subir Firma Olografica";
                return uat;
            }
            uat.Status = 200;
            uat.Mensaje = "Legajo Ok";

            if (prestamo.Cliente.Persona.TipoPersona.Organismo.APIEjercito == true)
            //if (e == 1)
            {
                var uatcge = new MEnviaOpcionesConfirmadasDTO();
                uatcge.UAT = LoginCGE(Uat.Cliente.Empresa);
                uatcge.CantidadCuotas = prestamo.CantidadCuotas;
                uatcge.ImporteCuota = prestamo.MontoCuota;
                uatcge.ImportePrestado = prestamo.Capital;
                uatcge.PrestamoId = prestamo.PrestamoCGEId;
                if (prestamo.FirmaOlografica == null)
                {
                    prestamo.FirmaOlograficaConfirmacion = uat.FirmaOlografica;
                    prestamo.FirmaOlografica = uat.FirmaOlografica;
                }
                else
                {
                    prestamo.FirmaOlograficaConfirmacion = uat.FirmaOlografica;

                }
                //prestamo.Integracion = false;
                uatcge.FirmaOlografica = uat.FirmaOlografica;
                uatcge.Token = uat.Token;
                uatcge.LegajoEntidad = TraeLegajoMutual(prestamo);
                uatcge.ImporteAmpliacion = prestamo.MontoCuotaAmpliacion;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://www.cge.mil.ar:81/api/mentidades/");
                    HttpResponseMessage response = client.PostAsJsonAsync("EnviaOpcionesConfirmadas", uatcge).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var readTask = response.Content.ReadAsAsync<MEnviaOpcionesConfirmadasDTO>();
                        readTask.Wait();
                        uatcge = readTask.Result;
                        uat.Status = uatcge.Status;
                        if (uat.Status == 200)
                        {
                            uat.Mensaje = uatcge.Mensaje + " ,para que se haga efectiva la acreditación del mismo, debe contactarse a: Atención al cliente SmartClick 11 4080 8488";
                        }
                        else
                        {
                            uat.Mensaje = uatcge.Mensaje;
                        }
                        return uat;
                    }
                    else
                    {
                        uat.Status = 500;
                        uat.Mensaje = "Error de API";
                        return uat;
                    }
                }

            }
            else if (uat.Token == prestamo.Cliente.Usuario.Token)
            {
                prestamo.EstadoActual = _context.EstadosPrestamos.Find(2);
                prestamo.FechaAprobacion = DateTime.Now;
                if (prestamo.FirmaOlografica == null)
                {
                    prestamo.FirmaOlografica = uat.FirmaOlografica;
                }
                if (uat.FirmaOlografica.Length > 0)
                {
                    prestamo.FirmaOlograficaConfirmacion = uat.FirmaOlografica;
                }
                else
                {
                    prestamo.FirmaOlograficaConfirmacion = prestamo.FirmaOlografica;
                }

                prestamo.Pais = _context.Paises.Find(uat.PaisId);
                prestamo.Provincia = _context.Provincia.Find(uat.ProvinciaId);
                prestamo.Localidad = _context.Localidad.Find(uat.LocalidadId);
                prestamo.Domicilio = uat.Domicilio;
                prestamo.Calle  = uat.Calle;
                prestamo.CalleNro = uat.CalleNro;
                prestamo.Piso = uat.Piso;
                prestamo.Dpto = uat.Dpto;
                prestamo.CodPostal = uat.CodPostal;

                _context.Prestamos.Update(prestamo);
                _context.SaveChanges();
                uat.Status = 200;
                uat.Mensaje = "Se confirmo correctamente Para que se haga efectiva la acreditación del mismo, debe contactarse a: Atención al cliente SmartClick 11 2553 8288";
                return uat;
            }
            else
            {
                uat.Status = 500;
                uat.Mensaje = "Token Invalido";
                return uat;
            }
        }
        [HttpPost]
        [Route("TraePlanesDisponibles")]
        [EnableCors("CorsPolicy")]
        [AllowAnonymous]
        public MTraePlanesDisponiblesDTO TraePlanesDisponibles([FromBody] MTraePlanesDisponiblesDTO uat)
        {
            var Uat = _context.UAT.FirstOrDefault(x => x.Token == uat.UAT);
            if (uat == null)
            {
                uat.Status = 500;
                uat.Mensaje = "UAT Invalida";
                return uat;
            }
            uat.Status = 200;
            uat.Mensaje = "Lineas Ok";
            MTraeDsiponibleCGEDTO disponible = new MTraeDsiponibleCGEDTO();
            disponible.UAT = LoginCGE(Uat.Cliente.Empresa);
            disponible.DNI = Convert.ToInt32(Uat.Cliente.Persona.NroDocumento);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://www.cge.mil.ar:81/api/mentidades/");
                HttpResponseMessage response = client.PostAsJsonAsync("TraeDisponible", disponible).Result;
                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsAsync<MTraeDsiponibleCGEDTO>();
                    readTask.Wait();
                    disponible = readTask.Result;
                }
                else
                {
                    return uat;
                }
            }
            if (Uat.Cliente.Persona.NroDocumento == "26625527")
            {
                disponible.Disponible = 25000;

            }
            uat.Disponible = disponible.Disponible;

            var limiteCuota = Uat.Cliente.Persona.TipoPersona.LimiteCuotas;
            if (uat.Ampliado)
            {
                disponible.Disponible += Uat.Cliente.Persona.TipoPersona.MontoAmpliacion;
                limiteCuota = Uat.Cliente.Persona.TipoPersona.TopeCantCuotasAmpliacion;
            }
            var planes = _context.LineasPrestamosPlanes.Where(x => x.Linea.Id == uat.LineaId && (x.MontoPrestado / 1000 == Math.Round(x.MontoPrestado / 1000) || uat.ImporteDeseado > 0) && x.MontoCuota <= (disponible.Disponible - x.MargenDisponible) && x.MontoPrestado <= Uat.Cliente.Persona.TipoPersona.TopePrestamo && x.CantidadCuotas <= limiteCuota && (x.MontoPrestado <= uat.ImporteDeseado || uat.ImporteDeseado == 0) && x.Activo == true).OrderByDescending(x => x.MontoPrestado).ThenByDescending(x => x.CantidadCuotas);
            if (planes.Count() == 0)
            {
                uat.Status = 500;
                uat.Mensaje = "Sin Disponible Para Realizar Prestamo";

                //uat.Mensaje=disponible.Disponible.ToString();
                return uat;
            }
            decimal montofijo = 0;
            if (uat.ImporteDeseado > 0)
            {
                montofijo = planes.Where(x => x.MontoPrestado == uat.ImporteDeseado).OrderBy(x => x.MontoPrestado).FirstOrDefault().MontoPrestado;
            }
            List<PlanesDisponiblesDTO> lista = new List<PlanesDisponiblesDTO>();
            foreach (var plan in planes)
            {
                if (montofijo == 0 || plan.MontoPrestado == montofijo)
                {
                    var nuevo = new PlanesDisponiblesDTO();
                    nuevo.CantidadCuotas = plan.CantidadCuotas;
                    nuevo.Id = plan.Id;
                    nuevo.MontoCuota = plan.MontoCuota;
                    nuevo.MontoPrestado = plan.MontoPrestado;
                    nuevo.CFT = plan.CFT;
                    //nuevo.CFT = common.CalculaCFT(Convert.ToDouble(plan.MontoPrestado), plan.CantidadCuotas, Convert.ToDouble(plan.MontoCuota));
                    lista.Add(nuevo);
                }
            }
            uat.PlanesDisponibles = lista;
            uat.Mensaje = disponible.Mensaje;
            return uat;
        }
        [HttpPost]
        [Route("TraePlanesDisponiblesOtroOrganismo")]
        [EnableCors("CorsPolicy")]
        [AllowAnonymous]
        public MTraePlanesDisponiblesDTO TraePlanesDisponiblesOtroOrganismo([FromBody] MTraePlanesDisponiblesDTO uat)
        {
            var Uat = _context.UAT.FirstOrDefault(x => x.Token == uat.UAT);
            if (uat == null)
            {
                uat.Status = 500;
                uat.Mensaje = "UAT Invalida";
                return uat;
            }
            if (Uat == null)
            {
                uat.Status = 500;
                uat.Mensaje = "UAT Invalida";
                return uat;
            }
            uat.Status = 200;
            uat.Mensaje = "Lineas Ok";

            var limiteCuota = Uat.Cliente.Persona.TipoPersona.LimiteCuotas;
            if (uat.Ampliado)
            {
                uat.Disponible += Uat.Cliente.Persona.TipoPersona.MontoAmpliacion;
                limiteCuota = Uat.Cliente.Persona.TipoPersona.TopeCantCuotasAmpliacion;
            }
            var planes = _context.LineasPrestamosPlanes.Where(x => x.Linea.Id == uat.LineaId && (x.MontoPrestado / 1000 == Math.Round(x.MontoPrestado / 1000) || uat.ImporteDeseado > 0) && x.MontoCuota <= (Uat.Cliente.MontoMensualDisponible - x.MargenDisponible) && x.MontoPrestado <= Uat.Cliente.Persona.TipoPersona.TopePrestamo && x.CantidadCuotas <= limiteCuota && (x.MontoPrestado <= uat.ImporteDeseado || uat.ImporteDeseado == 0) && x.Activo == true).OrderByDescending(x => x.MontoPrestado).ThenByDescending(x => x.CantidadCuotas);
            //var planes = _context.LineasPrestamosPlanes.Where(x => x.Linea.Id == uat.LineaId && (x.MontoPrestado / x.Linea.Intervalo == Math.Round(x.MontoPrestado / x.Linea.Intervalo) || uat.ImporteDeseado > 0) && x.MontoCuota <= Uat.Cliente.Persona.TipoPersona.TopePrestamo && x.CantidadCuotas <= Uat.Cliente.Persona.TipoPersona.LimiteCuotas && (x.MontoPrestado >= uat.ImporteDeseado || uat.ImporteDeseado == 0) && x.Activo == true && x.MontoCuota <= Uat.Cliente.MontoMensualDisponible).OrderByDescending(x => x.MontoPrestado).ThenByDescending(x => x.CantidadCuotas);
            if (planes.Count() == 0)
            {
                uat.Status = 500;
                uat.Mensaje = "Sin Disponible Para Realizar Prestamo";

                //uat.Mensaje=disponible.Disponible.ToString();
                return uat;
            }
            decimal montofijo = 0;
            if (uat.ImporteDeseado > 0)
            {
                montofijo = planes.Where(x => x.MontoPrestado == uat.ImporteDeseado).OrderBy(x => x.MontoPrestado).FirstOrDefault().MontoPrestado;
                //montofijo = planes.OrderBy(x => x.MontoPrestado).FirstOrDefault().MontoPrestado;
            }
            List<PlanesDisponiblesDTO> lista = new List<PlanesDisponiblesDTO>();
            foreach (var plan in planes)
            {
                if (montofijo == 0 || plan.MontoPrestado == montofijo)
                {
                    var nuevo = new PlanesDisponiblesDTO();
                    nuevo.CantidadCuotas = plan.CantidadCuotas;
                    nuevo.Id = plan.Id;
                    nuevo.MontoCuota = plan.MontoCuota;
                    nuevo.MontoPrestado = plan.MontoPrestado;
                    nuevo.CFT = plan.CFT;
                    //nuevo.CFT = common.CalculaCFT(Convert.ToDouble(plan.MontoPrestado), plan.CantidadCuotas, Convert.ToDouble(plan.MontoCuota));
                    lista.Add(nuevo);
                }
            }
            uat.PlanesDisponibles = lista;
            uat.Mensaje = "Disponible Para Realizar Prestamo";
            return uat;
        }
        public bool ActualizaPrestamoCGE(Prestamos prestamo)
        {

            return true;
        }
        public byte[] TraeLegajoMutual(Prestamos prestamo)
        {
            prestamo.MontoCuotaEnLetras = "PESOS " + common.NumeroALetras(Convert.ToDouble(prestamo.MontoCuota));
            prestamo.CuotasEnLetras = common.NumeroALetras(Convert.ToDouble(prestamo.CantidadCuotas));
            prestamo.CapitalEnLetras = "PESOS " + common.NumeroALetras(Convert.ToDouble(prestamo.Capital));
            _context.Prestamos.Update(prestamo);
            _context.SaveChanges();
            string parametros = "&id=" + prestamo.Id.ToString();
            byte[] legajo = common.Reporting("LegajoElectronico", parametros, "PDF", _context);
            return legajo;
        }
        public bool ActualizaLegajoCGE(Prestamos prestamo)
        {
            MActualizaLegajoEntidadDTO consulta = new MActualizaLegajoEntidadDTO();
            consulta.UAT = LoginCGE(prestamo.Cliente.Empresa);
            using (var client = new HttpClient())
            {
                consulta.PrestamoCGEId = prestamo.PrestamoCGEId;
                consulta.LegajoEntidad = TraeLegajoMutual(prestamo);
                client.BaseAddress = new Uri("https://www.cge.mil.ar:81/api/mentidades/");
                HttpResponseMessage response = client.PostAsJsonAsync("ActualizaLegajoEntidad", consulta).Result;
                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsAsync<MActualizaLegajoEntidadDTO>();
                    readTask.Wait();
                    consulta = readTask.Result;
                }
            }
            return true;
        }
        [HttpPost]
        [Route("TraePrecancelaciones")]
        [EnableCors("CorsPolicy")]
        [AllowAnonymous]
        public MTraePrecancelacionesDTO TraePrecancelaciones([FromBody] MTraePrecancelacionesDTO uat)
        {
            var Uat = _context.UAT.FirstOrDefault(x => x.Token == uat.UAT);
            if (uat == null)
            {
                uat.Status = 500;
                uat.Mensaje = "UAT Invalida";
                return uat;
            }
            uat.Status = 200;
            uat.Mensaje = "Precancelaciones Ok";
            uat.UAT = LoginCGE(Uat.Cliente.Empresa);
            uat.DNI = Convert.ToInt64(Uat.Cliente.Persona.NroDocumento);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://www.cge.mil.ar:81/api/mentidades/");
                HttpResponseMessage response = client.PostAsJsonAsync("TraePrecancelaciones", uat).Result;
                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsAsync<MTraePrecancelacionesDTO>();
                    readTask.Wait();
                    uat = readTask.Result;
                    if (uat.Status == 200)
                    {
                        uat.Mensaje = "Precancelaciones Ok";
                    }
                    else
                    {
                        uat.Status = 500;
                        uat.Mensaje = "Prestamo Ya Anulado en CGE Con Anterioridad";
                    }
                }
                else
                {
                    uat.Mensaje = "Error de API";
                    uat.Status = 500;
                }
            }
            return uat;
        }


        [HttpPost]
        [Route("MontoMensualDisponible")]
        [EnableCors("CorsPolicy")]
        [AllowAnonymous]
        public MontoMensualDisponibleDTO MontoMensualDisponible([FromBody] MontoMensualDisponibleDTO uat)
        {
            var Uat = _context.UAT.FirstOrDefault(x => x.Token == uat.UAT);
            if (Uat == null)
            {
                uat.Status = 500;
                uat.Mensaje = "UAT Invalida";
                return uat;
            }
            else
            {
                var cliente = _context.Clientes.Find(Uat.Cliente.Id);
                cliente.MontoMensualDisponible = uat.MontoMensualDisponible;
                _context.Clientes.Update(cliente);
                _context.SaveChanges();
                uat.Status = 200;
                uat.Mensaje = "Se Guardo Correctamente el Monto Mensual Disponible.";

            }

            return uat;
        }

        public decimal DisponibleCGE(Empresas empresa, Clientes cliente)
        {
            MTraeDsiponibleCGEDTO disponible = new MTraeDsiponibleCGEDTO();
            disponible.UAT = LoginCGE(empresa);
            disponible.DNI = Convert.ToInt32(cliente.Persona.NroDocumento);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://www.cge.mil.ar:81/api/mentidades/");
                HttpResponseMessage response = client.PostAsJsonAsync("TraeDisponible", disponible).Result;
                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsAsync<MTraeDsiponibleCGEDTO>();
                    readTask.Wait();
                    disponible = readTask.Result;
                    return disponible.Disponible;
                }
                else
                {
                    return 0;
                }
            }
        }

        ///METODOS CHAT BOT///
        ///


        ////////////////////////////////////////////
        ///////// METODOS CHAT BOT/////////
        //////////////////////////////////////////

        [HttpPost]
        [Route("RegistrarUatBot")]
        [EnableCors("CorsPolicy")]
        [AllowAnonymous]
        public MUatBotDTO RegistrarUatBot([FromBody] MUatBotDTO uat)
        {

            // Valido si alguna vez registro uat

            var uatBot = _context.UatBot.FirstOrDefault(x => x.Dni == uat.Dni);
         
            if (uatBot is null)
            {
                UatBot NuevouatBot = new UatBot();

                NuevouatBot.Uat = uat.Uat;
                NuevouatBot.Dni = uat.Dni;
                NuevouatBot.Celular = uat.Celular;
                NuevouatBot.Nombre = uat.Nombre;
                NuevouatBot.Apellido = uat.Apellido;
                NuevouatBot.Email = uat.Email;
                NuevouatBot.TipoPersonaId = uat.TipoPersonaId;
                NuevouatBot.LineaPrestamoId = _context.LineasPrestamosTiposPersonas.Where(x => x.TipoPersona.Id == uat.TipoPersonaId).Select(x => x.LineaPrestamo.Id).FirstOrDefault();
                NuevouatBot.ImporteSolicitado = uat.ImporteSolicitado;
                NuevouatBot.CantidadCuotas = uat.CantidadCuotas;
                NuevouatBot.MontoCuota = uat.MontoCuota;
                if (uat.FotoDNIAnverso != null)
                {
                    NuevouatBot.FotoDNIAnverso = uat.FotoDNIAnverso;
                }
                if (uat.FotoDNIReverso != null)
                {
                    NuevouatBot.FotoDNIReverso = uat.FotoDNIReverso;
                }
                if (uat.FotoSosteniendoDNI != null)
                {
                    NuevouatBot.FotoSosteniendoDNI = uat.FotoSosteniendoDNI;
                }
                if (uat.FirmaOlografica != null)
                {
                    NuevouatBot.FirmaOlografica = uat.FirmaOlografica;
                }
                
                _context.UatBot.Add(NuevouatBot);
                _context.SaveChanges();

                return uat;
            }

            // ACTUALIZA DATOS UAT
            uatBot.Uat = uat.Uat;
            uatBot.Celular = uat.Celular;
            uatBot.Nombre = uat.Nombre;
            uatBot.Apellido = uat.Apellido;
            if (uat.Email != null)
            {
                uatBot.Email = uat.Email;
            }
            if (uat.TipoPersonaId != 0)
            {
                uatBot.TipoPersonaId = uat.TipoPersonaId;
                uatBot.LineaPrestamoId = _context.LineasPrestamosTiposPersonas.Where(x => x.TipoPersona.Id == uat.TipoPersonaId).Select(x => x.LineaPrestamo.Id).FirstOrDefault();
            }
     
            uatBot.ImporteSolicitado = uat.ImporteSolicitado;
            uatBot.CantidadCuotas = uat.CantidadCuotas;
            uatBot.MontoCuota = uat.MontoCuota;
            if (uat.FotoDNIAnverso != null)
            {
                uatBot.FotoDNIAnverso = uat.FotoDNIAnverso;
            }
            if (uat.FotoDNIReverso != null)
            {
                uatBot.FotoDNIReverso = uat.FotoDNIReverso;
            }
            if (uat.FotoSosteniendoDNI != null)
                {
                uatBot.FotoSosteniendoDNI = uat.FotoSosteniendoDNI;
            }
            if (uat.FirmaOlografica != null)
            {
                uatBot.FirmaOlografica = uat.FirmaOlografica;
            }
            _context.UatBot.Update(uatBot);
            _context.SaveChanges();

            return uat;
        }

        [HttpPost]
        [Route("TraeDisponibleCge")]
        [EnableCors("CorsPolicy")]
        [AllowAnonymous]
        public MtraerDisponibleCgeDTO TraeDisponibleCge([FromBody] MtraerDisponibleCgeDTO uat)
        {

      
            var Empresa = _context.Empresas.Find(1);

            MTraeDsiponibleCGEDTO disponible = new MTraeDsiponibleCGEDTO();
            disponible.UAT = LoginCGE(_context.Empresas.Find(1));
            disponible.DNI = Convert.ToInt32(uat.DNI);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://www.cge.mil.ar:81/api/mentidades/");
                HttpResponseMessage response = client.PostAsJsonAsync("TraeDisponible", disponible).Result;
                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsAsync<MTraeDsiponibleCGEDTO>();
                    readTask.Wait();
                    disponible = readTask.Result;

                    uat.Disponible = disponible.Disponible.ToString();

                    return uat;
                }
                else
                {
                    return (uat);
                }
            }

        }

        [HttpPost]
        [Route("TraePlanesDisponiblesBot")]
        [EnableCors("CorsPolicy")]
        [AllowAnonymous]
        public MTraePlanesDisponiblesBotDTO TraePlanesDisponiblesBot([FromBody] MTraePlanesDisponiblesBotDTO uat)
        {

            // Validar UATBOT
            var uatbot = _context.UatBot.FirstOrDefault(x => x.Uat == uat.UAT);

            if (uatbot is null)
            {
                uat.Status = 500;
                uat.Mensaje = "UAT Inválido";
                return uat;
            }

            uat.Status = 200;
            uat.Mensaje = "Lineas Ok";
           
            if(uat.Disponible == 0)
            { 
            MTraeDsiponibleCGEDTO disponible = new MTraeDsiponibleCGEDTO();
            disponible.UAT = LoginCGE(_context.Empresas.Find(1));
            disponible.DNI = Convert.ToInt32(uat.DNI);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://www.cge.mil.ar:81/api/mentidades/");
                HttpResponseMessage response = client.PostAsJsonAsync("TraeDisponible", disponible).Result;
                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsAsync<MTraeDsiponibleCGEDTO>();
                    readTask.Wait();
                    disponible = readTask.Result;
                   uat.Mensaje = disponible.Mensaje;
                }
                else
                {
                    return uat;
                }
            }
            //Disponible
            uat.Disponible = disponible.Disponible;
            }


            // LInea de Prestamo
            if (uat.LineaId == 0)
            {
                uat.LineaId = _context.LineasPrestamosTiposPersonas.Where(x=>x.TipoPersona.Id == uatbot.TipoPersonaId).Select(x => x.LineaPrestamo.Id).FirstOrDefault();
            }
                      
           var limiteCuota = _context.TiposPersonas.Find(uatbot.TipoPersonaId).LimiteCuotas ;
            if (uat.Ampliado)
            {
                uat.Disponible += _context.TiposPersonas.Find(uatbot.TipoPersonaId).MontoAmpliacion;
                limiteCuota = _context.TiposPersonas.Find(uatbot.TipoPersonaId).TopeCantCuotasAmpliacion;
            }
            var topePrestamo = _context.TiposPersonas.Find(uatbot.TipoPersonaId).TopePrestamo;

            if (uat.ImporteDeseado > 0)
            {

                var LineaCercana = _context.LineasPrestamosPlanes.Where(x => x.Linea.Id == uat.LineaId && (x.MontoPrestado / 1000 == Math.Round(x.MontoPrestado / 1000) ) && x.MontoCuota <= (uat.Disponible - x.MargenDisponible) && x.MontoPrestado <= topePrestamo && x.CantidadCuotas <= limiteCuota && (x.MontoPrestado >= uat.ImporteDeseado ) && x.Activo == true).OrderBy(x => x.MontoPrestado).Take(1);

                uat.ImporteDeseado = LineaCercana.First().MontoPrestado;
            }

            // Planes de Prestamos
            var planes = _context.LineasPrestamosPlanes.Where(x => x.Linea.Id == uat.LineaId && (x.MontoPrestado / 1000 == Math.Round(x.MontoPrestado / 1000) || uat.ImporteDeseado > 0) && x.MontoCuota <= (uat.Disponible - x.MargenDisponible) && x.MontoPrestado <= topePrestamo && x.CantidadCuotas <= limiteCuota && (x.MontoPrestado <= uat.ImporteDeseado || uat.ImporteDeseado == 0) && x.Activo == true).OrderByDescending(x => x.MontoPrestado).ThenByDescending(x => x.CantidadCuotas).Take(5);
            if (planes.Count() == 0)
            {
                uat.Status = 500;
                uat.Mensaje = "Sin Disponible Para Realizar Prestamo";

                //uat.Mensaje=disponible.Disponible.ToString();
                return uat;
            }
            decimal montofijo = 0;
            if (uat.ImporteDeseado > 0)
            {
                montofijo = planes.Where(x => x.MontoPrestado >= uat.ImporteDeseado ).OrderBy(x => x.MontoPrestado).FirstOrDefault().MontoPrestado;
            }
            List<PlanesDisponiblesDTO> lista = new List<PlanesDisponiblesDTO>();
            foreach (var plan in planes)
            {
                if (montofijo == 0 || plan.MontoPrestado == montofijo)
                {
                    var nuevo = new PlanesDisponiblesDTO();
                    nuevo.CantidadCuotas = plan.CantidadCuotas;
                    nuevo.Id = plan.Id;
                    nuevo.MontoCuota = plan.MontoCuota;
                    nuevo.MontoPrestado = plan.MontoPrestado;
                    nuevo.CFT = plan.CFT;
                    //nuevo.CFT = common.CalculaCFT(Convert.ToDouble(plan.MontoPrestado), plan.CantidadCuotas, Convert.ToDouble(plan.MontoCuota));
                    uat.CantidadPlanes++;
                    uat.TextoOpcionesCuotas += "*" + uat.CantidadPlanes + "* - *$ " + plan.MontoPrestado + "* en *" + plan.CantidadCuotas + "* cuotas de *$ " + plan.MontoCuota + "*" +"\n"; ;
                    lista.Add(nuevo);
                }
            }
            //uat.CantidadPlanes = planes.Count();
            uat.PlanesDisponibles = lista;
          
            return uat;
        }

        [HttpPost]
        [Route("RegistrarPrestamoBot")]
        [EnableCors("CorsPolicy")]
        [AllowAnonymous]
        public async Task<MSolicitaPrestamoBotDTO>RegistrarPrestamoBot([FromBody] MSolicitaPrestamoBotDTO uat)
        {

            // Validar UATBOT
            var uatbot = _context.UatBot.FirstOrDefault(x => x.Uat == uat.UAT);

            if (uatbot is null)
            {
                uat.Status = 500;
                uat.Mensaje = "UAT Inválido";
                return uat;
            }

            if (uat.FotoDNIAnverso != null)
            {
                uatbot.FotoDNIAnverso = uat.FotoDNIAnverso;
            }
            if (uat.FotoDNIReverso != null)
            {
                uatbot.FotoDNIReverso = uat.FotoDNIReverso;
            }
            if (uat.FotoSosteniendoDNI != null)
            {
                uatbot.FotoSosteniendoDNI = uat.FotoSosteniendoDNI;
            }
            if (uat.FirmaOlografica != null)
            {
                uatbot.FirmaOlografica = uat.FirmaOlografica;
            }

            var tipoPersona = _context.TiposPersonas.FirstOrDefault(x=>x.Id == uatbot.TipoPersonaId);
            var empresa = _context.Empresas.FirstOrDefault(x => x.Id == 1);
            var tipoCliente = _context.TiposClientes.FirstOrDefault();
            _context.UatBot.Update(uatbot);


            //Validar si es Cliente.
            var cliente = _context.Personas.Where(x => x.NroDocumento == uatbot.Dni);
         
            /////////////Alta de Cliente////////////////////
            if (cliente.Count() == 0)
            {
               
                var user = await _userManager.FindByEmailAsync(uatbot.Email);
                if (user == null)
                {
                    var newuser = new Usuario() { UserName = uatbot.Email, Email = uatbot.Email };
                    var result1 = await _userManager.CreateAsync(newuser, uatbot.Dni.ToString());
                    user = await _userManager.FindByEmailAsync(uatbot.Email);
                }

                Persona nuevapersona = new Persona();

                nuevapersona.NroDocumento = uatbot.Dni.ToString();
                nuevapersona.Apellido = uatbot.Apellido;
                nuevapersona.Nombres = uatbot.Nombre;
                nuevapersona.TipoPersona = tipoPersona;
                _context.Personas.Add(nuevapersona);

                Clientes nuevocliente = new Clientes();

                nuevocliente.Empresa = empresa;
                nuevocliente.TipoCliente = tipoCliente;
                nuevocliente.Celular = (uatbot.Celular != null) ? uatbot.Celular : "";
                nuevocliente.Persona = nuevapersona;
                _context.Clientes.Add(nuevocliente);

                user.Clientes = nuevocliente;
               _context.Usuarios.Update(user);
               _context.SaveChanges();
            }

           
         
            // Registrar Prestamo Local
                Prestamos prestamo = new Prestamos();
                prestamo.FechaSolicitado = DateTime.Now;
                prestamo.Capital = uatbot.ImporteSolicitado;
                prestamo.Cliente = _context.Clientes.FirstOrDefault(x => x.Persona.NroDocumento == uatbot.Dni);
                prestamo.Linea = _context.LineasPrestamos.FirstOrDefault(x => x.Id == uatbot.LineaPrestamoId);
                prestamo.EstadoActual = _context.EstadosPrestamos.Find(1);
                prestamo.PrestamoCGEId = 0;
                prestamo.FirmaOlografica = uat.FirmaOlografica;
                prestamo.Cliente.FotoDNIAnverso = uat.FotoDNIAnverso;
                prestamo.Cliente.FotoDNIReverso = uat.FotoDNIReverso;
                prestamo.Cliente.FotoSosteniendoDNI = uat.FotoSosteniendoDNI;
                prestamo.CantidadCuotas = uatbot.CantidadCuotas;
                prestamo.MontoCuota = uatbot.MontoCuota;
                prestamo.Canal = "BOT";
                prestamo.CFT = common.CalculaCFT(Convert.ToDouble(uatbot.ImporteSolicitado), uatbot.CantidadCuotas, Convert.ToDouble(uatbot.MontoCuota));

            
            //Registar Prestamo en CGE.
            using (var client = new HttpClient())
            {

                MSolicitaPrestamoCGEDTO solicitud = new MSolicitaPrestamoCGEDTO();
                solicitud.DNI = Convert.ToInt64(uatbot.Dni);
                solicitud.EntidadId = _context.Empresas.FirstOrDefault().EntidadIdCGE;
                solicitud.FotoDNIAnverso = uatbot.FotoDNIAnverso;
                solicitud.FotoDNIReverso = uatbot.FotoDNIReverso;
                solicitud.FotoSosteniendoDNI = uatbot.FotoSosteniendoDNI;
                solicitud.TipoPrestamoId = prestamo.Linea.TipoDescuentoCGEId;
              // solicitud.Ampliado = uat.Ampliado;
                solicitud.MontoCuota = uatbot.MontoCuota;
                solicitud.ImporteSolicitado = uatbot.ImporteSolicitado;
                solicitud.CantidadCuotas = uatbot.CantidadCuotas;
                solicitud.FirmaOlografica = uatbot.FirmaOlografica;
                solicitud.UAT = LoginCGE(_context.Empresas.FirstOrDefault());
                //solicitud.Precancelaciones = uat.Precancelaciones;
                //solicitud.MontoCuotaAmpliado = Uat.Cliente.Persona.TipoPersona.MontoAmpliacion;
                client.BaseAddress = new Uri("https://www.cge.mil.ar:81/api/mentidades/");
                HttpResponseMessage response = client.PostAsJsonAsync("SolicitaPrestamo", solicitud).Result;
                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsAsync<MSolicitaPrestamoCGEDTO>();
                    readTask.Wait();
                    solicitud = readTask.Result;
                    if (solicitud.Status == 200)
                    {
                        uat.Status = 200;
                        uat.Mensaje = "Préstamo registado con Éxito!";
                        prestamo.PrestamoCGEId = solicitud.PrestamoCGEId;
                        _context.Prestamos.Update(prestamo);
                        _context.SaveChanges();

                        string html = "";
                        html = "<br/>Estimado: " + prestamo.Cliente.Empresa.RazonSocial + "<br/><br/>";
                        html += "Nos Agrada Comunicarle que ha recibido en su bandeja de Haberes 2.0 la siguiente solicitud de descuento por Decreto 14/12 segun detalle:<br/><br/>";
                        html += "<b>Persona:</b> " + prestamo.Cliente.Persona.Apellido.Trim() + ", " + prestamo.Cliente.Persona.Nombres.Trim() + " DNI: " + prestamo.Cliente.Persona.NroDocumento + "<br/>";
                        html += "<b>Importe Solicitado:</b> " + prestamo.Capital.ToString() + "<br/>";
                        html += "<b>Cantidad de Cuotas:</b> " + prestamo.CantidadCuotas.ToString() + "<br/>";
                        html += "<b>Monto de Cuota:</b> " + prestamo.MontoCuota.ToString() + "<br/><br/>";
                        html += "Sin Otro Particular Saludamos a Ud. Muy Atentamente<br/><br/>";
                        common.EnviarMail(prestamo.Cliente.Empresa.Mail, "Solicitud de Descuento Bot - Causante: " + prestamo.Cliente.Persona.Apellido.Trim() + ", " + prestamo.Cliente.Persona.Nombres.Trim(), html, "");
                        common.EnviarMail("rolando.d.ponce@hotmail.com", "Solicitud de Descuento Bot - Causante: " + prestamo.Cliente.Persona.Apellido.Trim() + ", " + prestamo.Cliente.Persona.Nombres.Trim(), html, "");


                    }
                    else
                    {
                        var disponibleCGE = DisponibleCGE(_context.Empresas.FirstOrDefault(), _context.Clientes.FirstOrDefault(x => x.Persona.NroDocumento == uatbot.Dni));
                        if (disponibleCGE < uat.ImporteSolicitado)
                        {
                            ClientesSinDisponible clienteSinD = new ClientesSinDisponible()
                            {
                                Cliente = _context.Clientes.FirstOrDefault(x => x.Persona.NroDocumento == uatbot.Dni),
                                Disponible = disponibleCGE,
                                Fecha = DateTime.Now
                            };
                            _context.ClientesSinDisponible.Add(clienteSinD);
                            _context.SaveChanges();
                        }
                        else
                        {
                            var estado = _context.EstadosPrestamos.FirstOrDefault(x => x.Id == 9);
                            prestamo.EstadoActual = estado;
                            prestamo.Observaciones = solicitud.Mensaje;
                            _context.Prestamos.Update(prestamo);
                            _context.SaveChanges();
                            uat.Status = 500;
                            uat.Mensaje = solicitud.Mensaje;
                        }
                    }
                }
                else
                {
                    var estado = _context.EstadosPrestamos.FirstOrDefault(x => x.Id == 11);
                    prestamo.EstadoActual = estado;
                    prestamo.Observaciones = "Error de conexion";
                    _context.Prestamos.Update(prestamo);
                    _context.SaveChanges();
                    uat.Mensaje = "Hubo problema de conexión,su prestamo se enviara a la brevedad!";
                    uat.Status = 500;
                }

            }

            return uat;

        }

        
    }

}
