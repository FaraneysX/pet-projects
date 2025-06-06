using System.ComponentModel.DataAnnotations.Schema;

namespace AnimalClinic.DataAccess.Entities;

[Table("Client")]
public class ClientEntity : BaseEntity
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Patronymic { get; set; }
    public DateOnly? BirthDate { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public ICollection<AnimalEntity>? Animals { get; set; }
    public ICollection<VisitEntity>? Visits { get; set; }
}
