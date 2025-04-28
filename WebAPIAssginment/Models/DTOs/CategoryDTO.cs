namespace WebAPIAssginment.Models.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<string> ProductsNames { get; set; } 
    }
}
