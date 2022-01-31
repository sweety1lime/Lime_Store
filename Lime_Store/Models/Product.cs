using System.Collections.Generic;

#nullable disable

namespace Lime_Store.Models
{
    public partial class Product
    {
        public Product()
        {
            Orders = new HashSet<Order>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int AvailabilityId { get; set; }
        public int ConditionId { get; set; }
        public string Brand { get; set; }
        public string PictureMain { get; set; }
        public string PictureAddOne { get; set; }

        public virtual Availability Availability { get; set; }
        public virtual Brand BrandNavigation { get; set; }
        public virtual Condition Condition { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}
