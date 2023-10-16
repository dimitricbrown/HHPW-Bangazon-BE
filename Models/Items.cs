using System.ComponentModel.DataAnnotations;

namespace HHPW_Bangazon_BE.Models
{
    public class Items
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ICollection<Orders> Orders { get; set; }
    }
}
