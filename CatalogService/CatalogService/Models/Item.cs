using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogService.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category category { get; set; }
    }
}
