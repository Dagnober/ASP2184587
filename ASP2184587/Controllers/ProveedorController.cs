using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ASP2184587.Models;

namespace ASP2184587.Controllers
{
    public class ProveedorController : Controller
    {
        // GET: Proveedor
        public ActionResult Index()
        {
            using(var db = new inventarioEntities1())
            {
                return View(db.proveedor.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        public static string HashSHA1(string value)
        {
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var inputBytes = Encoding.ASCII.GetBytes(value);
            var hash = sha1.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public ActionResult Details(int id)
        {
            using (var db = new inventarioEntities1())
            {
                var findProv = db.proveedor.Find(id);
                return View(findProv);
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    proveedor findProv = db.proveedor.Where(a => a.id == id).FirstOrDefault();
                    return View(findProv);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(proveedor editUser)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    proveedor user = db.proveedor.Find(editUser.id);

                    user.nombre = editUser.nombre;
                    user.direccion = editUser.direccion;
                    user.telefono = editUser.telefono;
                    user.nombre_contacto = editUser.nombre_contacto;                    

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

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    var findProv = db.proveedor.Find(id);
                    db.proveedor.Remove(findProv);
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