using System.ComponentModel.DataAnnotations;

namespace ConsumeProductApi.Models
{
    public class ProductList
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public int Price { get; set; }
    }
}
