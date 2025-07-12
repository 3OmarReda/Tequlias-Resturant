using System.ComponentModel.DataAnnotations;

namespace TequliasResturant.Models
{
    public class Category
    {
        [Key]
        public int CaregoryId { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}