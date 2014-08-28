using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesteValidation.Models;

namespace TesteValidation.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            var meses = new List<SelectListItem>();
            for (int i = 1; i <= 12; i++)
            {
                meses.Add(new SelectListItem() { Text = String.Format("{0:00}", i), Value = i.ToString() });
            }
            ViewBag.Meses = meses;

            var anos = new List<SelectListItem>();
            int anoInicial = DateTime.Now.Year;
            for (int i = 0; i < 15; i++)
            {
                anos.Add(new SelectListItem() { Text = (anoInicial + i).ToString(), Value = (anoInicial + i).ToString() });
            }
            ViewBag.Anos = anos;

            var cliente = new ClienteModels();
            cliente.ValorMinimo = 10;
            cliente.ValorMaximo = 100;

            return View(cliente);
        }

        [HttpPost]
        public ActionResult Create(ClienteModels modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return View(modelo);
                }

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, ClienteModels modelo)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id, ClienteModels modelo)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }
    }
}
