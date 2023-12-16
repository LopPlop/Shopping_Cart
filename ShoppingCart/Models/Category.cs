using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ShoppingCart.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Display(Name = "Введите название категории")]
        [Required(ErrorMessage = "Вам нужно ввести название")]
        public string? Name { get; set; }

        [Display(Name = "Введите описание категории")]
        public string? Description { get; set; }

        public List<Product>? Products{ get; set;}
    }
}
