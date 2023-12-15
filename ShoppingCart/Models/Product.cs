using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Models
{
    public class Product
    {
        public int Id { get; set; }


        [Display(Name = "Введите название продукта")]
        [Required(ErrorMessage = "Вам нужно ввести имя")]
        public string? Name { get; set; }

        [Display(Name = "Введите описание продукта")]
        public string? ShortDescription { get; set; } = "ShortDescription";

        [Display(Name = "Введите описание продукта")]
        public string? LongDescription { get; set; } = "LongDescription";

        [Display(Name = "Добавте изображение продукта")]
        public string? Image { get; set; } = "default.png";

        [Display(Name = "Введите цену продукта")]
        [Required(ErrorMessage = "Вам нужно ввести цену")]
        public int Price { get; set; }
        public bool IsInCart { get; set; } = false;

        [Display(Name = "Введите количество продукта")]
        [Required(ErrorMessage = "Вам нужно ввести количество продукта")]
        public int Available { get; set; }
        public int CategoryId { get; set; } = 1;
    }
}
