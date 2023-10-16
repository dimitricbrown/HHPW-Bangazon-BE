using System.ComponentModel.DataAnnotations;

namespace HHPW_Bangazon_BE.Models
{
    public class Orders
    {
        public int Id { get; set; }
        [Required]
        public string OrderName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string Type { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentType { get; set; }
        public decimal OrderTotal { get; set; }
        public int Rating { get; set; }
        public int UserId { get; set; }
        public Users User { get; set; }
        public ICollection<Items> Items { get; set; }

    }
}
