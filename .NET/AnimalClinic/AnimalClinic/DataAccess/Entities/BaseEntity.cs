using System.ComponentModel.DataAnnotations;

namespace AnimalClinic.DataAccess.Entities;

public abstract class BaseEntity
{
    [Key]
    public int? Id { get; set; }
}
