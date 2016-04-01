
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class MobileController : Controller
    {
        private IMobileRepository repository;
        private EFDbContext db = new EFDbContext();
        public int pageSize = 24 ;
        public MobileController(IMobileRepository repo)
        {
            repository = repo;
        }
        
        public ViewResult List(string brand = null, string SearchString = " ", int page = 1 )
        {
            UpdateDatabase();

            MobileListViewModel model = new MobileListViewModel
            {
                MobilePhones = repository.MobilePhones.Where(b => brand == null || b.Name.Split(new char[] { ' ' })[0] == brand )
                                                      .Where(b => b.Name.ToLower().Contains(SearchString.ToLower()))
                                                      .OrderBy(b => b.Price).Skip((page - 1) * pageSize).Take(pageSize),
                PageInfo = new PageInfo
                {
                    CurrentPage = page,
                    TotalItems = brand == null ? repository.MobilePhones.Where(b => b.Name.ToLower().Contains(SearchString.ToLower())).Count() :
                                          repository.MobilePhones.Where(b => b.Name.Split(new char[] { ' ' })[0] == brand).Count(),
                    
                    ItemsPerPage = pageSize
                },
                CurrentBrand = brand


            };
            return View(model);
        }


        public void UpdateDatabase()
        {
            var now = DateTime.Now;

            double diff = 45;
            var lastUpdate = db.UpdateDates.FirstOrDefault();

            if (lastUpdate != null)
            {
                diff = ((TimeSpan)(now - lastUpdate.Date)).TotalHours;
            }

            if (diff > 44)
            {
                var newUpdate = new UpdateDate() { Date = now };
                if (lastUpdate != null) db.UpdateDates.Remove(lastUpdate);

                db.UpdateDates.Add(newUpdate);
                db.MobilePhones.RemoveRange(db.MobilePhones);
               
                var parser = new Parser();
                var list = parser.Parse1Alv();
             
                db.MobilePhones.AddRange(list);
                db.SaveChanges();
                                             
            }
         
        }

  

 
	}
}