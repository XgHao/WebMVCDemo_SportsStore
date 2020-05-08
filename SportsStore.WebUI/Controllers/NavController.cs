using SportsStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IProductRepository repository;
        public NavController(IProductRepository _repository)
        {
            repository = _repository;
        }
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult Menu(string category = null)
        {
            //已选中的分类
            ViewBag.SelectedCategory = category;

            var categories = repository.Products.Select(x => x.Category).Distinct().OrderBy(x => x).ToList();

            return PartialView(categories);
        }
    }
}