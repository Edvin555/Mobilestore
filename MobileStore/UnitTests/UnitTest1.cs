using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;
using System.Collections.Generic;
using System.Linq;

using WebUI.Controllers;
using WebUI.Models;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            Mock<IMobileRepository> mock = new Mock<IMobileRepository>();
            mock.Setup(m => m.MobilePhones).Returns(new List<MobilePhone>
            {
                new MobilePhone {MobilePhoneId = 1, Name = "b1 "},
                new MobilePhone {MobilePhoneId = 2, Name = "b2 "},
                new MobilePhone {MobilePhoneId = 3, Name = "b3 "},
                new MobilePhone {MobilePhoneId = 4, Name = "b4 "},
                new MobilePhone {MobilePhoneId = 5, Name = "b5 "},
            });

            MobileController controller = new MobileController(mock.Object);

            controller.pageSize = 3;
            MobileListViewModel result = (MobileListViewModel)controller.List(null,"b",2).Model;

            List<MobilePhone> phones = result.MobilePhones.ToList();

            Assert.IsTrue(phones.Count == 2);
            Assert.AreEqual(phones[0].Name, "b4 ");
            Assert.AreEqual(phones[1].Name, "b5 ");

        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            Mock<IMobileRepository> mock = new Mock<IMobileRepository>();
            mock.Setup(m => m.MobilePhones).Returns(new List<MobilePhone>
            {
                new MobilePhone {MobilePhoneId = 1, Name = "b1"},
                new MobilePhone {MobilePhoneId = 2, Name = "b2"},
                new MobilePhone {MobilePhoneId = 3, Name = "b3"},
                new MobilePhone {MobilePhoneId = 4, Name = "b4"},
                new MobilePhone {MobilePhoneId = 5, Name = "b5"},
            });

            MobileController controller = new MobileController(mock.Object);
            controller.pageSize = 3;

            MobileListViewModel result = (MobileListViewModel)controller.List(null, "b", 2).Model;

            PageInfo pagingInfo = result.PageInfo;
            Assert.AreEqual(pagingInfo.CurrentPage, 2);
            Assert.AreEqual(pagingInfo.ItemsPerPage, 3);
        }
    }
}
