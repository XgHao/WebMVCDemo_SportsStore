using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Abstract
{
    /// <summary>
    /// 储存库接口
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// 储存库-商品集合
        /// </summary>
        IEnumerable<Product> Products { get; }

        /// <summary>
        /// 保存商品
        /// </summary>
        /// <param name="product"></param>
        void SaveProduct(Product product);

        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="productId"></param>
        Product DeleteProduct(int productId);
    }
}
