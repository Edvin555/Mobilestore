using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Models
{
    public class Search
    {
            [Required(ErrorMessage = "Field is required")]
            [StringLength(40, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
            [AllowHtml]
            public string SearchString { get; set; }
       
    }
}