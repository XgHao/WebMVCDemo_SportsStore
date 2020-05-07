using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsStore.WebUI.Models.ViewModels
{
    /// <summary>
    /// 产品列表视图模型
    /// </summary>
    public class ProductListViewModel
    {
        /// <summary>
        /// 产品集合
        /// </summary>
        public List<Product> Products { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        public PagingInfo PagingInfo { get; set; }

        /// <summary>
        /// 当前分类
        /// </summary>
        public string CurrentCategory { get; set; }
    }
}