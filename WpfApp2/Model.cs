using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class CartItem
    {
        public Products Product { get; set; }
        public int Count { get; set; }
    }

    public static class Cart
    {
        public static List<CartItem> Items = new List<CartItem>();

        public static void Add(Products product)
        {
            var existing = Items.FirstOrDefault(i => i.Product.ID == product.ID);
            if (existing != null)
            {
                existing.Count++;
            }
            else
            {
                Items.Add(new CartItem { Product = product, Count = 1 });
            }
        }
        public static void Decrease(Products product)
        {
            var existing = Items.FirstOrDefault(i => i.Product.ID == product.ID);
            if (existing != null)
            {
                existing.Count--;
                if (existing.Count <= 0)
                {
                    Items.Remove(existing);
                }
            }
        }
        public static decimal GetTotalSum()
        {
            return Items.Sum(item => (item.Product.Price ?? 0) * item.Count);
        }

        public static void Clear()
        {
            Items.Clear();
        }
    }
}

