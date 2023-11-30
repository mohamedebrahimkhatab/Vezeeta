namespace Vezeeta.Core.Models;

public class Doctor : BaseEntity
{
    public decimal? Price { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }

    public int SpecializationId { get; set; }
    public Specialization? Specialization { get; set; }
}
