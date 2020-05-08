using System;
using System.Runtime.Remoting;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class AdminTest
    {
        Product product = new Product { Id = 1, Name = "Test" };

        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            //准备
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            AdminController controller = new AdminController(mock.Object);

            //动作
            ActionResult result = controller.Edit(product);

            //断言
            mock.Verify(m => m.SaveProduct(product));

            //断言
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            //准备
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            AdminController controller = new AdminController(mock.Object);
            controller.ModelState.AddModelError("error", "error");

            //动作
            ActionResult result = controller.Edit(product);

            //断言-确认存储库未被调用
            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never);
            //断言-确认方法的结果类型
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
