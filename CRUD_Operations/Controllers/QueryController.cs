using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD_Operations.Models;
using CRUD_Operations.Models.Repository;
using System.IO;

namespace CRUD_Operations.Controllers
{
    public class QueryController : Controller
    {
        TicketDAL ticketDAL = new TicketDAL();
        ProjectDAL projectDAL = new ProjectDAL();
        DAL dal = new DAL();

        //private List<SelectListItem> GetPriority()
        //{
        //    return new List<SelectListItem>
        //    {
        //        new SelectListItem { Value = "Low", Text = "Low" },
        //        new SelectListItem { Value = "Mid", Text = "Mid" },
        //        new SelectListItem { Value = "High", Text = "High" }
        //    };
        //}

        // GET: Query
        [HttpGet]
        public ActionResult CreateQuery()
        {
            if(true /* Session["UserRole"] != null && Session["UserRole"].ToString() == "Client" */)
            {
                string clientEmail = Session["UserEmail"].ToString();
                ViewBag.Projects = projectDAL.GetProjectsByEmail(clientEmail);
            }
            return View();
        }

        [HttpPost]
        public ActionResult CreateQuery(QueryModel model, HttpPostedFileBase screenshotFile)
        {
                model.QueryRaiseDate = DateTime.Now;

            //if (screenshotFile != null && screenshotFile.ContentLength > 0)
            //{
            //        string filePath = System.IO.Path.Combine(Server.MapPath("~/Screenshots"), System.IO.Path.GetFileName(screenshotFile.FileName));
            //        screenshotFile.SaveAs(filePath);
            //        model.ScreenshotPath = filePath;
            //}

            string path = Server.MapPath("~/Screenshots/");
            string filename = Path.GetFileName(screenshotFile.FileName);
            string fullpath = Path.Combine(path, filename);
            screenshotFile.SaveAs(fullpath);
            model.FileUpload = filename;
            
            if (ticketDAL.InsertTicket(model))
            {
                ViewBag.message = "Data Inserted";
                return RedirectToAction("AllQueries");
            }
            return View(model);
        }
        
        public ActionResult AllQueries()
        {

            return View(ticketDAL.GetQueries());
        }

        //public ActionResult AddressQuery()
        //{
        //    var model = new QueryModel
        //    {
        //        Devs = ticketDAL.GetDevs()
        //    };
        //    return View(model);
        //}

        public ActionResult AddressQuery(int TicketNumber)
        {
            // Retrieve the query details by TicketNumber
            var queryDetails = ticketDAL.GetQueries().Find(Query => Query.TicketNumber == TicketNumber);

            // If the query details are not found, handle the case appropriately (e.g., return a NotFound view)
            if (queryDetails == null)
            {
                return HttpNotFound();
            }

            // Create a new instance of the QueryModel
            var model = new QueryModel
            {
                TicketNumber = queryDetails.TicketNumber,
                ProjectName = queryDetails.ProjectName,
                Subject = queryDetails.Subject,
                Message = queryDetails.Message,
                Priority = queryDetails.Priority,
                QueryRaiseDate = queryDetails.QueryRaiseDate,
                IsActive = queryDetails.IsActive,
                AssignedTo = queryDetails.AssignedTo,

                //Devs = ticketDAL.GetDevs()
            };
            ViewBag.Devs = ticketDAL.GetDevs();

            List<SelectListItem> Priority = new List<SelectListItem>
            {
                new SelectListItem { Text = "Low", Value = "Low" },
                new SelectListItem { Text = "Med", Value = "Med" },
                new SelectListItem { Text = "High", Value = "High" }
            };

            ViewData["Priority"] = Priority;
            // Return the model to the view
            return View(model);
        }

        [HttpPost]
        public ActionResult AddressQuery(int TicketNumber, QueryModel model)
        {
            List<SelectListItem> Priority = new List<SelectListItem>
            {
                new SelectListItem { Text = "Low", Value = "Low" },
                new SelectListItem { Text = "Med", Value = "Med" },
                new SelectListItem { Text = "High", Value = "High" }
            };

            ViewData["Priority"] = Priority;
             
            ViewBag.Devs = ticketDAL.GetDevs();
            try
            {
                if (ticketDAL.AddressQuery(model))
                {
                    ViewBag.Message("Data Updated");
                }
                return RedirectToAction("AllQueries");
            }
            catch(Exception e)
            {
                return View();
            }
        }
    }
}