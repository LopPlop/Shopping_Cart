using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Models
{
    public class Product
    {
        public int Id { get; set; }


        [Display(Name = "Название Продукта")]
        [Required(ErrorMessage = "Вам нужно ввести имя")]
        public string? Name { get; set; }

        [Display(Name = "Короткое Описание")]
        public string? ShortDescription { get; set; } = "ShortDescription";

        [Display(Name = "Расширенное Описание")]
        public string? LongDescription { get; set; } = "LongDescription";

        [Display(Name = "Изображение")]
        public string? Image { get; set; } = "default.png";

        [Display(Name = "Цена")]
        [Required(ErrorMessage = "Вам нужно ввести цену")]
        public int Price { get; set; }
        public bool IsInCart { get; set; } = false;

        [Display(Name = "Количество")]
        [Required(ErrorMessage = "Вам нужно ввести количество продукта")]
        public int Available { get; set; }
        public int CategoryId { get; set; } = 1;
    }
}
