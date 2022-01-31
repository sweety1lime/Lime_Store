#nullable disable

namespace Lime_Store.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Address { get; set; }
        public string Index { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public long Phone { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

       
    }
}
