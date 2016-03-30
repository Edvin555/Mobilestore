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
            MobilePhone m1 = new MobilePhone { MobilePhoneId = 1, Name = "b1" };
            MobilePhone m2 = new MobilePhone { MobilePhoneId = 2, Name = "b2" };

            Cart cart = new Cart();
            cart.AddItem(m1, 1);
            cart.AddItem(m2, 1);

            List<CartLine> results = cart.CartLines.ToList();
            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].MobilePhoneId, m1.MobilePhoneId);

            Assert.AreEqual(results[1].MobilePhoneId, m2.MobilePhoneId);
        }

        [TestMethod]
        public void Can_Add_Quantity_for_Existing_Lines()
        {
            MobilePhone m1 = new MobilePhone { MobilePhoneId = 1, Name = "b1" };
            MobilePhone m2 = new MobilePhone { MobilePhoneId = 2, Name = "b2" };

            Cart cart = new Cart();
            cart.AddItem(m1, 1);
            cart.AddItem(m2, 1);
            cart.AddItem(m1, 5);

            List<CartLine> results = cart.CartLines.ToList();

            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Quantity, 6);

            Assert.AreEqual(results[1].Quantity, 1);
        }

        [TestMethod]
        public void Can_Remove_Lines()
        {
            MobilePhone m1 = new MobilePhone { MobilePhoneId = 1, Name = "b1" };
            MobilePhone m2 = new MobilePhone { MobilePhoneId = 2, Name = "b2" };

            Cart cart = new Cart();
            cart.AddItem(m1, 1);
            cart.AddItem(m2, 6);
            
            cart.RemoveLine(m2);
            
            Assert.AreEqual(cart.CartLines.Count(), 1);
            Assert.AreEqual(cart.CartLines.Where(l => l.MobilePhoneId == m2.MobilePhoneId).Count(), 0);
        }
    }
}
