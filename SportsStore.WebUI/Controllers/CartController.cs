using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        private IOrderProcessor orderProcessor;
        public CartController(IProductRepository _repository, IOrderProcessor _orderProcessor)
        {
            repository = _repository;
            orderProcessor = _orderProcessor;
        }

        public ViewResult Index(Cart cart, string returnUrl = "/Product/List")
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }


        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public RedirectToRouteResult AddToCart(Cart cart, int Id, string returnUrl)
        {
            var prod = repository.Products.FirstOrDefault(p => p.Id == Id);
            if (prod != null) 
            {
                cart.AddItem(prod);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public RedirectToRouteResult RemoveFromCart(Cart cart, int Id, string returnUrl)
        {
            var prod = repository.Products.FirstOrDefault(p => p.Id == Id);
            if (prod != null)
            {
                cart.RemoveItem(prod);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        /// <summary>
        /// 购物车概览
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        /// <summary>
        /// 结算
        /// </summary>
        /// <returns></returns>
        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        /// <summary>
        /// 结算【HttpPost】
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="shippingDetails"></param>
        /// <returns></returns>
        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.CartLine.Count == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }
    }
}