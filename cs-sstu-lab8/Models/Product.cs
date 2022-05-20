using System.ComponentModel.DataAnnotations;

namespace cs_sstu_lab8.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        
        [Required]
        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        [Required]
        public string PictureUrl { get; set; }
    }
}
