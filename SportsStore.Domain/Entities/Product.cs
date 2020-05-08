using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Razor.Generator;

namespace SportsStore.Domain.Entities
{
    /// <summary>
    /// 商品类
    /// </summary>
    public class Product
    {
        /// <summary>
        /// ID
        /// </summary>  
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "Please enter a product name")]
        public string Name { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        [Required]
        [Range(0.01,double.MaxValue,ErrorMessage = "Please enter a positive price")]
        public decimal Price { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        [Required(ErrorMessage = "Please specifty a category")]
        public string Category { get; set; }


        public byte[] ImageData { get; set; }

        public string ImageMimeType { get; set; }
    }
}
