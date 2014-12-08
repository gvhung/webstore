using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Product:BaseEntity
    {
        [Required(ErrorMessage = "Пожалуйста, введите название продукта")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Пожалуйста, введите описание продукта")]
        public string Description { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Пожалуйста, введите цену продукта")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите объём продукта")]
        public string Weight { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите название категории продукта")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите название производителя продукта")]
        public string Manufacturer { get; set; }

        //public byte[] ImageData { get; set; }
        //
        //public string ImageMimeType { get; set; }
    }
}
