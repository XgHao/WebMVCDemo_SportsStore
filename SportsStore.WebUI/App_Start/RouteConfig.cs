using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SportsStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //空链接
            routes.MapRoute(
                name: "null",
                url: "",
                defaults: new { controller = "Product", action = "List", Category = (string)null, page = 1 }
            );

            //分页-页码
            routes.MapRoute(
                name: "PageList",
                url: "Page{page}",
                defaults: new { controller = "Product", action = "List", category = (string)null },
                constraints: new { page = @"\d+" }
            );

            //分页-分类
            routes.MapRoute(
                name: "PageCategory",
                url: "{category}",
                defaults: new { controller = "Product", action = "List", page = 1 }
            );

            //分页-分类页码
            routes.MapRoute(
                name: "PageCategoryPage",
                url: "{category}/{page}",
                defaults: new { controller = "Product", action = "List" },
                constraints: new { page = @"\d+" }
            );

            //分页-全
            routes.MapRoute(
                name: "FULL",
                url: "{controller}/{action}/{category}/{page}",
                defaults: new { controller = "Product", action = "List" },
                constraints: new { page = @"\d+" }
            );


            //默认
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Product", action = "List", id = UrlParameter.Optional }
            );
        }
    }
}
