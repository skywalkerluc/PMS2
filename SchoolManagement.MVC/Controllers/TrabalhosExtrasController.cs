using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.MVC.Controllers
{
    public class TrabalhosExtrasController : Controller
    {
        //
        // GET: /TrabalhosExtras/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /TrabalhosExtras/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /TrabalhosExtras/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TrabalhosExtras/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /TrabalhosExtras/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /TrabalhosExtras/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /TrabalhosExtras/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /TrabalhosExtras/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
