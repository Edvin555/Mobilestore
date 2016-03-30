
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class CartIndexViewModel
    {
       
        public List<CartLineView> CartLines { get; set; }
        public decimal TotalValue { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class CartLineView
    {
        public CartLine Line { get; set; }
        public MobilePhone Phone { get; set; }
    }
}