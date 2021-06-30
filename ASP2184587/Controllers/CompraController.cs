using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP2184587.Models;
using Rotativa;

namespace ASP2184587.Controllers
{
    public class CompraController : Controller
    {
        // GET: Compra
        public ActionResult Index()
        {
            using(var db = new inventarioEntities1())
            {
                return View(db.compra.ToList());
            }
        }

        public static string NombreUsuario(int? idusuarioo)
        {
            using (var db = new inventarioEntities1())
            {
                return db.usuario.Find(idusuarioo).nombre;
            }
        }

        public ActionResult ListarUsuarios()
        {
            using (var db = new inventarioEntities1())
            {
                return PartialView(db.usuario.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(compra compra)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventarioEntities1())
                {
                    db.compra.Add(compra);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            using (var db = new inventarioEntities1())
            {
                compra compraEdit = db.compra.Where(a => a.id == id).FirstOrDefault();
                return View(compraEdit);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(compra compraEdit)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    var oldcomp = db.compra.Find(compraEdit.id);
                    oldcomp.fecha = compraEdit.fecha;
                    oldcomp.total = compraEdit.total;
                    oldcomp.id_cliente = compraEdit.id_cliente;
                    oldcomp.id_usuario = compraEdit.id_usuario;                    
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            using (var db = new inventarioEntities1())
            {
                return View(db.compra.Find(id));
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    compra compra = db.compra.Find(id);
                    db.compra.Remove(compra);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }

        public ActionResult Reporte2()
        {
            var db = new inventarioEntities1();
            var query = from tabCliente in db.cliente
                        join tabCompra in db.compra on tabCliente.id equals tabCompra.id_cliente
                        select new Reporte2
                        {
                            nombreCliente = tabCliente.nombre,
                            documentoCliente = tabCliente.documento,
                            emailCliente = tabCliente.email,
                            total = tabCompra.total

                        };
            return View(query);
        }

        public ActionResult ImprimirReporte2()
        {
            return new ActionAsPdf("Reporte2") { FileName = "Reporte.pdf" };
        }
    }
}