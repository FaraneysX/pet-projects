using System.ComponentModel.DataAnnotations.Schema;

namespace AnimalClinic.DataAccess.Entities;

[Table("Animal")]
public class AnimalEntity : BaseEntity
{
    public string? Nickname { get; set; }
    public string? Kind { get; set; }
    public string? Breed { get; set; }
    public required ClientEntity Client { get; set; }
}
