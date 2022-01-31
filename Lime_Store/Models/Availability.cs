using System.Collections.Generic;

#nullable disable

namespace Lime_Store.Models
{
    public partial class Availability
    {
        public Availability()
        {
            Products = new HashSet<Product>();
        }

        public int AvailabilityId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
