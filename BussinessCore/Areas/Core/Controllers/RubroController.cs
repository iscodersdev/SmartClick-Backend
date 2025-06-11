using SmartClickCore.Controllers;
using System.Collections.Generic;
using Commons.Models;
using DAL.Data;
using DAL.Models.Core;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Drawing;

namespace SmartClickCore.Areas.Core.Controllers
{
    [Area("Core")]
    public class RubroController : SmartClickCoreController
    {
        public RubroController(SmartClickContext context) : base(context)
        {
            breadcumb.Add(new Message() { DisplayName = "Datos" });
        }
        public ActionResult Index()
        {
            breadcumb.Add(new Message() { DisplayName = "Rubros" });
            ViewBag.Breadcrumb = breadcumb;

            List<Rubro> rubros = _context.Rubros.Where(x=>x.Activo).ToList();

            return View(rubros);
        }



        public ActionResult _Create()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> _Create([Bind("Nombre, Descripcion")] Rubro nuevoRubro)
        {
            try
            {
                ModelState.Remove("Id");
                if (ModelState.IsValid)
                {
                    nuevoRubro.Activo = true;
                    nuevoRubro.VerEnAPP = true;
                    await _context.Rubros.AddAsync(nuevoRubro);
                    await _context.SaveChangesAsync();
                    AddPageAlerts(PageAlertType.Success, "Se cargo correctamente el Rubro " + nuevoRubro.Nombre + ".");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return PartialView(nuevoRubro);
                }

            }
            catch (System.Exception)
            {
                AddPageAlerts(PageAlertType.Error, "Hubo un error al cargar el Rubro. Intentelo nuevamente mas tarde.");
                return RedirectToAction(nameof(Index));
            }
        }
        public IActionResult Delete(int id)
        {
            try
            {
                Rubro rubro = _context.Rubros.Find(id);
                if (rubro != null)
                {
                    rubro.Activo = false;
                    _context.Rubros.Update(rubro);
                    _context.SaveChanges();
                    AddPageAlerts(PageAlertType.Success, "Se eliminó correctamente el Rubro " + rubro.Nombre + ".");
                    return RedirectToAction(nameof(Index));
                }
                AddPageAlerts(PageAlertType.Error, "Hubo un error al eliminar el Rubro. Intentelo nuevamente mas tarde.");
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception)
            {
                AddPageAlerts(PageAlertType.Error, "Hubo un error al eliminar el Rubro. Intentelo nuevamente mas tarde.");
                return RedirectToAction(nameof(Index));
            }
        }

        public ActionResult _Update(int id)
        {
            Rubro rubro = _context.Rubros.Find(id);
            if (rubro != null)
            {
                return PartialView(rubro);
            }
            AddPageAlerts(PageAlertType.Error, "Hubo un error al editar el Rubro. Intentelo nuevamente mas tarde.");
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> _Update(Rubro editarRubro)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    editarRubro.Activo = true;
                    editarRubro.VerEnAPP = true;
                    _context.Rubros.Update(editarRubro);
                    await _context.SaveChangesAsync();
                    AddPageAlerts(PageAlertType.Success, "Se modifico corretamente el Rubro.");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return PartialView(editarRubro);
                }
            }
            catch (System.Exception)
            {
                AddPageAlerts(PageAlertType.Error, "Hubo un error al modificar el Rubro. Intentelo nuevamente mas tarde.");
                return RedirectToAction(nameof(Index));
            }
        }

        public ActionResult _Image(int Id)
        {
            Rubro rubro  = _context.Rubros.Find(Id);
            if (rubro != null)
            {
                if (rubro.IconoAPP != null)
                {
                    ViewBag.Foto = Convert.ToBase64String(rubro.IconoAPP);
                }
               
                return PartialView(rubro);
            }
            AddPageAlerts(PageAlertType.Error, "Hubo un error al editar la Imagen del Rubro. Intentelo nuevamente mas tarde.");
            return RedirectToAction("Index", "Rubro");
        }

        [HttpPost]
        public async Task<ActionResult> _Image(Rubro rubro, IFormFile FotoRubro)
        {
            try
            {
                Rubro rubroEdit = await _context.Rubros.FindAsync(rubro.Id);
                if (FotoRubro != null)
                {

                   
                    using (var memoryStream = new MemoryStream())
                    {
                        await FotoRubro.CopyToAsync(memoryStream);
                        //rubroEdit.IconoAPP = common.imageaByte(common.resizeImage(common.ByteaImage(memoryStream.ToArray()), new Size(300, 300)));
                        rubroEdit.IconoAPP = memoryStream.ToArray();                  
                    }
                }

                _context.Rubros.Update(rubroEdit);
                await _context.SaveChangesAsync();
                AddPageAlerts(PageAlertType.Success, "Se cargo correctamente la Foto del Rubro.");
                return RedirectToAction("Index", "Rubro");
            }
            catch (System.Exception e)
            {
                AddPageAlerts(PageAlertType.Error, "Hubo un error al cargar la Foto del Producto. Intentelo nuevamente mas tarde.");
                return RedirectToAction("Index", "Rubro");
            }
        }


    }
}
