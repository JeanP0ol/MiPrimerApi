using System.ComponentModel.DataAnnotations;

namespace MiPrimerApi.DAL.Models.Dtos
{
    public class MovieCreateUpdateDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int Duration { get; set; }

        public string? Description { get; set; }

        [Required]
        [MaxLength(10)]
        public string Clasification { get; set; } = string.Empty;
    }
}
