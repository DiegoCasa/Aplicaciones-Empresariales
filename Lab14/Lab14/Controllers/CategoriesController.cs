using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Lab14.Models;

namespace Lab14.Controllers
{
    public class CategoriesController : Controller
    {
        #region Contexto
        //ATRIBUTO CONTEXTO
        private NORTHWNDEntities _contexto;
        //PROPIEDAD CONTEXTO
        public NORTHWNDEntities Contexto
        {
            set { _contexto = value; }
            get
            {
                if (_contexto == null)
                    _contexto = new NORTHWNDEntities();
                return _contexto;
            }
        }
        #endregion
        // GET: Categories
        public ActionResult Index()
        {
            //envia a la vista la coleccion de entidades categories
            return View(Contexto.Categories.ToList());
        }
        public ActionResult Details(int id)
        {
            var productosPorcategoria = from p in Contexto.Products
                                        orderby p.ProductName ascending

                                        where p.CategoryID == id

                                        select p;
            return View(productosPorcategoria.ToList());
        }
        // GET: /Categories/Create
        //mostrar formulario
        public ActionResult Create()
        {
            return View();
        }
        // POST: /Categories/Create
        //registrar nueva categoria en la BD
        [HttpPost]
        public ActionResult Create(Categories nuevaCategoria)
        {
            try
            { //validamos los datos ingresados
                if (ModelState.IsValid)
                {
                    //registramos la neva categoria
                    Contexto.Categories.Add(nuevaCategoria);
                    Contexto.SaveChanges();
                    //llamamos al metodo index
                    return RedirectToAction("Index");

                }
                //muestra la vista "create" con datos ingresados
                return View(nuevaCategoria);
            }
            catch
            {
                //muestra la vista "create" vacia
                return View();
            }
        }
        // GET: /Categories/Edit/5
        //muestra formulario para edicion
        public ActionResult Edit(int? id)
        {
            //si el ID es nulo
            if (id == null) //muestra un mensaje de error
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //buscar la categoria a editar
            Categories CategoriaEditar = Contexto.Categories.Find(id);
            //si entidad es NULO (categoria no existe)
            if (CategoriaEditar == null)
                return HttpNotFound();
            //envia la categoria a editar a la vista
            return View(CategoriaEditar);
        }
        // POST: /Categories/Edit/5
        //registrar cambios de la categoria en la BD
        [HttpPost]
        public ActionResult Edit(Categories CategoriaEditar)
        {
            try
            {//validamos los datos ingresados
                if (ModelState.IsValid)
                { //graba los cambios en la categoria

                    Contexto.Entry(CategoriaEditar).State = System.Data.EntityState.Modified;

                    Contexto.SaveChanges();
                    // llama a al metodo index
                    return RedirectToAction("Index");

                }
                //muestra la vista Edit con los datos ingresados
                return View(CategoriaEditar);
            }
            catch
            { //muestra la vista Edit vacia
                return View();
            }
        }
        // GET: /Categories/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //busca categoria a eliminar
            Categories CategoriaEliminar = Contexto.Categories.Find(id);
            // valida que categoria exista
            if (CategoriaEliminar == null)
                return HttpNotFound();
            //envia a la vista la categoria a eliminar
            return View(CategoriaEliminar);
        }
        // POST: /Categories/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, Categories Categoria1)
        {
            try
            {
                Categories CategoriaEliminar = new Categories();
                if (ModelState.IsValid)
                {
                    if (id == null)
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    //busca categoria a eliminar

                    CategoriaEliminar = Contexto.Categories.Find(id);
                    //sino encuentra categoria muestra mensaje
                    if (CategoriaEliminar == null)
                        return HttpNotFound();
                    //elimina la cateegoria

                    Contexto.Categories.Remove(CategoriaEliminar);

                    Contexto.SaveChanges();

                    return RedirectToAction("Index");

                }
                return View(CategoriaEliminar);
            }
            catch
            {
                return View();
            }
        }
    }
}
