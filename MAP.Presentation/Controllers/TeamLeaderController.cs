using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MAP.Presentation.Controllers
{
    public class TeamLeaderController : Controller
    {
        public string x;
        // GET: TeamLeader
        public ActionResult Index()
        {
            if (AccountController.CurrentUserStatus == 2)
            {
                x = "Manager";
            }
            else if (AccountController.CurrentUserStatus == 3)
            {
                x = "Team Leader";
            }
            else
            { x = "Membre"; }
            ViewBag.CurrentUserStatus = x;
            return View();
            
        }

        public ActionResult dashboard()
        {
            return View();
        }

        public ActionResult form()
        {
            return View();
        }

        public ActionResult Table()
        {
            return View();
        }

        public ActionResult Task()
        {
            return View();
        }






        // GET: TeamLeader/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TeamLeader/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TeamLeader/Create
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

        // GET: TeamLeader/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TeamLeader/Edit/5
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

        // GET: TeamLeader/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TeamLeader/Delete/5
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
