using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUI.Models
{
   
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        public string UserIp { get; set; }
        public List<CartLine> CartLines = new List<CartLine>();
        
        public void AddItem(MobilePhone mobile, int quantity)
        {

            CartLine line = CartLines.Where(b => b.MobilePhoneId == mobile.MobilePhoneId).FirstOrDefault();
            
            if (line == null)
            {
                CartLines.Add(new CartLine {  MobilePhoneId = mobile.MobilePhoneId, Cart = this, Quantity = quantity });
            }
            else
            {
                
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(MobilePhone mobile)
        {
            CartLines.RemoveAll(l => l.MobilePhoneId == mobile.MobilePhoneId);
        }

      
        public void Clear()
        {
            CartLines.Clear();
        }
    }

    public class CartLine
    {
        [Key]
        public int CartLineId { get; set; }
        public Cart Cart { get; set; }
        public int CartId { get; set; }
       // public  MobilePhone MobilePhone { get; set; }
        public int MobilePhoneId { get; set; }
        public int Quantity { get; set; }

    }
}
