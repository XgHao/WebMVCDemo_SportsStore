using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repository;
        public AdminController(IProductRepository _repository)
        {
            repository = _repository;
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View(repository.Products);
        }

        /// <summary>
        /// 编辑商品
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ViewResult Edit(int Id = 1)
        {
            var prod = repository.Products.FirstOrDefault(p => p.Id == Id);
            return View(prod);
        }

        /// <summary>
        /// 编辑商品【HttpPost】
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }

                repository.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been saved";
                return RedirectToAction("Index");
            }
            else
            {
                //验证不通过
                return View(product);
            }
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <returns></returns>
        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            var deletedProd = repository.DeleteProduct(Id);
            if (deletedProd != null)
            {
                TempData["message"] = $"{deletedProd.Name} was deleted.";
            }
            return RedirectToAction("Index");
        }
    }
}