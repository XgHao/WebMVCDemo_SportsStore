using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Entities
{
    /// <summary>
    /// 购物车
    /// </summary>
    public class Cart
    {
        /// <summary>
        /// 购物车商品信息字典集合
        /// </summary>
        public Dictionary<Product, int> CartLine { get; set; } = new Dictionary<Product, int>();

        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="product"></param>
        /// <param name="quantity"></param>
        public void AddItem(Product product, int quantity = 1)
        {
            //尝试获取
            if (CartLine.TryGetValue(product, out int curCnt))
            {
                CartLine[product] = curCnt + quantity;
                return;
            }

            CartLine.Add(product, quantity);
        }

        /// <summary>
        /// 移除商品
        /// </summary>
        /// <param name="product"></param>
        public void RemoveItem(Product product)
        {
            //判断如果减一为0了后，删除
            if (CartLine.TryGetValue(product, out int curCnt) && curCnt <= 1) 
            {
                CartLine.Remove(product);
                return;
            }
            //否则数量减一
            CartLine[product]--;
        }

        /// <summary>
        /// 总价
        /// </summary>
        /// <returns></returns>
        public decimal ComputeTotalValue()
        {
            return CartLine.Sum(p => p.Key.Price * p.Value);
        }

        /// <summary>
        /// 清空购物车
        /// </summary>
        public void Clear()
        {
            CartLine.Clear();
        }
    }
}
