namespace MiPrimerApi.DAL.Models
{
    public class Category : AuditBase
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
