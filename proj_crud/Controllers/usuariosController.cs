using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using proj_crud.Models;
using proj_crud.Delegates; 

namespace proj_crud.Controllers
{
    public class usuariosController : Controller
    {
        private ApiEntities db = new ApiEntities();

        // GET: usuarios
        public ActionResult Index()
        {
            return View(db.usuarios.ToList());
        }

        // GET: usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario usuario = db.usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
       public ActionResult Create([Bind(Include = "Id,dni,Nombre,Apellido,sexo,Contrasena,correo")] usuario usuario)
{
    var errores = UsuarioDelegates.ValidarUsuario(usuario);

    if (errores.Any())
    {
        foreach (var error in errores)
        {
            ModelState.AddModelError("", error);
        }
        return View(usuario);
    }

    if (ModelState.IsValid)
    {
        db.usuarios.Add(usuario);

        try
        {
            db.SaveChanges();

            TempData["Notificacion"] = $"Nuevo Usuario creado: {usuario.Nombre} {usuario.Apellido}";

            UsuarioDelegates.NotificarCreacion(usuario);

            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            // Registrar o mostrar el error
            ModelState.AddModelError("", "Error al guardar en la base de datos: " + ex.Message);

            // Puedes también registrar los errores internos si necesitas más detalles:
            if (ex.InnerException != null)
            {
                ModelState.AddModelError("", "Detalle interno: " + ex.InnerException.Message);
            }
        }
    }

    return View(usuario);
}

        // GET: usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario usuario = db.usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,dni,Nombre,Apellido,sexo,Contrasena,correo")] usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();

              
                UsuarioDelegates.NotificarEdicion(usuario);

                TempData["Notificacion"] = $"Usuario editado: {usuario.Nombre} {usuario.Apellido}";


                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // GET: usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario usuario = db.usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {


            usuario usuario = db.usuarios.Find(id);

            // Notificación por consola
            UsuarioDelegates.NotificarEliminacion(usuario);

            db.usuarios.Remove(usuario);
            db.SaveChanges();

            TempData["NotificacionEliminado"] = $"Usuario eliminado: {usuario.Nombre} {usuario.Apellido}";

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
