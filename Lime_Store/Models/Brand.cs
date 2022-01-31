using System.Collections.Generic;

#nullable disable

namespace Lime_Store.Models
{
    public partial class Brand
    {
        public Brand()
        {
            Products = new HashSet<Product>();
        }

        public string NameBrand { get; set; }
        public string Characteristic { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
