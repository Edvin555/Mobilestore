
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class MobileListViewModel
    {
        public IEnumerable<MobilePhone> MobilePhones { get; set; }
        public PageInfo PageInfo { get; set; }
        public string CurrentBrand { get; set; }
    }
}