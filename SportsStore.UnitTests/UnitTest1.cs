using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            //准备
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product{ Id = 1, Name = "P1" },
                new Product{ Id = 2, Name = "P2" },
                new Product{ Id = 3, Name = "P3" },
                new Product{ Id = 4, Name = "P4" },
                new Product{ Id = 5, Name = "P5" },
            });
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //动作
            List<Product> result = (controller.List(2).Model as IEnumerable<Product>).ToList();

            //断言
            Assert.IsTrue(result.Count == 2);
            Assert.AreEqual(result[0].Name, "P4");
            Assert.AreEqual(result[1].Name, "P5");
        }
    }
}
