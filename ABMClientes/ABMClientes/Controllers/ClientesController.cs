using ABMClientes.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;


namespace ABMClientes.Controllers
{
    public class ClientesController : Controller
    {
        // GET: Clientes
        public ActionResult Index()
        {
            using (DbModel context = new DbModel())
            {
                return View(context.Clientes.ToList());

            }
            
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int id)
        {
            using (DbModel context = new DbModel())
            {
                return View(context.Clientes.Where(x => x.id == id).FirstOrDefault());
            }
            
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        [HttpPost]
        public ActionResult Create(Clientes clientes)
        {
            try
            {
                using (DbModel context = new DbModel())
                {
                    context.Clientes.Add(clientes);
                    context.SaveChanges();
                }

                   return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int id)
        {
            using (DbModel context = new DbModel())
            {
                return View(context.Clientes.Where(x => x.id == id).FirstOrDefault());
            }
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Clientes clientes)
        {
            try
            {
                using (DbModel context = new DbModel())
                {
                    context.Entry(clientes).State = EntityState.Modified;
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Clientes/Delete/5   
        public ActionResult Delete(int id)
        {
            using (DbModel context = new DbModel())
            {
                return View(context.Clientes.Where(x => x.id == id).FirstOrDefault());
            }
        }

        // POST: Clientes/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (DbModel context = new DbModel())
                {
                    Clientes cliente = context.Clientes.Where(x => x.id == id).FirstOrDefault();
                    context.Clientes.Remove(cliente);
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public JsonResult ObtenerRazonSocialPorCuit(string cuit)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    // Asegúrate de que la URL esté correcta y que tenga el valor del CUIT
                    var response = client.GetStringAsync($"https://sistemaintegracomex.com.ar/Account/GetNombreByCuit?cuit={cuit}").Result;


                    // Retorna la respuesta como JSON
                    return Json(response, JsonRequestBehavior.AllowGet);
                }
            }
            catch 
            {
                return Json(new { error = "No se pudo obtener la razón social." }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
