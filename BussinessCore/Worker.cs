using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using DAL.Data;
using DAL.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;

namespace SmartClickCore
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        public SmartClickContext _context;

        public Worker(ILogger<Worker> logger, SmartClickContext context)
        {
            _context = context;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                ActualizaPrestamo();
                ConfirmarPrestamo();
                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
            }
        }


        private void ActualizaPrestamo()
        {

            // var Prestamos = _context.Prestamos.Where(x => new[] { 1,2,3,6,7 }.Contains(x.EstadoActual.Id)).OrderByDescending(x => x.FechaSolicitado).ToList();

            var fechaDesde = DateTime.Now.AddMonths(-1); // Fecha de hace un mes
            var fechaHasta = DateTime.Now; // Fecha actual

            var Prestamos = _context.Prestamos
                .Where(x => new[] { 1, 2, 3, 6, 7 }.Contains(x.EstadoActual.Id) &&
                            x.FechaSolicitado >= fechaDesde &&
                            x.FechaSolicitado <= fechaHasta)
                .OrderByDescending(x => x.FechaSolicitado)
                .ToList();

            string UAT = LoginCGE(_context.Empresas.FirstOrDefault());

            List<PrestamoDTO> lista = new List<PrestamoDTO>();
            foreach (var prestamo in Prestamos)
            {

                MEstadoPrestamoDTO consulta = new MEstadoPrestamoDTO();
                consulta.UAT = UAT;
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
                _context.SaveChanges();
            }
        }


        private void ConfirmarPrestamo()
        {
            var fechaDesde = DateTime.Now.AddMonths(-1); // Fecha de hace un mes
            var fechaHasta = DateTime.Now; // Fecha actual

            var Prestamos = _context.Prestamos
                .Where(x => x.EstadoActual.Id == 6 &&
                            x.FechaSolicitado >= fechaDesde &&
                            x.FechaSolicitado <= fechaHasta)
                .OrderByDescending(x => x.FechaSolicitado)
                .ToList();

            string UAT = LoginCGE(_context.Empresas.FirstOrDefault());

            List<PrestamoDTO> lista = new List<PrestamoDTO>();

            foreach (var prestamo in Prestamos)
            {

                if (prestamo.Cliente.Persona.TipoPersona.Organismo.APIEjercito == true)
                //if (e == 1)
                {
                    var uatcge = new MEnviaOpcionesConfirmadasDTO();
                    uatcge.UAT = UAT;
                    uatcge.CantidadCuotas = prestamo.CantidadCuotas;
                    uatcge.ImporteCuota = prestamo.MontoCuota;
                    uatcge.ImportePrestado = prestamo.Capital;
                    uatcge.PrestamoId = prestamo.PrestamoCGEId;

                    //prestamo.Integracion = false;
                    uatcge.FirmaOlografica = prestamo.FirmaOlografica;
                    //uatcge.Token = prestamo.Token;
                    //uatcge.LegajoEntidad = TraeLegajoMutual(prestamo);
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
                            //   uat.Status = uatcge.Status;
                            if (uatcge.Status == 200)
                            {

                                prestamo.EstadoActual = _context.EstadosPrestamos.Find(2);
                                prestamo.FechaAprobacion = DateTime.Now;
                                if (prestamo.FirmaOlografica == null)
                                {
                                    prestamo.FirmaOlograficaConfirmacion = prestamo.FirmaOlografica;
                                    prestamo.FirmaOlografica = prestamo.FirmaOlografica;
                                }
                                else
                                {
                                    prestamo.FirmaOlograficaConfirmacion = prestamo.FirmaOlografica;
                                }
                                _context.Prestamos.Update(prestamo);
                                _context.SaveChanges();

                                string html = "";
                                html = "<br/>Estimado: " + prestamo.Cliente.Empresa.RazonSocial + "<br/><br/>";
                                html += "La opción crediticia ha sido aprobada por el Solicitante!<br/><br/>";
                                html += "<b>Persona:</b> " + prestamo.Cliente.Persona.Apellido.Trim() + ", " + prestamo.Cliente.Persona.Nombres.Trim() + " DNI: " + prestamo.Cliente.Persona.NroDocumento + "<br/>";
                                html += "<b>Importe Solicitado:</b> " + prestamo.Capital.ToString() + "<br/>";
                                html += "<b>Cantidad de Cuotas:</b> " + prestamo.CantidadCuotas.ToString() + "<br/>";
                                html += "<b>Monto de Cuota:</b> " + prestamo.MontoCuota.ToString() + "<br/><br/>";
                                html += "Sin Otro Particular Saludamos a Ud. Muy Atentamente<br/><br/>";
                                common.EnviarMail(prestamo.Cliente.Empresa.Mail, "Aprobación de Descuento Bot - Causante: " + prestamo.Cliente.Persona.Apellido.Trim() + ", " + prestamo.Cliente.Persona.Nombres.Trim(), html, "");
                                common.EnviarMail("rolando.d.ponce@hotmail.com", "Aprobación de Descuento Bot - Causante: " + prestamo.Cliente.Persona.Apellido.Trim() + ", " + prestamo.Cliente.Persona.Nombres.Trim(), html, "");


                            }
                        }
                    }
                }
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
    }
}
