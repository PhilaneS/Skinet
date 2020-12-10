using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Dto
{
    public class CustomBasketDto
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItemDto> items { get; set; }
    }
}