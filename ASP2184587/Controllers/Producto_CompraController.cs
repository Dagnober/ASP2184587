using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP2184587.Models;



namespace ASP2184587.Controllers
{
    public class Producto_CompraController : Controller
    {
        // GET: Producto_Compra
        public ActionResult Index()
        {
            using (var db = new inventarioEntities1())
            {
                return View(db.producto_compra.ToList());
            }
        }

        public static string NombreProducto(int? idproductoo)
        {
            using (var db = new inventarioEntities1())
            {
                return db.producto.Find(idproductoo).nombre;
            }
        }

        public ActionResult ListarProducto()
        {
            using (var db = new inventarioEntities1())
            {
                return PartialView(db.producto.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(producto_compra producto_Compra)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventarioEntities1())
                {
                    db.producto_compra.Add(producto_Compra);
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
                producto_compra pro_compraEdit = db.producto_compra.Where(a => a.id == id).FirstOrDefault();
                return View(pro_compraEdit);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(producto_compra pro_compraEdit)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    var oldpro_comp = db.producto_compra.Find(pro_compraEdit.id);
                    oldpro_comp.id_compra = pro_compraEdit.id_compra;
                    oldpro_comp.id_producto = pro_compraEdit.id_producto;
                    oldpro_comp.cantidad = pro_compraEdit.cantidad;                    
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
                return View(db.producto_compra.Find(id));
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    producto_compra producto_Compra = db.producto_compra.Find(id);
                    db.producto_compra.Remove(producto_Compra);
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
    }
}