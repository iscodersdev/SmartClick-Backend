using SmartClickCore.Controllers;
using DAL.Data;
using DAL.DTOs;
using DAL.Models.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SmartClickCore.Areas.Core.Controllers
{
    [Area("Core")]
    public class ProductosController : SmartClickCoreController
    {
        int g_proveedorid;
        public ProductosController(SmartClickContext context) : base(context)
        {
        }
        public IActionResult _ListaProductos(int Id)
        {
            ViewBag.ProveedorId = Id;
            g_proveedorid = Id;
            return PartialView();
        }
        public IActionResult ProductosDataTable(int ProveedorId)
        {
            List<Producto> productos = _context.Productos.Where(x => x.Activo && x.Proveedor.Id == ProveedorId).ToList();
            var query = from p in productos
                        select new ListProductosDTO
                        {
                            Id = p.Id,
                            Descripcion = p.Descripcion?.ToUpper(),
                            DescripcionAmpliada = p.DescripcionAmpliada?.ToUpper(),
                            Precio = p.Precio,
                            Oferta = (p.Oferta != null) ? p.Oferta.ToString() : "---",
                            Financiable = (p.Financiable) ? "SI" : "NO",
                            Rubro = p.Rubro?.Nombre?.ToString()

                        };
            return DataTable(query.AsQueryable());
        }
        public ActionResult _Create(int Id)
        {
            ProductoDTO modelo = new ProductoDTO();
            modelo = CreateProductoDTO(modelo, Id);
            return PartialView(modelo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> _Create([Bind("Descripcion, ProveedorId, RubroSeleccionado,DescripcionAmpliada,Precio,Oferta,Financiable")] ProductoDTO nuevoProducto)
        {
            try
            {
                if (nuevoProducto.ProveedorId < 1)
                {
                    ModelState.AddModelError("ProveedorId", "Debe tener asignado un Proveedor.");
                }
                if (nuevoProducto.RubroSeleccionado < 1)
                {
                    ModelState.AddModelError("RubroSeleccionado", "Debe seleccionar un Rubro");
                }
                if (ModelState.IsValid)
                {
                    Producto producto = new Producto
                    {
                        Proveedor = await _context.Proveedores.FindAsync(nuevoProducto.ProveedorId),
                        Descripcion = nuevoProducto.Descripcion,
                        DescripcionAmpliada = nuevoProducto.DescripcionAmpliada,
                        Precio = nuevoProducto.Precio,
                        Oferta = nuevoProducto.Oferta,
                        Financiable = nuevoProducto.Financiable,
                        Rubro = await _context.Rubros.FindAsync(nuevoProducto.RubroSeleccionado),
                        Activo = true
                    };
                    await _context.Productos.AddAsync(producto);
                    await _context.SaveChangesAsync();
                    AddPageAlerts(PageAlertType.Success, "Se cargo correctamente el Producto " + nuevoProducto.Descripcion + ".");
                    return RedirectToAction("_DetalleProveedor", "Proveedor", new { @Id = nuevoProducto.ProveedorId });
                }
                else
                {
                    return PartialView(CreateProductoDTO(nuevoProducto, nuevoProducto.ProveedorId));
                }

            }
            catch (System.Exception)
            {
                AddPageAlerts(PageAlertType.Error, "Hubo un error al cargar el Producto. Intentelo nuevamente mas tarde.");
                return RedirectToAction("_DetalleProveedor", "Proveedor", new { @Id = nuevoProducto.ProveedorId });
            }
        }
        public ActionResult _Delete(int id)
        {
            Producto producto = _context.Productos.Find(id);
            if (producto != null)
            {
                ProductoDTO deleteProducto = new ProductoDTO();
                deleteProducto = CreateProductoDTO(deleteProducto, producto.Proveedor.Id, producto);
                return PartialView(deleteProducto);
            }
            AddPageAlerts(PageAlertType.Error, "Hubo un error al borrar el Producto. Intentelo nuevamente mas tarde.");
            return RedirectToAction("Index", "Proveedor");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int ProductoId)
        {
            try
            {
                Producto producto = _context.Productos.Find(ProductoId);
                if (producto != null)
                {
                    producto.Activo = false;
                    _context.Productos.Update(producto);
                    _context.SaveChanges();
                    AddPageAlerts(PageAlertType.Success, "Se eliminó correctamente el Producto " + producto.Descripcion + ".");
                    return RedirectToAction("_DetalleProveedor", "Proveedor", new { @Id = producto.Proveedor.Id });
                }
                AddPageAlerts(PageAlertType.Error, "Hubo un error al eliminar el Producto. Intentelo nuevamente mas tarde.");
                return RedirectToAction("Index", "Proveedor");
            }
            catch (System.Exception)
            {
                AddPageAlerts(PageAlertType.Error, "Hubo un error al eliminar el Producto. Intentelo nuevamente mas tarde.");
                return RedirectToAction("Index", "Proveedor");
            }
        }

        public ActionResult _Update(int id)
        {
            Producto producto = _context.Productos.Find(id);
            if (producto != null)
            {
                ProductoDTO editProducto = new ProductoDTO();
                editProducto = CreateProductoDTO(editProducto, producto.Proveedor.Id, producto);
                return PartialView(editProducto);
            }
            AddPageAlerts(PageAlertType.Error, "Hubo un error al editar el Producto. Intentelo nuevamente mas tarde.");
            return RedirectToAction("Index", "Proveedor");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> _Update([Bind("ProveedorId,ProductoId,Descripcion,RubroSeleccionado,DescripcionAmpliada,Precio,Oferta,Financiable")] ProductoDTO editProducto)
        {
            try
            {
                if (editProducto.ProveedorId < 1)
                {
                    ModelState.AddModelError("ProveedorId", "Debe tener asignado un Proveedor.");
                }
                if (editProducto.RubroSeleccionado < 1)
                {
                    ModelState.AddModelError("RubroSeleccionado", "Debe seleccionar un Rubro");
                }
                if (ModelState.IsValid)
                {
                    Producto producto = await _context.Productos.FindAsync(editProducto.ProductoId);
                    producto.Descripcion = editProducto.Descripcion;
                    producto.DescripcionAmpliada = editProducto.DescripcionAmpliada;
                    producto.Precio = editProducto.Precio;
                    producto.Oferta = editProducto.Oferta;
                    producto.Financiable = editProducto.Financiable;
                    producto.Proveedor = await _context.Proveedores.FindAsync(editProducto.ProveedorId);
                    producto.Rubro = await _context.Rubros.FindAsync(editProducto.RubroSeleccionado);
                    producto.Activo = true;
                    _context.Productos.Update(producto);
                    await _context.SaveChangesAsync();
                    AddPageAlerts(PageAlertType.Success, "Se modifico corretamente el Producto.");
                    return RedirectToAction("_DetalleProveedor", "Proveedor", new { @Id = editProducto.ProveedorId });
                }
                else
                {
                    return PartialView(CreateProductoDTO(editProducto, editProducto.ProveedorId));
                }
            }
            catch (System.Exception)
            {
                AddPageAlerts(PageAlertType.Error, "Hubo un error al modificar el Producto. Intentelo nuevamente mas tarde.");
                return RedirectToAction("_DetalleProveedor", "Proveedor", new { @Id = editProducto.ProveedorId });
            }
        }

        public ActionResult _Image(int Id)
        {
            Producto producto = _context.Productos.Find(Id);
            if (producto != null)
            {
                if (producto.Foto != null)
                {
                    ViewBag.Foto = Convert.ToBase64String(producto.Foto);
                }
                if (producto.Foto1 != null)
                {
                    ViewBag.Foto1 = Convert.ToBase64String(producto.Foto1);
                }
                if (producto.Foto2 != null)
                {
                    ViewBag.Foto2 = Convert.ToBase64String(producto.Foto2);
                }
                if (producto.Foto3 != null)
                {
                    ViewBag.Foto3 = Convert.ToBase64String(producto.Foto3);
                }
                if (producto.Foto4 != null)
                {
                    ViewBag.Foto4 = Convert.ToBase64String(producto.Foto4);
                }
                if (producto.Foto5 != null)
                {
                    ViewBag.Foto5 = Convert.ToBase64String(producto.Foto5);
                }

                return PartialView(producto);
            }
            AddPageAlerts(PageAlertType.Error, "Hubo un error al editar la Imagen del Producto. Intentelo nuevamente mas tarde.");
            return RedirectToAction("Index", "Proveedor");
        }
        [HttpGet]
        public IActionResult _Financiacion(int id)
        {
            Producto p = _context.Productos.Find(id);
            ListFinanciacionDTO lista = new ListFinanciacionDTO();
            lista = CreateListFinanciacionDTO(lista, p);
            return PartialView(lista);
        }

        [HttpPost]
        public async Task<IActionResult> _Financiacion(ListFinanciacionDTO financiacion)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Producto p = await _context.Productos.FindAsync(financiacion.ProductoId);
                    await _context.FinanciacionProductos.AddAsync(new Financiacion
                    {
                        CantidadCuotas = Convert.ToInt32(financiacion.CantidadCuotasAdd),
                        InteresesPorCuota = Convert.ToDecimal(financiacion.InteresCoutasAdd),
                        Producto = p
                    });
                    await _context.SaveChangesAsync();
                    financiacion.InteresCoutasAdd = null;
                    financiacion.CantidadCuotasAdd = null;
                    ModelState.Clear();
                    return PartialView(CreateListFinanciacionDTO(financiacion, p));
                }
                catch (System.Exception)
                {
                    AddPageAlerts(PageAlertType.Error, "Hubo un error al Cargar la Finaciación del Producto. Intentelo nuevamente mas tarde.");
                    return RedirectToAction("Index", "Proveedor");
                }
            }
            else
            {
                Producto p = await _context.Productos.FindAsync(financiacion.ProductoId);
                return PartialView(CreateListFinanciacionDTO(financiacion, p));
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFinanciacion(int Id)
        {
            Financiacion financiacion = await _context.FinanciacionProductos.FindAsync(Id);
            try
            {
                _context.FinanciacionProductos.Remove(financiacion);
                await _context.SaveChangesAsync();

                return Ok(HttpStatusCode.OK);
            }
            catch (System.Exception)
            {
                return Ok(HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        public async Task<ActionResult> _Image(Producto producto, IFormFile FotoProducto, IFormFile Foto1, IFormFile Foto2, IFormFile Foto3, IFormFile Foto4, IFormFile Foto5)
        {
            try
            {
                Producto productoEdit = await _context.Productos.FindAsync(producto.Id);
                if (FotoProducto != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await FotoProducto.CopyToAsync(memoryStream);
                        // productoEdit.Foto = memoryStream.ToArray();
                        productoEdit.Foto = common.imageaByte(common.resizeImage(common.ByteaImage(memoryStream.ToArray()), new Size(300, 300)));
                    }
                }

                if (Foto1 != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await Foto1.CopyToAsync(memoryStream);
                        // productoEdit.Foto = memoryStream.ToArray();
                        productoEdit.Foto1 = common.imageaByte(common.resizeImage(common.ByteaImage(memoryStream.ToArray()), new Size(300, 300)));
                    }
                }
                if (Foto2 != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await Foto2.CopyToAsync(memoryStream);
                        // productoEdit.Foto = memoryStream.ToArray();
                        productoEdit.Foto2 = common.imageaByte(common.resizeImage(common.ByteaImage(memoryStream.ToArray()), new Size(300, 300)));
                    }
                }
                if (Foto3 != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await Foto3.CopyToAsync(memoryStream);
                        // productoEdit.Foto = memoryStream.ToArray();
                        productoEdit.Foto3 = common.imageaByte(common.resizeImage(common.ByteaImage(memoryStream.ToArray()), new Size(300, 300)));
                    }
                }
                if (Foto4 != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await Foto4.CopyToAsync(memoryStream);
                        // productoEdit.Foto = memoryStream.ToArray();
                        productoEdit.Foto4 = common.imageaByte(common.resizeImage(common.ByteaImage(memoryStream.ToArray()), new Size(300, 300)));
                    }
                }
                if (Foto5 != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await Foto5.CopyToAsync(memoryStream);
                        // productoEdit.Foto = memoryStream.ToArray();
                        productoEdit.Foto5 = common.imageaByte(common.resizeImage(common.ByteaImage(memoryStream.ToArray()), new Size(300, 300)));
                    }
                }

                _context.Productos.Update(productoEdit);
                await _context.SaveChangesAsync();
                AddPageAlerts(PageAlertType.Success, "Se cargo correctamente la Foto del Producto.");
                return RedirectToAction("_DetalleProveedor", "Proveedor", new { @Id = productoEdit.Proveedor.Id });
            }
            catch (System.Exception e)
            {
                AddPageAlerts(PageAlertType.Error, "Hubo un error al cargar la Foto del Producto. Intentelo nuevamente mas tarde.");
                return RedirectToAction("Index", "Proveedor");
            }
        }
        public IActionResult _DescargaProductosExcel(int ProveedorId)
        {
            var memoryStream = new MemoryStream(System.IO.File.ReadAllBytes("wwwroot/Plantillas/PlantillaProductos.xlsx"));
            using (var package = new ExcelPackage(memoryStream))
            {
                var workSheet = package.Workbook.Worksheets[1];
                var renglones = _context.Productos.Where(x => x.Proveedor.Id == ProveedorId && x.Activo);
                int linea = 2;
                foreach (var renglon in renglones)
                {
                    workSheet.Cells[linea, 1].Value = renglon.Proveedor.Nombre;
                    workSheet.Cells[linea, 2].Value = renglon.Rubro.Nombre;
                    workSheet.Cells[linea, 3].Value = renglon.Descripcion;
                    workSheet.Cells[linea, 4].Value = renglon.DescripcionAmpliada;
                    workSheet.Cells[linea, 5].Value = decimal.Round(renglon.Precio, 2).ToString();
                    workSheet.Cells[linea, 6].Value = (renglon.Oferta != null) ? decimal.Round((decimal)renglon.Oferta, 2).ToString() : "---";
                    workSheet.Cells[linea, 7].Value = (renglon.Financiable) ? "SI" : "NO";
                    linea = linea + 1;
                }
                return File(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Productos del Proveedor " + renglones.FirstOrDefault()?.Proveedor?.Nombre + ".xlsx");
            }
        }
        private ProductoDTO CreateProductoDTO(ProductoDTO productoDTO, int ProveedorId, Producto producto = null)
        {
            productoDTO.Rubros = _context.Proveedores.Find(ProveedorId).Rubros.Select(x => x.Rubro).Where(n => n.Activo).Select(g => new SelectListItem() { Text = g.Nombre, Value = g.Id.ToString() });
            productoDTO.ProveedorId = ProveedorId;
            if (producto != null)
            {
                productoDTO.ProductoId = producto.Id;
                productoDTO.Descripcion = producto.Descripcion;
                productoDTO.DescripcionAmpliada = producto.DescripcionAmpliada;
                productoDTO.Precio = producto.Precio;
                productoDTO.Oferta = producto.Oferta;
                productoDTO.Financiable = producto.Financiable;
                productoDTO.RubroSeleccionado = producto.Rubro.Id;
            }
            return productoDTO;
        }

        private ListFinanciacionDTO CreateListFinanciacionDTO(ListFinanciacionDTO lista, Producto producto)
        {
            lista.PrecioProducto = producto.Precio;
            lista.ProductoId = producto.Id;
            lista.ProductoNombre = producto.Descripcion;
            foreach (Financiacion f in producto.FinanciacionProducto)
            {
                lista.ListaFinanciacion.Add(new FinanciacionDTO
                {
                    FinaciacionId = f.Id,
                    CantidadCuotas = f.CantidadCuotas,
                    InteresCuota = f.InteresesPorCuota,
                    ValorCuota = decimal.Round((producto.Precio / f.CantidadCuotas) * (1 + (f.InteresesPorCuota / 100)), 2)
                });
            }
            return lista;
        }
        public IActionResult _SubProductos(int Id)
        {
            var subproductos = _context.SubProductos.Where(x => x.Producto.Id == Id && x.FechaBaja == null).OrderBy(x => x.NombreSubProducto).ToList();
            ViewBag.ProductoId = Id;
            return PartialView(subproductos);
        }
        public ActionResult _CreateSubProducto(int Id)
        {
            var subproducto = new SubProducto();
            subproducto.Producto = _context.Productos.Find(Id);
            return PartialView(subproducto);
        }

        [HttpPost]
        public ActionResult _CreateSubProducto(SubProducto subproducto)
        {
            subproducto.Id = 0;
            subproducto.Producto = _context.Productos.Find(subproducto.Producto.Id);
            _context.SubProductos.Add(subproducto);
            _context.SaveChanges();
            AddPageAlerts(PageAlertType.Success, "SubProducto Agregado Correctamente!!!");
            return RedirectToAction("_DetalleProveedor", "Proveedor", new { Id = subproducto.Producto.Proveedor.Id });
        }
        public ActionResult _UpdateSubProducto(int Id)
        {
            var subproducto = _context.SubProductos.Find(Id);
            return PartialView(subproducto);
        }

        [HttpPost]
        public ActionResult _UpdateSubProducto(SubProducto subproducto, int productoId)
        {
            var actualiza = _context.SubProductos.Find(subproducto.Id);
            actualiza.NombreSubProducto = subproducto.NombreSubProducto;
            _context.SubProductos.Update(actualiza);
            _context.SaveChanges();
            AddPageAlerts(PageAlertType.Success, "SubProducto Actualizado Correctamente!!!");
            return RedirectToAction("_DetalleProveedor", "Proveedor", new { Id = actualiza.Producto.Proveedor.Id });


        }

        public ActionResult _DeleteSubProducto(int Id)
        {
            var subproducto = _context.SubProductos.Find(Id);
            return PartialView(subproducto);
        }

        [HttpPost]
        public ActionResult DeleteSubProducto(int Id)
        {
            var subproducto = _context.SubProductos.Find(Id);
            subproducto.FechaBaja = DateTime.Now;
            _context.SubProductos.Update(subproducto);
            _context.SaveChanges();
            return RedirectToAction("_DetalleProveedor", "Proveedor", new { Id = subproducto.Producto.Proveedor.Id });
        }

    }
}
