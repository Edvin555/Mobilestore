
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class NavController : Controller
    {
        private IMobileRepository repository;
        
        
        public NavController(IMobileRepository repo)
        {
            repository = repo;
        }
        //
        // GET: /Nav/
        public PartialViewResult Menu(string brand = null)
        {
            ViewBag.SelectedBrand = brand;
            IEnumerable<string> brands = repository.MobilePhones
                                         .Select(b => b.Name.Split(new char[]{' '})[0])
                                         .Distinct()
                                         .OrderBy(x => x);
            return PartialView(brands);
        }
	}
}