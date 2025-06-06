using System.ComponentModel.DataAnnotations.Schema;

namespace AnimalClinic.DataAccess.Entities;

[Table("Visit")]
public class VisitEntity : BaseEntity
{
    public string? Office { get; set; }
    public DateOnly? Date { get; set; }
    public TimeOnly? Time { get; set; }
    public required ClientEntity Client { get; set; }
}
