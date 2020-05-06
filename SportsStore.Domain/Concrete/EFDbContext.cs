using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Concrete
{
    /// <summary>
    /// EF数据库上下文
    /// </summary>
    public class EFDbContext : DbContext
    {
        /// <summary>
        /// Products为对应表名
        /// </summary>
        public DbSet<Product> Products { get; set; }
    }
}
