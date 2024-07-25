using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using CRUD_Operations.Models;
using CRUD_Operations.Models.Repository;

namespace CRUD_Operations.Controllers
{
    public class UserController : Controller
    {
        DAL dal = new DAL();

        // Add roles to a list
        private List<SelectListItem> GetRoles()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "Admin", Text = "Admin" },
                new SelectListItem { Value = "Client", Text = "Client" },
                new SelectListItem { Value = "Developer", Text = "Developer" },
                new SelectListItem { Value = "HR", Text = "HR" }
            };
        }

        // GET: User
        public ActionResult Index()
        {
            if(Session["UserEmail"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            ModelState.Clear();
            return View(dal.GetDataList());
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            if (Session["UserEmail"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View(dal.GetDataList().Find(userReg => userReg.id == id));
        }

        // GET: User/Create
        public ActionResult Create()
        {
            ViewBag.Roles = GetRoles();
            return View();
        }

/*        public ActionResult EmailExists(string email)
        {
            return Json(!String.Equals(email, email ,StringComparison.OrdinalIgnoreCase));
        } */

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(UserRegModel userReg)
        {
            try
            {
                if (dal.InsertData(userReg))
                {
                    ViewBag.Message = "Data Saved";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Roles = GetRoles();
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["UserEmail"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            ViewBag.Roles = GetRoles();
            return View(dal.GetDataList().Find(userReg => userReg.id == id));
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, UserRegModel userReg)
        {
            try
            {
                if (dal.UpdateData(userReg))
                {
                    ViewBag.Message("Data Updated");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Roles = GetRoles();
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["UserEmail"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View(dal.GetDataList().Find(userReg => userReg.id == id));
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, UserRegModel userReg)
        {
            try
            {
                if (dal.DeleteData(userReg))
                {
                    ViewBag.Message("Data Deleted");
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
