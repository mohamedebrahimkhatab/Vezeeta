using Vezeeta.Core.Enums;

namespace Vezeeta.Core.Contracts.AppointmentDtos;

public class DayDto
{
    public Days Day { get; set; }
    public List<string>? Times { get; set; }
}
