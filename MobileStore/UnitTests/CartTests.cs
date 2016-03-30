using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;
using System.Linq;
using WebUI.Models;

namespace UnitTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void Can_Add_New_Lines()
        {
            MobilePhone book1 = new MobilePhone { MobilePhoneId = 1, Name = "b1" };
            MobilePhone book2 = new MobilePhone { MobilePhoneId = 2, Name = "b2" };

            Cart cart = new Cart();
            cart.AddItem(book1, 1);
            cart.AddItem(book2, 1);

            List<CartLine> results = cart.CartLines.ToList();
            Assert.AreEqual(results.Count(), 2);
           // Assert.AreEqual(results[0].MobilePhone, book1);

           // Assert.AreEqual(results[1].MobilePhone, book2);
        }

        [TestMethod]
        public void Can_Add_Quantity_for_Existing_Lines()
        {
            MobilePhone book1 = new MobilePhone { MobilePhoneId = 1, Name = "b1" };
            MobilePhone book2 = new MobilePhone { MobilePhoneId = 2, Name = "b2" };

            Cart cart = new Cart();
            cart.AddItem(book1, 1);
            cart.AddItem(book2, 1);
            cart.AddItem(book1, 5);

            List<CartLine> results = cart.CartLines.ToList();

            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Quantity, 6);

            Assert.AreEqual(results[1].Quantity, 1);
        }

        [TestMethod]
        public void Can_Remove_Lines()
        {
            MobilePhone book1 = new MobilePhone { MobilePhoneId = 1, Name = "b1" };
            MobilePhone book2 = new MobilePhone { MobilePhoneId = 2, Name = "b2" };

            Cart cart = new Cart();
            cart.AddItem(book1, 1);
            cart.AddItem(book2, 6);
            cart.RemoveLine(book2);
            
            //Assert.AreEqual(cart.Count(), 1);
            //Assert.AreEqual(cart.CartLines.Where(l => l.MobilePhone == book2).Count(), 0);
        }
    }
}
