using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD_Operations.Models;
using CRUD_Operations.Models.Repository;

namespace CRUD_Operations.Controllers
{
    public class ProjectController : Controller
    {
        ProjectDAL repository = new ProjectDAL();
        
        public ActionResult Create()
        {
            var model = new ProjectsModel
            {
                Clients = repository.GetClients()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ProjectsModel projects)
        {
            try
            {
                if (repository.AddProject(projects))
                {
                    ViewBag.Message = "Data Saved";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Index()
        {
            var projects = repository.GetProjects();
            return View(projects);
        }
    }
}