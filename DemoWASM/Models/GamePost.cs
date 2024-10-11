using System.ComponentModel.DataAnnotations;

namespace DemoWASM.Models
{
    public class GamePost
    {
        [Required]
        public string Title { get; set; }
        [Required]
        [Range(1980, 2025)]
        public int ReleaseYear { get; set; }
        [Required]
        public string Synopsis { get; set; }
    }
}
