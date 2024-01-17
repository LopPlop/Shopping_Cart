using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Surname { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? StreetAddress { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? Region { get; set; }
        public int Price { get; set; }
        [Phone]
        [Required]
        public string? PhoneNumber { get; set; }
        public List<Product>? Products { get; set; } 
    }
}
