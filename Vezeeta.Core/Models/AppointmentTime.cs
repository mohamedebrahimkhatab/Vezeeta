namespace Vezeeta.Core.Models;

public class AppointmentTime : BaseEntity
{
    public TimeOnly Time { get; set; }
    public int AppointmentId { get; set; }
    public Appointment Appointment { get; set; } = null!;
}
