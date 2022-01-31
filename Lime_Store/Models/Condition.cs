using System.Collections.Generic;

#nullable disable

namespace Lime_Store.Models
{
    public partial class Condition
    {
        public Condition()
        {
            Products = new HashSet<Product>();
        }

        public int ConditionId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
