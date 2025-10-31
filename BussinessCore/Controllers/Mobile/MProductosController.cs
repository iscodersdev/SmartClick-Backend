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
using System.Threading.Tasks;
using SmartClickCore.API.Controllers;
using DAL.Models.Core;
using MailKit.Search;

namespace SmartClick.Controllers
{
    [Route("api/[controller]")]
    public class MProductosController : BaseApiController
    {
        private readonly UserService<Usuario> _userManager;
        //private readonly BusinessCore.Services.NotificacionAPIService _notificacionAPIService;
        public SmartClickContext _context;
        public MProductosController(SmartClickContext context, UserService<Usuario> userManager) : base(context)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("TraeProveedores")]
        [EnableCors("CorsPolicy")]
        [AllowAnonymous]
        public MTraeProveedoresDTO TraeProveedores([FromBody] MTraeProveedoresDTO uat)
        {
            var Uat = _context.UAT.FirstOrDefault(x => x.Token == uat.UAT);
            if (Uat == null)
            {
                uat.Status = 500;
                uat.Mensaje = "UAT Invalida";
                return uat;
            }
            uat.Status = 200;
            uat.Mensaje = "Listado Proveedores";
            var Proveedores = _context.Proveedores.Where(x => x.Activo == true);
            List<ProveedoresDTO> lista = new List<ProveedoresDTO>();
            foreach (var Proveedor in Proveedores)
            {
                var renglon = new ProveedoresDTO();
                renglon.Id = Proveedor.Id;
                renglon.Nombre = Proveedor.Nombre;
                renglon.CUIT = Proveedor.CUIT.ToString();
                renglon.RazonSocial = Proveedor.RazonSocial;
                renglon.Domicilio = Proveedor.Domicilio.ToString();
                renglon.Foto = Proveedor.Foto;
                lista.Add(renglon);
            }
            uat.Proveedores = lista;
            return uat;
        }
        [HttpPost]
        [Route("TraeRubros")]
        [EnableCors("CorsPolicy")]
        [AllowAnonymous]
        public MTraeRubrosDTO TraeRubros([FromBody] MTraeRubrosDTO uat)
        {
            var Uat = _context.UAT.FirstOrDefault(x => x.Token == uat.UAT);
            if (Uat == null)
            {
                uat.Status = 500;
                uat.Mensaje = "UAT Invalida";
                return uat;
            }
            uat.Status = 200;
            uat.Mensaje = "Listado Rubros";
            var Rubros = _context.Rubros.Where(x => x.Activo == true).OrderBy(x => x.Nombre);
            List<RubrosDTO> lista = new List<RubrosDTO>();
            foreach (var Rubro in Rubros)
            {
                lista.Add(new RubrosDTO
                {
                    Id = Rubro.Id,
                    Nombre = Rubro.Nombre,
                    Descripcion = Rubro.Descripcion,
                    IconoAPP = Rubro.IconoAPP,
                    VerEnAPP = Rubro.VerEnAPP
                });
            }
            uat.Rubros = lista;
            return uat;
        }
        [HttpPost]
        [Route("TraeFotoProveedor")]
        [EnableCors("CorsPolicy")]
        [AllowAnonymous]
        public MTraeFotoProveedoresDTO TraeFotoProveedor([FromBody] MTraeFotoProveedoresDTO uat)
        {
            var Uat = _context.UAT.FirstOrDefault(x => x.Token == uat.UAT);
            if (Uat == null)
            {
                uat.Status = 500;
                uat.Mensaje = "UAT Invalida";
                return uat;
            }
            uat.Status = 200;
            uat.Mensaje = "Foto de Proveedor";
            var Proveedores = _context.Proveedores.Where(x => x.Activo == true && x.Id == uat.ProveedorId).FirstOrDefault();
            if (Proveedores != null)
            {
                uat.Foto = Proveedores.Foto;
            }
            return uat;
        }

        [HttpPost]
        [Route("TraeProductos")]
        [EnableCors("CorsPolicy")]
        [AllowAnonymous]
        public MTraeProductosDTO TraeProductos([FromBody] MTraeProductosDTO uat)
        {
            var Uat = _context.UAT.FirstOrDefault(x => x.Token == uat.UAT);
            if (Uat == null)
            {
                uat.Status = 500;
                uat.Mensaje = "UAT Invalida";
                return uat;
            }
            uat.Status = 200;
            uat.Mensaje = "Listado Productos";

            uat.Productos = _context.Productos
            .Where(x => x.Activo && x.Precio > 0)
            .Select(x => new MProductosDTO { Descripcion = x.Descripcion, DescripcionAmpliada = x.DescripcionAmpliada, Foto = x.Foto, Foto1 = x.Foto1, Foto2 = x.Foto2, Foto3 = x.Foto3, Foto4 = x.Foto4, Foto5 = x.Foto5, Id = x.Id, Precio = x.Precio })
            .ToList();

            return uat;
        }

        [HttpPost]
        [Route("TraeProductosPaginados")]
        [EnableCors("CorsPolicy")]
        [AllowAnonymous]
        public MTraeProductosPaginadosDTO TraeProductosPaginados([FromBody] MTraeProductosPaginadosDTO uat)
        {
            var Uat = _context.UAT.FirstOrDefault(x => x.Token == uat.UAT);
            if (Uat == null)
            {
                uat.Status = 500;
                uat.Mensaje = "UAT Invalida";
                return uat;
            }
            uat.Status = 200;
            uat.Mensaje = "Listado Productos";

            if(uat.Desde>0 && uat.Hasta>0)
            {
                var queryBase = _context.Productos
                    .Where(x => x.Activo && x.Precio > 0)
                    .Select(x => new MProductosDTO { Descripcion = x.Descripcion, DescripcionAmpliada = x.DescripcionAmpliada, Foto = x.Foto, Foto1 = x.Foto1, Foto2 = x.Foto2, Foto3 = x.Foto3, Foto4 = x.Foto4, Foto5 = x.Foto5, Id = x.Id, Precio = x.Precio })
                    .Skip(uat.Desde).Take(uat.Hasta);
                    uat.Productos = queryBase.ToList();
            }
            else
            {
                var queryBase = _context.Productos
                    .Where(x => x.Activo && x.Precio > 0)
                    .Select(x => new MProductosDTO { Descripcion = x.Descripcion, DescripcionAmpliada = x.DescripcionAmpliada, Foto = x.Foto, Foto1 = x.Foto1, Foto2 = x.Foto2, Foto3 = x.Foto3, Foto4 = x.Foto4, Foto5 = x.Foto5, Id = x.Id, Precio = x.Precio });
                    uat.Productos = queryBase.ToList();
            }
            return uat;
        }

        [HttpPost]
        [Route("TraeSubProductos")]
        [EnableCors("CorsPolicy")]
        [AllowAnonymous]
        public MTraeSubProductosDTO TraeSubProductos([FromBody] MTraeSubProductosDTO uat)
        {
            var Uat = _context.UAT.FirstOrDefault(x => x.Token == uat.UAT);
            if (Uat == null)
            {
                uat.Status = 500;
                uat.Mensaje = "UAT Invalida";
                return uat;
            }
            uat.Status = 200;
            uat.Mensaje = "Listado SubProductos";

            uat.SubProductos = _context.SubProductos
            .Where(x => x.Producto.Id == uat.ProductoId)
            .Select(x => new MSubProductosDTO { ProductoId = x.Producto.Id, NombreSubProducto = x.NombreSubProducto, Id = x.Id })
            .ToList();

            return uat;
        }


        [HttpPost]
        [Route("TraeProductosPorRubro")]
        [EnableCors("CorsPolicy")]
        [AllowAnonymous]
        public MTraeProductosPorRubroDTO TraeProductosPorRubro([FromBody] MTraeProductosPorRubroDTO uat)
        {
            var Uat = _context.UAT.FirstOrDefault(x => x.Token == uat.UAT);
            if (Uat == null)
            {
                uat.Status = 500;
                uat.Mensaje = "UAT Invalida";
                return uat;
            }
            uat.Status = 200;
            uat.Mensaje = "Listado Productos";

            if (uat.RubroId == 0)
            {
                uat.Productos = _context.Productos.Where(x => x.Activo && x.Precio > 0)
                .Select(x => new MProductosDTO { Descripcion = x.Descripcion, DescripcionAmpliada = x.DescripcionAmpliada, Foto = x.Foto, Foto1 = x.Foto1, Foto2 = x.Foto2, Foto3 = x.Foto3, Foto4 = x.Foto4, Foto5 = x.Foto5, Id = x.Id, Precio = x.Precio })
                .ToList();
            }
            else
            {
                uat.Productos = _context.Productos.Where(x => x.Rubro.Id == uat.RubroId)
                .Where(x => x.Activo && x.Precio > 0)
                .Select(x => new MProductosDTO { Descripcion = x.Descripcion, DescripcionAmpliada = x.DescripcionAmpliada, Foto = x.Foto, Foto1 = x.Foto1, Foto2 = x.Foto2, Foto3 = x.Foto3, Foto4 = x.Foto4, Foto5 = x.Foto5, Id = x.Id, Precio = x.Precio })
                .ToList();
            }



            return uat;
        }

        [HttpPost]
        [Route("TraeProductosComprados")]
        [EnableCors("CorsPolicy")]
        [AllowAnonymous]
        public MTraeComprasProductos TraeProductosComprados([FromBody] MTraeComprasProductos uat)
        {
            var Uat = _context.UAT.FirstOrDefault(x => x.Token == uat.UAT);
            if (Uat == null)
            {
                uat.Status = 500;
                uat.Mensaje = "UAT Invalida";
                return uat;
            }
            uat.Status = 200;
            uat.Mensaje = "Listado Productos";

            uat.Compras = _context.ComprasProductos.Where(x => x.Cliente.Id == Uat.Cliente.Id)
            .Select(x => new MComprasDTO
            {
                Estado = x.EstadoCompra.Id,
                EstadoDescripcion = x.EstadoCompra.Estado.ToString(),
                FechaCompra = x.FechaCompra,
                FechaAnulacion = x.FechaAnulacion,
                Id = x.Id,
                TipoCompra = x.TipoCompra,
                TipoCompraDescripcion = x.TipoCompra.ToString(),
                ProductoId = x.Producto.Id,
                Descripcion = x.Producto.Descripcion,
                DescripcionAmpliada = x.Producto.DescripcionAmpliada,
                Precio = x.Producto.Precio,
                Foto = x.Producto.Foto
            }).ToList();

            return uat;
        }

        [HttpPost]
        [Route("TraeProductosFiltrados")]
        [EnableCors("CorsPolicy")]
        [AllowAnonymous]
        public MTraeProductosFiltradosDTO TraeProductosFiltrados([FromBody] MTraeProductosFiltradosDTO uat)
        {
            try
            {
                var Uat = _context.UAT.FirstOrDefault(x => x.Token == uat.UAT);
                if (Uat == null)
                {
                    uat.Status = 500;
                    uat.Mensaje = "UAT Invalida";
                    return uat;
                }
                uat.Status = 200;
                uat.Mensaje = "Listado Productos";

                uat.Productos = _context.Productos
                .Where(x => x.Activo && x.Precio > 0 && x.Descripcion.Contains(uat.Producto))
                .Select(x => new MProductosDTO { Descripcion = x.Descripcion, DescripcionAmpliada = x.DescripcionAmpliada, Foto = x.Foto, Foto1 = x.Foto1, Foto2 = x.Foto2, Foto3 = x.Foto3, Foto4 = x.Foto4, Foto5 = x.Foto5, Id = x.Id, Precio = x.Precio })
                .ToList();

                return uat;
            }
            catch (Exception e)
            {
                uat.Status = 500;
                uat.Mensaje = "Error - "+e.Message;
                return uat;
            }
        }



        [HttpPost]
        [Route("CompraPorDebito")]
        [EnableCors("CorsPolicy")]
        public IActionResult ComprarProductoDebito([FromBody] MCompraProductoDTO CompraProducto)
        {
            try
            {
                Billetera billeteraPagadora = TraeBilleteraCliente(TraeClienteUAT(CompraProducto.UAT));

                Producto producto = _context.Productos.FirstOrDefault(x => x.Id == CompraProducto.ProductoId);


                if (billeteraPagadora.ChequeaDebito(producto.Precio))
                {

                    Compras compra = new Compras();

                    if (CompraProducto.SubProductoId > 0)
                    {
                        SubProducto subproducto = _context.SubProductos.FirstOrDefault(x => x.Id == CompraProducto.SubProductoId);

                        compra.SubProducto = subproducto;
                    }

                    var EstadoCompra = _context.EstadoCompra.Find(2); //Efectuado
                    var TipoCompra = _context.TipoCompra.Find(3); // Debito
                    compra.Cliente = billeteraPagadora.Cliente;
                    compra.Producto = producto;
                    compra.FechaCompra = DateTime.Now;
                    compra.EstadoCompra = EstadoCompra;
                    compra.TipoCompra = TipoCompra;

                    MovimientoBilletera movimientoBilletera = new MovimientoBilletera();
                    //Agregar Movimiento Billetera

                    billeteraPagadora.Saldo -= producto.Precio;
                    _context.Update(billeteraPagadora);
                    _context.ComprasProductos.Add(compra);
                    _context.SaveChanges();
                    //_notificacionAPIService.Envia_Push(billeteraPagadora.Cliente.Usuario.DeviceId, "Compra de Producto", $"Ha abonado ${producto.Precio} desde su billetera para la compra de un Producto");
                    return new JsonResult(new RespuestaAPI { Status = 200, UAT = CompraProducto.UAT, Mensaje = "Compra ejecutada satifactoriamente" });
                }
                else
                {
                    return new JsonResult(new RespuestaAPI { Status = 400, UAT = CompraProducto.UAT, Mensaje = "El monto supera su saldo y el Producto no es Financiable" });
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new RespuestaAPI { Status = 500, UAT = CompraProducto.UAT, Mensaje = "Error al Comprar el Producto" });
            }
        }



        [HttpPost]
        [Route("CompraPorFinanciamiento")]
        [EnableCors("CorsPolicy")]
        public MCompraProductoDTO ComprarProductoFinanciamiento([FromBody] MCompraProductoDTO CompraProducto)
        {
            try
            {
                Billetera billeteraPagadora = TraeBilleteraCliente(TraeClienteUAT(CompraProducto.UAT));

                Producto producto = _context.Productos.FirstOrDefault(x => x.Id == CompraProducto.ProductoId);

                if (producto.Financiable)
                {

                    LineasPrestamosTiposPersonas LineaTipoPersona = _context.LineasPrestamosTiposPersonas.Where(x => x.TipoPersona.Id == billeteraPagadora.Cliente.Persona.TipoPersona.Id).FirstOrDefault();
                    IQueryable<LineasPrestamosPlanes> planes;

                    planes = _context.LineasPrestamosPlanes.Where(x => x.Linea.Id == LineaTipoPersona.LineaPrestamo.Id && (x.MontoPrestado >= producto.Precio)
                                        // && x.MontoCuota <= (x.MargenDisponible) 
                                        //&& x.MontoCuota <= billeteraPagadora.Cliente.Persona.TipoPersona.TopePrestamo 
                                        && x.CantidadCuotas <= billeteraPagadora.Cliente.Persona.TipoPersona.LimiteCuotas
                                        && (x.MontoPrestado >= producto.Precio) && x.Activo == true).OrderBy(x => x.MontoPrestado).ThenByDescending(x => x.CantidadCuotas);
                    CompraProducto.Mensaje = "Disponible Para Realizar Prestamo";

                    if (planes.Count() == 0)
                    {
                        if (billeteraPagadora.Cliente.Persona.TipoPersona.TopePrestamo <= producto.Precio && (billeteraPagadora.Cliente.Persona.TipoPersona.TopePrestamo + billeteraPagadora.Saldo) >= producto.Precio)
                        {
                            planes = _context.LineasPrestamosPlanes.Where(x => x.Linea.Id == LineaTipoPersona.LineaPrestamo.Id
                                        //&& x.MontoCuota <= (disponible.Disponible - x.MargenDisponible) 
                                        //&& x.MontoCuota <= billeteraPagadora.Cliente.Persona.TipoPersona.TopePrestamo
                                        && x.CantidadCuotas <= billeteraPagadora.Cliente.Persona.TipoPersona.LimiteCuotas
                                        && (x.MontoPrestado == billeteraPagadora.Cliente.Persona.TipoPersona.TopePrestamo) && x.Activo == true).OrderByDescending(x => x.MontoPrestado).ThenByDescending(x => x.CantidadCuotas);
                            CompraProducto.Mensaje = "Disponible Para Realizar Prestamo más los Fondos de Billetera";
                        }
                        else
                        {
                            CompraProducto.Status = 500;
                            CompraProducto.Mensaje = "No posee Prestamos Disponibles ni Fondos en la Billetera.";
                            return CompraProducto;
                        }
                    }

                    decimal montofijo = 0;
                    if (producto.Precio > 0)
                    {
                        montofijo = planes.OrderBy(x => x.MontoPrestado).FirstOrDefault().MontoPrestado;
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
                            lista.Add(nuevo);
                        }
                    }
                    CompraProducto.PlanesDisponibles = lista;
                    CompraProducto.Status = 200;
                    return CompraProducto;
                }
                else
                {
                    CompraProducto.Mensaje = "El Producto no es Financiable";
                    CompraProducto.Status = 400;
                    return CompraProducto;
                }
            }
            catch (Exception e)
            {
                CompraProducto.Mensaje = "Error al Comprar el Producto";
                CompraProducto.Status = 500;
                return CompraProducto;
            }
        }

        [HttpPost]
        [Route("Financiar")]
        [EnableCors("CorsPolicy")]
        public MSolicitaPrestamoProductoDTO FinanciarProducto([FromBody] MSolicitaPrestamoProductoDTO uat)
        {
            try
            {
                Billetera billeteraPagadora = TraeBilleteraCliente(TraeClienteUAT(uat.UAT));
                Producto producto = _context.Productos.FirstOrDefault(x => x.Id == uat.ProductoId);


                if (producto.Financiable)
                {
                    var Uat = _context.UAT.FirstOrDefault(x => x.Token == uat.UAT);
                    if (uat == null)
                    {
                        uat.Status = 500;
                        uat.Mensaje = "UAT Invalida";
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
                        MSolicitaPrestamoCGEDTO solicitud = new MSolicitaPrestamoCGEDTO();
                        solicitud.DNI = Convert.ToInt64(Uat.Cliente.Persona.NroDocumento);
                        solicitud.EntidadId = Uat.Cliente.Empresa.EntidadIdCGE;
                        solicitud.FotoDNIAnverso = uat.FotoDNIAnverso;
                        solicitud.FotoDNIReverso = uat.FotoDNIReverso;
                        solicitud.FotoSosteniendoDNI = uat.FotoSosteniendoDNI;
                        solicitud.ImporteSolicitado = uat.ImporteSolicitado;
                        solicitud.TipoPrestamoId = linea.TipoDescuentoCGEId;
                        solicitud.MontoCuota = uat.MontoCuota;
                        solicitud.CantidadCuotas = uat.CantidadCuotas;
                        solicitud.FirmaOlografica = uat.FirmaOlografica;
                        solicitud.UAT = LoginCGE(Uat.Cliente.Empresa);
                        solicitud.EntidadId = Uat.Cliente.Empresa.EntidadIdCGE;
                        solicitud.Precancelaciones = uat.Precancelaciones;
                        client.BaseAddress = new Uri("https://www.cge.mil.ar:81/api/mentidades/");
                        HttpResponseMessage response = client.PostAsJsonAsync("SolicitaPrestamo", solicitud).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var readTask = response.Content.ReadAsAsync<MSolicitaPrestamoCGEDTO>();
                            readTask.Wait();
                            solicitud = readTask.Result;
                            if (solicitud.Status == 200)
                            {
                                Prestamos prestamo = new Prestamos();
                                prestamo.FechaSolicitado = DateTime.Now;
                                prestamo.Capital = solicitud.ImporteSolicitado;
                                prestamo.Cliente = Uat.Cliente;
                                prestamo.Linea = linea;
                                prestamo.EstadoActual = _context.EstadosPrestamos.Find(1);
                                prestamo.PrestamoCGEId = solicitud.PrestamoCGEId;
                                prestamo.FirmaOlografica = uat.FirmaOlografica;
                                prestamo.Cliente.FotoDNIAnverso = uat.FotoDNIAnverso;
                                prestamo.Cliente.FotoDNIReverso = uat.FotoDNIReverso;
                                prestamo.Cliente.FotoSosteniendoDNI = uat.FotoSosteniendoDNI;
                                prestamo.CantidadCuotas = uat.CantidadCuotas;
                                prestamo.MontoCuota = uat.MontoCuota;
                                prestamo.CFT = _context.LineasPrestamosPlanes.FirstOrDefault(x => x.Linea.Id == linea.Id).CFT;
                                prestamo.Tipos = TipoPrestamo.CompraBienes;
                                //prestamo.CFT=common.CalculaCFT(Convert.ToDouble(uat.ImporteSolicitado), uat.CantidadCuotas, Convert.ToDouble(uat.MontoCuota));
                                _context.Prestamos.Add(prestamo);
                                _context.SaveChanges();
                                string html = "";
                                html = "<br/>Estimado: " + prestamo.Cliente.Empresa.RazonSocial + "<br/><br/>";
                                html += "Nos Agrada Comunicarle que ha recibido en su bandeja de Haberes 2.0 la siguiente solicitud de descuento por Decreto 14/12 segun detalle:<br/><br/>";
                                html += "<b>Persona:</b> " + prestamo.Cliente.Persona.Apellido.Trim() + ", " + prestamo.Cliente.Persona.Nombres.Trim() + " DNI: " + prestamo.Cliente.Persona.NroDocumento + "<br/>";
                                html += "<b>Importe Solicitado:</b> " + prestamo.Capital.ToString() + "<br/>";
                                html += "<b>Cantidad de Cuotas:</b> " + prestamo.CantidadCuotas.ToString() + "<br/>";
                                html += "<b>Monto de Cuota:</b> " + prestamo.MontoCuota.ToString() + "<br/><br/>";
                                html += "Sin Otro Particular Saludamos a Ud. Muy Atentamente<br/><br/>";
                                common.EnviarMail(prestamo.Cliente.Empresa.Mail, "Solicitud de Descuento - Causante: " + prestamo.Cliente.Persona.Apellido.Trim() + ", " + prestamo.Cliente.Persona.Nombres.Trim(), html, "");
                                //uat.LegajoElectronico = solicitud.LegajoElectronico;


                                Compras compra = new Compras();

                                if (uat.SubProductoId > 0)
                                {
                                    SubProducto subproducto = _context.SubProductos.FirstOrDefault(x => x.Id == uat.SubProductoId);
                                    compra.SubProducto = subproducto;
                                }

                                var EstadoCompra = _context.EstadoCompra.Find(1); //Pendiente
                                var TipoCompra = _context.TipoCompra.Find(1); //Financiado

                                compra.Cliente = billeteraPagadora.Cliente;
                                compra.Producto = producto;
                                compra.FechaCompra = DateTime.Now;
                                compra.EstadoCompra = EstadoCompra;
                                compra.TipoCompra = TipoCompra;

                                Venta venta = new Venta();
                                var prestamos = _context.Prestamos.Where(x => x.PrestamoCGEId == solicitud.PrestamoCGEId);
                                venta.Producto = producto;
                                venta.Prestamo = (Prestamos)prestamos;


                                MovimientoBilletera movimientoBilletera = new MovimientoBilletera();
                                //Agregar Movimiento Billetera
                                _context.Update(billeteraPagadora);
                                _context.ComprasProductos.Add(compra);
                                _context.Venta.Add(venta);

                            }
                            else
                            {
                                uat.Status = 500;
                                uat.Mensaje = solicitud.Mensaje;
                                return uat;
                            }
                        }
                        else
                        {
                            uat.Mensaje = "Error de API";
                            uat.Status = 500;
                            return uat;
                        }

                    }

                    return uat;
                }
                else
                {
                    uat.Mensaje = "El Producto no es Financiable";
                    uat.Status = 500;
                    return uat;
                }


            }
            catch (Exception e)
            {
                uat.Mensaje = "Error de API";
                uat.Status = 500;
                return uat;
            }
        }

        public bool RegitrarPrestamoOtroOrganismo(MSolicitaPrestamoProductoDTO uat, UAT Uat, LineasPrestamos linea)
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
                //prestamo.FotoCertificadoDescuento = uat.FotoCertificadoDescuento;
                prestamo.CantidadCuotas = uat.CantidadCuotas;
                prestamo.MontoCuota = uat.MontoCuota;
                prestamo.CFT = _context.LineasPrestamosPlanes.FirstOrDefault(x => x.Linea.Id == linea.Id).CFT;
                //prestamo.CFT = common.CalculaCFT(Convert.ToDouble(uat.ImporteSolicitado), uat.CantidadCuotas, Convert.ToDouble(uat.MontoCuota));
                _context.Prestamos.Add(prestamo);
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
                //common.EnviarMail(prestamo.Cliente.Empresa.Mail, "Solicitud de Descuento - Causante: " + prestamo.Cliente.Persona.Apellido.Trim() + ", " + prestamo.Cliente.Persona.Nombres.Trim(), html, "");

                Compras compra = new Compras();

                if (uat.SubProductoId > 0)
                {
                    SubProducto subproducto = _context.SubProductos.FirstOrDefault(x => x.Id == uat.SubProductoId);
                    compra.SubProducto = subproducto;
                }
                var producto = _context.Productos.Find(uat.ProductoId);
                var EstadoCompra = _context.EstadoCompra.Find(1); //Pendiente
                var TipoCompra = _context.TipoCompra.Find(1); //Financiado
                compra.Cliente = Uat.Cliente;
                compra.Producto = producto;
                compra.FechaCompra = DateTime.Now;
                compra.EstadoCompra = EstadoCompra;
                compra.TipoCompra = TipoCompra;

                Venta venta = new Venta();
                var prestamos = _context.Prestamos.FirstOrDefault(x => x.Cliente == prestamo.Cliente & x.FechaSolicitado == prestamo.FechaSolicitado);
                venta.Producto = producto;
                venta.Prestamo = prestamos;

                //Agregar Movimiento Billetera
                _context.ComprasProductos.Add(compra);
                _context.Venta.Add(venta);
                _context.SaveChanges();

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







    }


}

