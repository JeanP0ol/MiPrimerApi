namespace MiPrimerApi.DAL.Models
{
    public class Movie : AuditBase
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Duration { get; set; }
        public string? Description { get; set; }
        public string Clasification { get; set; } = string.Empty;
    }
}
