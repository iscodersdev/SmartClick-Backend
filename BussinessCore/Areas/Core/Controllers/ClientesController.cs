﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SmartClickCore.Controllers;
using Commons.Identity.Services;
using Commons.Models;
using DAL.Data;
using DAL.DTOs;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace SmartClickCore.Areas.Core.Controllers
{
    [Area("Core")]

    public class ClientesController : SmartClickCoreController
    {
        private readonly UserService<Usuario> _userService;
        public ClientesController(SmartClickContext context, UserService<Usuario> userService) : base(context)
        {
            breadcumb.Add(new Message() { DisplayName = "Gestión" });
            _userService = userService;
        }
        public ActionResult Index()
        {
            breadcumb.Add(new Message() { DisplayName = "Clientes" });
            ViewBag.Breadcrumb = breadcumb;
            //var empresa = _context.Empresas.FirstOrDefault();
            //var Clientes = _context.Clientes.Where(x => x.Persona.TipoPersona.Organismo.Id == 1 && x.Usuario.UserName != null && x.NumeroLegajoLaboral == null && x.Usuario.PhoneNumberConfirmed ==true ).ToList();

            //foreach (var cliente in Clientes)
            //{
            //    int codigo = 0;
            //    SmartClickCore.SmartClick.ActualizaPersonaCGE20(empresa, cliente, codigo.ToString(), _context);
            //}
            return View();
        }

        public IActionResult ClienteDataTable()
        {
            var clientes = from p in _context.Set<Clientes>()
                           //join pers in _context.Set<Persona>() on p.Persona.Id equals pers.Id
                           where p.FechaBaja == null && p.EsUsuarioInterno == false select p;

             var query = from p in clientes
                         where p.EsUsuarioInterno == false
                        select new ClienteDTO
                        {
                            Id = p.Id,
                            NombreCompleto =(p.Persona!=null)?p.Persona.GetNombreCompleto():"---",
                            DNI = p.Persona.NroDocumento,
                            //NombreCompleto =(p.Persona!=null)?p.Persona.GetNombreCompleto():"---",
                            //DNI = (p.Persona != null)? p.Persona.NroDocumento:"---",
                            FechaIngreso = (p.FechaIngreso.ToShortDateString()!=null)? p.FechaIngreso.ToShortDateString():"-----",
                            Estado = p.ClienteValidado,
                            BloquearPrestamos = p.BloquearPrestamos
                        };
            return DataTable(query.AsQueryable<ClienteDTO>());
        }

        public ActionResult _Create()
        {
            ClienteViewBag(null);
            Clientes model = new Clientes();
            return View(model);
        }
        public IActionResult ClienteCombo(string term, int? id)
        {
            term = term.ToUpper();
            var items = _context.Clientes
                .Where(x => x.FechaBaja == null)
                .Where(x => (x.Persona.Nombres.ToUpper() + " " + x.Persona.Apellido.ToUpper()).Contains(term.ToUpper()));
            if (id != null)
            {
                items = items.Where(x => x.Id != id);
            }

            var list = items.Select(x => new
            {
                text = $"{x.Persona.Nombres.ToUpper() + " " + x.Persona.Apellido.ToUpper()}",
                id = x.Id
            })
            .Take(30).ToList();

            return Json(list);
        }

        public IActionResult UsuarioCombo(string term, string id)
        {
            term = term.ToUpper();
            var items = _context.Usuarios
                .Where(x => (x.Clientes.Persona.Nombres.ToUpper() + " " + x.Clientes.Persona.Apellido.ToUpper()).Contains(term.ToUpper()));
            if (id != null)
            {
                items = items.Where(x => x.Id != id);
            }

            var list = items.Select(x => new
            {
                text = $"{x.Clientes.Persona.Nombres.ToUpper() + " " + x.Clientes.Persona.Apellido.ToUpper()}",
                id = x.Id
            })
            .Take(30).ToList();

            return Json(list);
        }

        [HttpPost]
        public async Task<ActionResult> _Create([Bind("Provincia,Localidad,CodigoPostal,TipoCliente,Persona,Usuario,ReferenciaA,ReferenciaB,Persona.TipoDocumento,Persona.Pais,Persona.NroDocumento,Persona.Cuil,RazonSocial,Persona.Apellido,Persona.Nombres,Domicilio,Altura,CBU,Telefono,Celular,NumeroCliente,Persona.FechaNacimiento,Usuario.Mail,Empresa,Persona.EstadoCivil,Persona.CantidadHijos,FechaIngreso,FechaIngresoLaboral,CategoriaLaboral,DestinoLaboral,NumeroAsociado,Codeudor,PersonaPoliticamenteExpuesta,Persona.TipoPersona,DependeDe,NumeroLegajoLaboral,EsMilitar,RecibirPublicidad,ReferenciaA.NombreCompleto,ReferenciaA.Vinculo,ReferenciaA.Telefono,ReferenciaB.NombreCompleto,ReferenciaB.Vinculo,ReferenciaB.Telefono,Persona.LugarNacimiento")] Clientes cliente)
        {
            try
            {
                if (cliente.Persona.NroDocumento == null)
                {
                    ModelState.AddModelError("Persona.NroDocumento", "Debe ingresar el Numero de Documento del Cliente.");
                }
                else
                {
                    var usuarioCliente = _context.Usuarios.Where(x => x.Clientes.Persona.NroDocumento == cliente.Persona.NroDocumento.ToString()).FirstOrDefault();
                    if (usuarioCliente != null)
                    {
                        ModelState.AddModelError("Persona.NroDocumento", "Ya existe un Cliente con el Numero de Documento Ingresado");
                    }
                }
                if (cliente.TipoCliente == null)
                {
                    ModelState.AddModelError("TipoCliente", "Debe seleccionar un Tipo de Cliente");
                }
                if (cliente.Persona.TipoDocumento == null)
                {
                    ModelState.AddModelError("Persona.TipoDocumento", "Debe seleccionar un Tipo de Documento");
                }
                else
                {
                    ModelState.Remove("Persona.TipoDocumento.Descripcion");
                }
                if (cliente.Persona.Pais == null)
                {
                    ModelState.AddModelError("Persona.Pais", "Debe seleccionar un Pais");
                }
                if (cliente.Empresa == null)
                {
                    ModelState.AddModelError("Empresa", "Debe seleccionar una Empresa");
                }
                if (cliente.Persona.EstadoCivil == null)
                {
                    ModelState.AddModelError("Persona.EstadoCivil", "Debe seleccionar un Estado Civil");
                }
                if (cliente.Persona.TipoPersona == null)
                {
                    ModelState.AddModelError("Persona.TipoPersona", "Debe seleccionar un Tipo de Persona");
                }
                ModelState.Remove("Id");
                ModelState.Remove("DependeDe.Id");
                ModelState.Remove("Codeudor.Id");
                ModelState.Remove("Persona.Id");
                ModelState.Remove("Persona.Genero.Descripcion");
                ModelState.Remove("ReferenciaA.Id");
                ModelState.Remove("ReferenciaB.Id");
                ModelState.Remove("Provincia.Id");
                ModelState.Remove("Localidad.Id");
                ModelState.Remove("Persona.LugarNacimiento.Id");
                if (ModelState.IsValid)
                {
                    await _context.Referencias.AddAsync(cliente.ReferenciaA);
                    await _context.Referencias.AddAsync(cliente.ReferenciaB);
                    cliente.TipoCliente = await _context.TiposClientes.FindAsync(cliente.TipoCliente.Id);
                    if (cliente.DependeDe != null && cliente.DependeDe.Id != 0)
                    {
                        cliente.DependeDe = await _context.Clientes.FindAsync(cliente.DependeDe.Id);
                    }
                    else
                    {
                        cliente.DependeDe = null;
                    }
                    cliente.Empresa = await _context.Empresas.FindAsync(cliente.Empresa.Id);
                    if (cliente.Codeudor != null && cliente.Codeudor.Id != 0)
                    {
                        cliente.Codeudor = await _context.Clientes.FindAsync(cliente.Codeudor.Id);
                    }
                    else
                    {
                        cliente.Codeudor = null;
                    }
                    cliente.ClienteValidado = true;
                    if (cliente.Provincia != null)
                    {
                        cliente.Provincia = await _context.Provincia.FindAsync(cliente.Provincia.Id);
                    }
                    if (cliente.Localidad != null)
                    {
                        cliente.Localidad = await _context.Localidad.FindAsync(cliente.Localidad.Id);
                    }
                    cliente.Persona.TipoDocumento = await _context.TipoDocumento.FindAsync(cliente.Persona.TipoDocumento.Id);
                    cliente.Persona.Pais = await _context.Paises.FindAsync(cliente.Persona.Pais.Id);
                    cliente.Persona.EstadoCivil = await _context.EstadosCiviles.FindAsync(cliente.Persona.EstadoCivil.Id);
                    cliente.Persona.TipoPersona = await _context.TiposPersonas.FindAsync(cliente.Persona.TipoPersona.Id);
                    cliente.Persona.Genero = await _context.Genero.FindAsync(cliente.Persona.Genero.Id);
                    cliente.Persona.LugarNacimiento = await _context.Provincia.FindAsync(cliente.Persona.LugarNacimiento.Id);

                    cliente.Usuario = new Usuario()
                    {
                        UserName = cliente.Usuario.Mail,
                        Email = cliente.Usuario.Mail,
                        Mail = cliente.Usuario.Mail,
                    };
                    var result = await _userService.CreateAsync(cliente.Usuario, cliente.Persona.NroDocumento.ToString());

                    cliente.UsuarioId = cliente.Usuario.Id;

                    var usuarioLogueado = _context.Users.Where(x => x.UserName == User.Identity.Name).First();
                    cliente.CreadoPorUsuarioId = usuarioLogueado.Id;
                    cliente.FechaCreacionModificacion = DateTime.Now;
                    cliente.Persona.CreadoPorUsuarioId =  usuarioLogueado.Id;
                    cliente.Persona.FechaCreacionModificacion = DateTime.Now;

                    await _context.Personas.AddAsync(cliente.Persona);
                    await _context.Clientes.AddAsync(cliente);
                    Usuario user = await _context.Usuarios.FindAsync(cliente.UsuarioId);
                    user.Personas = cliente.Persona;
                    _context.Usuarios.Update(user);
                    await _context.SaveChangesAsync();
                    AddPageAlerts(PageAlertType.Success, "Se cargo correctamente el Cliente.");
                    return RedirectToAction("Index", "Clientes");
                }
                else
                {
                    if (cliente.DependeDe != null && cliente.DependeDe.Id != 0)
                    {
                        cliente.DependeDe = await _context.Clientes.FindAsync(cliente.DependeDe.Id);
                    }
                    if (cliente.Codeudor != null && cliente.Codeudor.Id != 0)
                    {
                        cliente.Codeudor = await _context.Clientes.FindAsync(cliente.Codeudor.Id);
                    }
                    ClienteViewBag(cliente);
                    return View(cliente);
                }

            }
            catch (Exception e)
            {
                AddPageAlerts(PageAlertType.Error, "Hubo un error al cargar el Cliente. Intentelo nuevamente mas tarde.");
                return RedirectToAction("Index", "Clientes");
            }
        }
        public IActionResult Delete(int id)
        {
            try
            {
                Clientes cliente = _context.Clientes.Where(s => s.Id == id).First();
                cliente.Usuario.Clientes = null;
                cliente.Usuario = null;
                cliente.ReferenciaA = null;
                if (cliente.ReferenciaA != null)
                {
                    cliente.ReferenciaA = null;
                    _context.Referencias.Remove(cliente.ReferenciaA);
                }
                if (cliente.ReferenciaB != null)
                {
                    cliente.ReferenciaB = null;
                    _context.Referencias.Remove(cliente.ReferenciaB);
                }                
                _context.Clientes.Remove(cliente);
                _context.SaveChanges();
                AddPageAlerts(PageAlertType.Success, "Se dio de Baja correctamente al Cliente.");
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception)
            {
                AddPageAlerts(PageAlertType.Success, "Hubo un error al dar de baja al Cliente.");
                return RedirectToAction("Index", "Clientes");
            }
        }

        public ActionResult _Update(int id)
        {
            Clientes cliente = _context.Clientes.Where(s => s.Id == id).First();
            ClienteViewBag(cliente);
            return View(cliente);
        }
        [HttpPost]
        public async Task<ActionResult> _Update(Clientes cliente)
        {
            try
            {
                if (cliente.TipoCliente == null)
                {
                    ModelState.AddModelError("TipoCliente", "Debe seleccionar un Tipo de Cliente");
                }
                if (cliente.Persona.TipoDocumento == null)
                {
                    ModelState.AddModelError("Persona.TipoDocumento", "Debe seleccionar un Tipo de Documento");
                }
                else
                {
                    ModelState.Remove("Persona.TipoDocumento.Descripcion");
                }
                if (cliente.Persona.Pais == null)
                {
                    ModelState.AddModelError("Persona.Pais", "Debe seleccionar un Pais");
                }
                if (cliente.Empresa == null)
                {
                    ModelState.AddModelError("Empresa", "Debe seleccionar una Empresa");
                }
                if (cliente.Persona.EstadoCivil == null)
                {
                    ModelState.AddModelError("Persona.EstadoCivil", "Debe seleccionar un Estado Civil");
                }
                if (cliente.Persona.TipoPersona == null)
                {
                    ModelState.AddModelError("Persona.TipoPersona", "Debe seleccionar un Tipo de Persona");
                }
                ModelState.Remove("DependeDe");
                ModelState.Remove("Codeudor.id");
                ModelState.Remove("Provincia.id");
                ModelState.Remove("Localidad.id");
                ModelState.Remove("Persona.Genero.Descripcion");
                if (ModelState.IsValid)
                {
                    Clientes clienteEditar = await _context.Clientes.FindAsync(cliente.Id);
                    if (clienteEditar.ReferenciaA != null)
                    {
                        clienteEditar.ReferenciaA.NombreCompleto = cliente.ReferenciaA.NombreCompleto;
                        clienteEditar.ReferenciaA.Vinculo = cliente.ReferenciaA.Vinculo;
                        clienteEditar.ReferenciaA.Telefono = cliente.ReferenciaA.Telefono;
                    }
                    else
                    {
                        await _context.Referencias.AddAsync(cliente.ReferenciaA);
                        clienteEditar.ReferenciaA = cliente.ReferenciaA;
                    }
                    if (clienteEditar.ReferenciaB != null)
                    {
                        clienteEditar.ReferenciaB.NombreCompleto = cliente.ReferenciaB.NombreCompleto;
                        clienteEditar.ReferenciaB.Vinculo = cliente.ReferenciaB.Vinculo;
                        clienteEditar.ReferenciaB.Telefono = cliente.ReferenciaB.Telefono;
                    }
                    else
                    {
                        await _context.Referencias.AddAsync(cliente.ReferenciaB);
                        clienteEditar.ReferenciaB = cliente.ReferenciaB;
                    }
                    if (cliente.Provincia != null)
                    {
                        clienteEditar.Provincia = await _context.Provincia.FindAsync(cliente.Provincia.Id);
                    }
                    if (cliente.Localidad != null)
                    {
                        clienteEditar.Localidad = await _context.Localidad.FindAsync(cliente.Localidad.Id);
                    }
                    clienteEditar.RazonSocial = cliente.RazonSocial;
                    clienteEditar.NumeroCliente = cliente.NumeroCliente;
                    clienteEditar.Domicilio = cliente.Domicilio;
                    clienteEditar.Altura = cliente.Altura;
                    clienteEditar.CodigoPostal = cliente.CodigoPostal;
                    clienteEditar.CBU = cliente.CBU;
                    clienteEditar.Telefono = cliente.Telefono;
                    clienteEditar.Celular = cliente.Celular;
                    clienteEditar.FechaIngresoLaboral = cliente.FechaIngresoLaboral;
                    clienteEditar.NumeroLegajoLaboral = cliente.NumeroLegajoLaboral;
                    clienteEditar.CategoriaLaboral = cliente.CategoriaLaboral;
                    clienteEditar.DestinoLaboral = cliente.DestinoLaboral;
                    clienteEditar.NumeroAsociado = cliente.NumeroAsociado;
                    clienteEditar.PersonaPoliticamenteExpuesta = cliente.PersonaPoliticamenteExpuesta;
                    clienteEditar.EsMilitar = cliente.EsMilitar;
                    clienteEditar.FechaIngreso = cliente.FechaIngreso;
                    clienteEditar.ClienteValidado = true;
                    clienteEditar.RecibirPublicidad = cliente.RecibirPublicidad;
                    if (cliente.DependeDe != null && cliente.DependeDe.Id != 0)
                    {
                        clienteEditar.DependeDe = await _context.Clientes.FindAsync(cliente.DependeDe.Id);
                    }
                    else
                    {
                        clienteEditar.DependeDe = null;
                    }
                    if (cliente.Codeudor != null && cliente.Codeudor.Id != 0)
                    {
                        clienteEditar.Codeudor = await _context.Clientes.FindAsync(cliente.Codeudor.Id);
                    }
                    else
                    {
                        clienteEditar.Codeudor = null;
                    }
                    clienteEditar.TipoCliente = await _context.TiposClientes.FindAsync(cliente.TipoCliente.Id);
                    clienteEditar.Empresa = await _context.Empresas.FindAsync(cliente.Empresa.Id);

                    clienteEditar.Persona.Genero = await _context.Genero.FindAsync(cliente.Persona.Genero.Id);
                    clienteEditar.Persona.TipoDocumento = await _context.TipoDocumento.FindAsync(cliente.Persona.TipoDocumento.Id);
                    clienteEditar.Persona.Pais = await _context.Paises.FindAsync(cliente.Persona.Pais.Id);
                    clienteEditar.Persona.NroDocumento = cliente.Persona.NroDocumento;
                    clienteEditar.Persona.Cuil = cliente.Persona.Cuil;
                    clienteEditar.Persona.Apellido = cliente.Persona.Apellido;
                    clienteEditar.Persona.Nombres = cliente.Persona.Nombres;
                    clienteEditar.Persona.FechaNacimiento = cliente.Persona.FechaNacimiento;
                    clienteEditar.Persona.CantidadHijos = cliente.Persona.CantidadHijos;
                    clienteEditar.Persona.EstadoCivil = await _context.EstadosCiviles.FindAsync(cliente.Persona.EstadoCivil.Id);
                    clienteEditar.Persona.TipoPersona = await _context.TiposPersonas.FindAsync(cliente.Persona.TipoPersona.Id);
                    clienteEditar.Persona.LugarNacimiento = await _context.Provincia.FindAsync(cliente.Persona.LugarNacimiento.Id);
                    //clienteEditar.Usuario.Mail = cliente.Usuario.Mail;
                    //clienteEditar.Usuario.UserName = cliente.Usuario.Mail;
                    //clienteEditar.Usuario.NormalizedUserName = cliente.Usuario.Mail;
                    //clienteEditar.Usuario.Email = cliente.Usuario.Mail;
                    //clienteEditar.Usuario.NormalizedEmail = cliente.Usuario.Mail;

                    var usuarioLogueado = _context.Users.Where(x => x.UserName == User.Identity.Name).First();
                    clienteEditar.CreadoPorUsuarioId = usuarioLogueado.Id;
                    clienteEditar.FechaCreacionModificacion = DateTime.Now;
                    clienteEditar.Persona.CreadoPorUsuarioId =  usuarioLogueado.Id;
                    clienteEditar.Persona.FechaCreacionModificacion = DateTime.Now;

                    _context.Clientes.Update(clienteEditar);
                    Usuario user = await _context.Usuarios.FindAsync(clienteEditar.UsuarioId);
                    user.Personas = clienteEditar.Persona;
                    _context.Usuarios.Update(user);
                    await _context.SaveChangesAsync();
                    AddPageAlerts(PageAlertType.Success, "Se modifico corretamente el Cliente.");
                    return RedirectToAction("Index", "Clientes");
                }
                else
                {
                    if (cliente.DependeDe != null && cliente.DependeDe.Id != 0)
                    {
                        cliente.DependeDe = await _context.Clientes.FindAsync(cliente.DependeDe.Id);
                    }
                    if (cliente.Codeudor != null && cliente.Codeudor.Id != 0)
                    {
                        cliente.Codeudor = await _context.Clientes.FindAsync(cliente.Codeudor.Id);
                    }
                    ClienteViewBag(cliente);
                    return View(cliente);
                }
            }
            catch (System.Exception e)
            {
                AddPageAlerts(PageAlertType.Error,e.Message + "Hubo un error al modificar el Cliente. Intentelo nuevamente mas tarde.");
                return RedirectToAction("Index", "Clientes");
            }
        }

        public ActionResult _Image(int Id)
        {
            Clientes cliente = _context.Clientes.Where(s => s.Id == Id).First();

            if (cliente.Persona.Foto != null)
            {
                ViewBag.Foto = Convert.ToBase64String(cliente.Persona.Foto);
            }
            return PartialView(cliente);
        }

        [HttpPost]
        public async Task<ActionResult> _Image(Clientes cliente, IFormFile FotoCliente)
        {
            try
            {
                Clientes clienteEdit = await _context.Clientes.FindAsync(cliente.Id);
                if (FotoCliente != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await FotoCliente.CopyToAsync(memoryStream);
                        clienteEdit.Persona.Foto = memoryStream.ToArray();
                    }
                }

                _context.Clientes.Update(clienteEdit);
                await _context.SaveChangesAsync();
                AddPageAlerts(PageAlertType.Success, "Se cargo correctamente la Foto del Cliente.");
                return RedirectToAction("Index", "Clientes");

            }
            catch (System.Exception e)
            {
                AddPageAlerts(PageAlertType.Error, "Hubo un error al cargar la Foto del Cliente. Intentelo nuevamente mas tarde.");
                return RedirectToAction("Index", "Clientes");
            }
        }

        private void ClienteViewBag(Clientes cliente = null)
        {
            ViewBag.TiposClientes = _context.TiposClientes.Select(g => new SelectListItem() { Text = g.Nombre, Value = g.Id.ToString() });
            ViewBag.TiposDocumento = _context.TipoDocumento.Select(g => new SelectListItem() { Text = g.Descripcion, Value = g.Id.ToString() });
            ViewBag.Paises = _context.Paises.Select(g => new SelectListItem() { Text = g.Nombre, Value = g.Id.ToString() });
            ViewBag.Empresa = _context.Empresas.Select(g => new SelectListItem() { Text = g.RazonSocial, Value = g.Id.ToString() });
            ViewBag.EstadosCiviles = _context.EstadosCiviles.OrderByDescending(x => x.Id).Select(g => new SelectListItem() { Text = g.Nombre, Value = g.Id.ToString() });
            ViewBag.TipoPersonas = _context.TiposPersonas.Select(g => new SelectListItem() { Text = g.nombre + " ("+g.Organismo.Descripcion+")",  Value = g.Id.ToString() });
            ViewBag.Genero = _context.Genero.Select(g => new SelectListItem() { Text = g.Descripcion, Value = g.Id.ToString() }) ;
            ViewBag.LugarNacimiento = _context.Provincia.Select(g => new SelectListItem() { Text = g.Descripcion, Value = g.Id.ToString() });
            var provincias = _context.Provincia.ToList();
            ViewBag.Provincias = provincias.Select(g => new SelectListItem() { Text = g.Descripcion, Value = g.Id.ToString() });
            ViewBag.Localidades = (provincias!=null && provincias.Count()>0)? _context.Localidad.Where(x=>x.IdProvincia==provincias.First().Id).Select(g => new SelectListItem() { Text = g.Descripcion, Value = g.Id.ToString() }) : _context.Localidad.Select(g => new SelectListItem() { Text = g.Descripcion, Value = g.Id.ToString() });
            if (cliente != null)
            {
                if (cliente.Provincia != null)
                {
                    ViewBag.Localidades = _context.Localidad.Where(x => x.IdProvincia == cliente.Provincia.Id).Select(g => new SelectListItem() { Text = g.Descripcion, Value = g.Id.ToString() });
                }
                ViewBag.CodeudorId = (cliente.Codeudor != null) ? cliente.Codeudor.Id.ToString() : "";
                ViewBag.CodeudorDescripcion = (cliente.Codeudor != null) ? cliente.Codeudor.Persona?.Nombres?.ToUpper() + " " + cliente.Codeudor.Persona?.Apellido?.ToUpper() : "";
                ViewBag.DependeDeId = (cliente.DependeDe != null) ? cliente.DependeDe.Id.ToString() : "";
                ViewBag.DependeDeDescripcion = (cliente.DependeDe != null) ? cliente.DependeDe.Persona.Nombres?.ToUpper() + " " + cliente.DependeDe.Persona.Apellido?.ToUpper() : "";
            }
        }

        [HttpPost]
        public async Task<ActionResult> ValdiarCliente(string ValdiarClienteId)
        {
            try
            {
                Clientes cliente = _context.Clientes.Find(Convert.ToInt32(ValdiarClienteId));
                cliente.ClienteValidado = true;
                _context.Update(cliente);
                _context.SaveChanges();
                AddPageAlerts(PageAlertType.Success, "Se Valido el Cliente Correctamente.");
                return RedirectToAction("Index", "Clientes");
            }
            catch (System.Exception e)
            {
                AddPageAlerts(PageAlertType.Error, "Hubo un error al Validar el Cliente. Intentelo nuevamente mas tarde.");
                return RedirectToAction("Index", "Clientes");
            }
        }
        [HttpPost]
        public string SelectLocalidades(int id)
        {
            string array = "";
            var localidad = _context.Localidad.Where(x => x.IdProvincia==id).ToList();
            foreach (var loc in localidad)
            {
                array += "<option value='" + loc.Id + "'>" + loc.Descripcion + "</option>";
            }

            return array;
        }

        [HttpGet]
        public async Task<ActionResult> _ClientePrestamoEnabled(int id)
        {
            try
            {
                Clientes cliente = _context.Clientes.Find(id);
                cliente.BloquearPrestamos = false;
                _context.Update(cliente);
                _context.SaveChanges();
                AddPageAlerts(PageAlertType.Success, "Se Desbloqueo Correctamente el Cliente para solicitar prestamos.");
            }
            catch (Exception)
            {
                AddPageAlerts(PageAlertType.Error, "Hubo un error al Habilitar las Solicitudes del Cliente. Intentelo nuevamente mas tarde.");
            }            
            return RedirectToAction("Index", "Clientes");
        }

        [HttpGet]
        public async Task<ActionResult> _ClientePrestamoDisabled(int id)
        {
            try
            {
                Clientes cliente = _context.Clientes.Find(id);
                cliente.BloquearPrestamos = true;
                _context.Update(cliente);
                _context.SaveChanges();
                AddPageAlerts(PageAlertType.Success, "Se bloqueo Correctamente el Cliente para solicitar prestamos.");
            }
            catch (Exception)
            {
                AddPageAlerts(PageAlertType.Error, "Hubo un error al bloquear el Cliente para solicitar prestamos. Intentelo nuevamente mas tarde.");
            }
            return RedirectToAction("Index", "Clientes");
        }
    }
}