using SportsStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;

        public ProductController(IProductRepository _repository)
        {
            repository = _repository;
        }
        
        //[HttpGet]
        //public ActionResult List()
        //{
        //    return View(repository.Products);
        //}

        public int PageSize = 4;
        public ViewResult List(int page = 1)
        {
            return View(repository.Products.Skip((page - 1) * PageSize).Take(PageSize));
        }
    }
}