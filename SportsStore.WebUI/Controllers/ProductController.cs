using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;
using SportsStore.WebUI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        /// <summary>
        /// 产品存储库
        /// </summary>
        private IProductRepository repository;

        public ProductController(IProductRepository _repository)
        {
            repository = _repository;
        }
        
        /// <summary>
        /// 分页每页数
        /// </summary>
        public int PageSize = 4;
        [HttpGet]
        public ViewResult List(string category, int page = 1)
        {
            var date = DateTime.Now.ToString();

            var prods = repository.Products
                                     .Where(p => category == null || p.Category == category);

            ProductListViewModel model = new ProductListViewModel
            {
                //筛选后的产品
                Products = prods.OrderBy(p => p.Id).Skip((page - 1) * PageSize).Take(PageSize).ToList(),
                //分页信息
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    TotalItems = prods.Count(),
                    ItemsPerPage = PageSize
                },
                //当前分类
                CurrentCategory = category
            };

            return View(model);
        }

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public FileContentResult GetImage(int Id)
        {
            var prod = repository.Products.FirstOrDefault(p => p.Id == Id);
            return prod == null ? null : File(prod.ImageData, prod.ImageMimeType);
        }
    }
}