using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIAssginment.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        
        public virtual Category Category { get; set; } //Navigational Prop
    }
}
