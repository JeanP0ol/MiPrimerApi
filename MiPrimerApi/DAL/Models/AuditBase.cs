namespace MiPrimerApi.DAL.Models
{
    public class AuditBase
    {
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
    }
}
