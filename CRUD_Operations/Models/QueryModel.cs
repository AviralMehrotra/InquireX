using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_Operations.Models
{
    public class QueryModel
    {
        public int TicketNumber { get; set; }
        public string ProjectName { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Priority { get; set; }
        public HttpPostedFileBase ScreenshotPath { get; set; }
        public string FileUpload { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd  HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime QueryRaiseDate { get; set; }
        public bool IsActive { get; set; }
        //public IEnumerable<SelectListItem> Devs { get; set; }
        public string AssignedTo { get; set; }
    }
}