using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Web.Mvc;

namespace CRUD_Operations.Models
{
    public class UserRegModel
    {
        [Required]
        [Display(Name = "User ID")]
        public int id { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        // [Remote("EmailExists", "User", HttpMethod = "POST", ErrorMessage = "Email already registered.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

        [Required]
        [Display(Name ="Name")]
        public string name { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        [Display(Name = "Role")]
        public string role { get; set; }
    }
}