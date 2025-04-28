using System.ComponentModel.DataAnnotations;

namespace WebAPIAssginment.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        
        public virtual ICollection<Product> Products { get; set; } //Navigational Prop
    }
}
