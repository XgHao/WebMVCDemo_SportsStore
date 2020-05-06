using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Concrete
{
    /// <summary>
    /// 商品存储库
    /// </summary>
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();

        /// <summary>
        /// 商品集合
        /// </summary>
        public IEnumerable<Product> Products => context.Products;
    }
}
