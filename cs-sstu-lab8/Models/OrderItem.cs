using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cs_sstu_lab8.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Amount { get; set; }
        
        [Required]
        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }
}
