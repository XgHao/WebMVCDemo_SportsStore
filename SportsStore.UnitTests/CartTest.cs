using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models.ViewModels;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class CartTest
    {
        private Product p1 = new Product { Id = 1, Name = "P1" };
        private Product p2 = new Product { Id = 2, Name = "P2" };
        private Product p3 = new Product { Id = 3, Name = "P3" };

        [TestMethod]
        public void Can_Add_New_Lines()
        {
            //准备
            Cart target = new Cart();

            //动作
            target.AddItem(p1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 1);
            target.AddItem(p1, 5);

            var result = target.CartLine;

            target.RemoveItem(p1);
            target.RemoveItem(p2);
            target.RemoveItem(p2);
            target.RemoveItem(p3);

            var result1 = target.CartLine;
        }

        [TestMethod]
        public void Can_Add_To_Cart()
        {
            //准备
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product> { p1, p2, p3 });

            Cart cart = new Cart();
            CartController controller = new CartController(mock.Object, null) ;

            //动作
            controller.AddToCart(cart, 1, null);
        }

        [TestMethod]
        public void Adding_Product_To_Cart_Goes_To_Cart_Screen()
        {
            //准备
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product> { p1, p2, p3 });

            Cart cart = new Cart();
            CartController controller = new CartController(mock.Object, null) ;

            //动作
            var result = controller.AddToCart(cart, 2, "myUrl");
        }

        [TestMethod]
        public void Can_View_Cart_Contents()
        {
            //准备
            Cart cart = new Cart();

            CartController target = new CartController(null, null) ;

            //动作
            CartIndexViewModel result = target.Index(cart, "myUrl").ViewData.Model as CartIndexViewModel;
        }

        [TestMethod]
        public void Cannot_Checkout_Empty_Cart()
        {
            //准备
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            ShippingDetails shippingDetails = new ShippingDetails();
            CartController controller = new CartController(null, mock.Object);

            //动作
            ViewResult result = controller.Checkout(cart, shippingDetails);

            //断言
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never);

            Assert.AreEqual("", result.ViewName);

            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }
    }
}
