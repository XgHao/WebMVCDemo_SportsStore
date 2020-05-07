using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.HtmlHelpers
{
    /// <summary>
    /// 分页HTML辅助器
    /// </summary>
    public static class PagingHelper
    {
        /// <summary>
        /// 分页链接
        /// </summary>
        /// <param name="html"></param>
        /// <param name="pagingInfo">分页模型</param>
        /// <param name="pageUrl">页码链接的委托</param>
        /// <returns></returns>
        public static MvcHtmlString PageLinks(this HtmlHelper html,PagingInfo pagingInfo,Func<int,string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            //遍历页码
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                //当前页添加不同样式
                if (i == pagingInfo.CurrentPage)
                {
                    tag.AddCssClass("selected btn btn-primary");
                }
                else
                {
                    tag.AddCssClass("btn btn-default");
                }
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}