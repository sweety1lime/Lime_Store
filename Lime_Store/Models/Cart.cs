using System.Collections.Generic;

namespace Lime_Store.Models
{
    public class Cart : CheckOut
    {
        public Cart()
        {
            CartLines = new List<Product>();
        }
        public List<Product> CartLines { get; set; }

        //CartLines.Count(x => x.ProductId == 1);
        //cart.ToList();
        public int ProductCount { get; set; }
        public List<Product> CartFiltered
        {
            get
            {
                List<Product> cart = new List<Product>();
                for (int i = 1; i <= ProductCount; i++)
                {
                    var prod = CartLines.Find(item => item.ProductId == i);
                    if (prod != null)
                        cart.Add(prod);
                }
                return cart;
            }
        }
        // Вычисление итоговой стоимости
        public decimal FinalPrice
        {
            get
            {
                decimal sum = 0;
                foreach (Product product in CartLines)
                    sum += product.Price;

                return sum;
            }
        }
    }
}
