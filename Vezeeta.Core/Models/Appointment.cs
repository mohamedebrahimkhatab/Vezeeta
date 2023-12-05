using Vezeeta.Core.Enums;

namespace Vezeeta.Core.Models;

public class Appointment : BaseEntity
{
    public Days Day { get; set; }
    public int DoctorId { get; set; }
    public Doctor? Doctor { get; set; }
    public List<AppointmentTime>? AppointmentTimes { get; set; }
}
