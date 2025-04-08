using System.ComponentModel.DataAnnotations;

namespace GameZone.Models
{
    public class Category
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}
