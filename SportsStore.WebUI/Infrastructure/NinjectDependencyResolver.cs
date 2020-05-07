using Moq;
using Ninject;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Concrete;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;
        public NinjectDependencyResolver(IKernel _kernel)
        {
            kernel = _kernel;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        /// <summary>
        /// 绑定
        /// </summary>
        private void AddBindings()
        {
            #region 模拟商品数据
            //Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //mock.Setup(m => m.Products).Returns(new List<Product>
            //{
            //    new Product{ Name = "Football", Price = 25 },
            //    new Product{ Name = "Surf board", Price = 25 },
            //    new Product{ Name = "Running shoes", Price = 95 },
            //});
            //单例
            //kernel.Bind<IProductRepository>().ToConstant(mock.Object);
            #endregion


            #region 绑定商品存储库-单例
            kernel.Bind<IProductRepository>().To<EFProductRepository>().InSingletonScope();
            #endregion
        }
    }
}