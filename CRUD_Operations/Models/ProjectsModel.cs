using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_Operations.Models
{
    public class ProjectsModel
    {
        public string ProjectName { get; set; }
        public int ClientId { get; set; }
        public IEnumerable<SelectListItem> Clients { get; set; }
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        public DateTime DateAdded { get; set; }
    }
}