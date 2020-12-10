using System.ComponentModel.DataAnnotations;

namespace API.Dto
{
    public class BasketItemDto
    {
        [Required]
         public int Id { get; set; }

         [Required]
        public string productName { get; set; }

        [Required]
        [Range(0.1,double.MaxValue,ErrorMessage="Price must be greater that zero")]
        public decimal price { get; set; }

        [Required]
        [Range(1,double.MaxValue,ErrorMessage="Quantity must at least 1")]
        public int Quantity { get; set; }

        [Required]
        public string pictureUrl { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string type { get; set; }
    }
}