using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.HtmlHelpers;
using SportsStore.WebUI.Models;
using SportsStore.WebUI.Models.ViewModels;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly List<Product> products = new List<Product>
        {
            new Product{ Id = 1, Name = "P1", Category = "Soccer" },
            new Product{ Id = 2, Name = "P2", Category = "Soccer" },
            new Product{ Id = 3, Name = "P3", Category = "Water" },
            new Product{ Id = 4, Name = "P4", Category = "Soccer" },
            new Product{ Id = 5, Name = "P5", Category = "Water" },
        };

        [TestMethod]
        public void Can_Paginate()
        {
            //准备
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(products);
            ProductController controller = new ProductController(mock.Object)
            {
                PageSize = 3
            };

            //动作
            ProductListViewModel result = controller.List(null, 2).Model as ProductListViewModel;

            //断言
            Assert.IsTrue(result.Products.Count == 2);
            Assert.AreEqual(result.Products[0].Name, "P4");
            Assert.AreEqual(result.Products[1].Name, "P5");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            //准备-定义一个Html辅助器
            HtmlHelper myhelper = null;
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            //动作
            MvcHtmlString result = myhelper.PageLinks(pagingInfo, i => $"Page{i}");
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            //准备
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(products);

            ProductController controller = new ProductController(mock.Object)
            {
                PageSize = 3
            };

            //动作
            ProductListViewModel result = controller.List("Soccer", 1).Model as ProductListViewModel;
        }

        [TestMethod]
        public void Can_Create_Categories()
        {
            //准备
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(products);

            NavController controller = new NavController(mock.Object);

            //动作
            var result = controller.Menu().Model as List<string>;
        }

        [TestMethod]
        public void Indicates_Selected_Category()
        {
            //准备
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(products);

            NavController controller = new NavController(mock.Object);

            //动作
            var result = controller.Menu("Soccer").ViewBag.SelectedCategory;
        }

        [TestMethod]
        public void Generate_Category_Specific_Product_Count()
        {
            //准备
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(products);

            ProductController controller = new ProductController(mock.Object) { PageSize = 2 };

            //动作
            var result = controller.List("Water").Model as ProductListViewModel;
        }
    }
}
