using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD_Operations.Models;
using CRUD_Operations.Models.Repository;

namespace CRUD_Operations.Controllers
{
    public class LoginController : Controller
    {
        DAL dal = new DAL();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserRegModel userReg)
        {
            if (!ModelState.IsValid)
            {
                bool isValidUser = dal.LoginUser(userReg);
                string role = dal.GetRole(userReg).ToLower();
                if (isValidUser)
                {
                    Session["UserEmail"] = userReg.email;
                    Session["UserRole"] = role;
                    // Redirect to the Index action of the Home controller
                    if((string) Session["UserRole"] == "client")
                    {
                        return RedirectToAction("AllQueries", "Query");
                    }
                    if((string) Session["UserRole"] == "admin" || (string) Session["UserRole"] == "hr")
                    {
                        return RedirectToAction("Index", "User");
                    }
                    if((string) Session["UserRole"] == "developer")
                    {
                        return RedirectToAction("AllQueries", "Query");
                    }
                
                }
                else
                {
                    ViewBag.Message = "Invalid Email or Password";
                    return View();
                }
            }

            return View(userReg);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Login");
        }
    }

}
