
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class CartController : Controller
    {  
        
        private IMobileRepository repository;
        private EFDbContext db = new EFDbContext();
       
        public CartController(IMobileRepository repo)
        {
            repository = repo;
        }

        public Cart GetCart()
        {
           
            Cart cart = (Cart) Session["Cart"];
            if (cart == null)
            {
                if (db.Carts.Count() > 0 )
                {
                    cart = db.Carts.Include("CartLines").Where(c => c.UserIp == Request.UserHostAddress).FirstOrDefault();
                    cart.CartLines = new List<CartLine>();
                    cart.CartLines.AddRange(db.CartLines.Where(l => l.CartId == cart.CartId).ToList());
                 
                    Session["Cart"] = cart;

                }
                else
                {
                   cart = new Cart();
                   cart.CartLines = new List<CartLine>();
                   cart.UserIp = Request.UserHostAddress;
                   Session["Cart"] = cart;
                }
                
            }
            return cart;
        }

        public ViewResult Index(string returnUrl)
        {
                   
            Cart cart = GetCart();
            var cartLines = new List<CartLineView> ();
            decimal total = 0;
            foreach(var line in cart.CartLines)
            {
              
              var phone = db.MobilePhones.Where(m => m.MobilePhoneId == line.MobilePhoneId).FirstOrDefault();
              CartLineView lineView = new CartLineView() {Phone = phone, Line = line };
              cartLines.Add(lineView);
              total += phone.Price * line.Quantity;
            }
             
            return View(new CartIndexViewModel
            {
                CartLines = cartLines,
                TotalValue = total,
                ReturnUrl = returnUrl

            });
        }
        public RedirectToRouteResult AddToCart(int MobilePhoneId, string returnUrl)
        {
            MobilePhone mp = repository.MobilePhones.FirstOrDefault(b => b.MobilePhoneId == MobilePhoneId);
            if (mp != null)
            {
                GetCart().AddItem(mp, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }


        public RedirectToRouteResult RemoveFromCart(int MobilePhoneId, string returnUrl)
        {
            MobilePhone mp = repository.MobilePhones.FirstOrDefault(b => b.MobilePhoneId == MobilePhoneId);
            if (mp != null)
            {
                GetCart().RemoveLine(mp);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public ActionResult Buy(string address, int MobilePhoneId = 0, string returnUrl = "")
        {
            if (address.Trim() == "") 
            {
               ViewBag.Error = "Purchase failed, because you have entered empty address.";
               
               return View(new CartIndexViewModel
               {
                   CartLines = new List<CartLineView>(),
                   TotalValue = 0,
                   ReturnUrl = returnUrl

               });
            }
            Cart cart = GetCart();
            List<CartLineView> orders = new List<CartLineView>();
            decimal total = 0;
            var phone = new MobilePhone();
            foreach (var line in (MobilePhoneId == 0 ? cart.CartLines : cart.CartLines.Where(m => m.MobilePhoneId == MobilePhoneId)))
            {
                phone = db.MobilePhones.Where(m => m.MobilePhoneId == line.MobilePhoneId).FirstOrDefault();
                CartLineView lineView = new CartLineView() { Phone = phone, Line = line };
                orders.Add(lineView);
                total += phone.Price * line.Quantity;
            }

            if (MobilePhoneId == 0) cart.Clear();
            else cart.RemoveLine(phone);

            return View(new CartIndexViewModel
            {
                CartLines = orders,
                TotalValue = total,
                ReturnUrl = returnUrl

            });
        }

        [HttpGet]
        public void SaveCartToDataBase()
        {
          /*  Cart cart = (Cart) Session["Cart"];
            
            if (cart != null)
            {

               var currentCart = db.Carts.Where(c => c.UserIp == Request.UserHostAddress).FirstOrDefault();
               
               if (currentCart != null)
               {
                   db.Carts.Remove(currentCart);
                   db.Carts.Add(cart);
                                                    
               }
               else db.Carts.Add(cart);
              
               db.SaveChanges();
                                                                
            }  */
        }
	}
}