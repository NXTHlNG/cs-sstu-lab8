using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cs_sstu_lab8.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedDate { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public List<OrderItem> Items { get; set; }
    }
}
