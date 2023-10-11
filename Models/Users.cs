using System.ComponentModel.DataAnnotations;

namespace HHPW_Bangazon_BE.Models
{
    public class Users
    {
        public int Id { get; set; }
        [Required]
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Uid { get; set; }
    }
}
