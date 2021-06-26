﻿using System;
using System.Linq;
using System.Web.Mvc;
using ASP2184587.Models;
using Rotativa;


namespace ASP2184587.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            using (var db = new inventarioEntities1())
            {
                return View(db.producto.ToList());
            }
        }

        public static string NombreProveedor(int? idproveedoor)
        {
            using (var db = new inventarioEntities1())
            {
                return db.proveedor.Find(idproveedoor).nombre;
            }
        }

        public ActionResult ListarProveedores()
        {
            using (var db = new inventarioEntities1())
            {
                return PartialView(db.proveedor.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(producto producto)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventarioEntities1())
                {
                    db.producto.Add(producto);
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
                producto productoEdit = db.producto.Where(a => a.id == id).FirstOrDefault();
                return View(productoEdit);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(producto productoEdit)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    var oldProduct = db.producto.Find(productoEdit.id);
                    oldProduct.nombre = productoEdit.nombre;
                    oldProduct.cantidad = productoEdit.cantidad;
                    oldProduct.descripcion = productoEdit.descripcion;
                    oldProduct.percio_unitario = productoEdit.percio_unitario;
                    oldProduct.id_proveedor = productoEdit.id_proveedor;
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
                return View(db.producto.Find(id));
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    producto producto = db.producto.Find(id);
                    db.producto.Remove(producto);
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

        public ActionResult Reporte()
        {
            var db = new inventarioEntities1();
            var query = from tabProvedor in db.proveedor
                        join tabProducto in db.producto on tabProvedor.id equals tabProducto.id_proveedor
                        select new Reporte
                        {
                            nombreProveedor = tabProvedor.nombre,
                            telefonoProveedor = tabProvedor.telefono,
                            direccionProveedor = tabProvedor.direccion,
                            nombreProducto = tabProducto.nombre,
                            precioProducto = tabProducto.percio_unitario
                        };
            return View(query);
        }

        public ActionResult ImprimirReporte()
        {
            return new ActionAsPdf("Reporte") { FileName = "Reporte.pdf" };
        }

    }
}