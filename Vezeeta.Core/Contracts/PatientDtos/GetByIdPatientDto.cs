using Vezeeta.Core.Contracts.BookingDtos;

namespace Vezeeta.Core.Contracts.PatientDtos;

public class GetByIdPatientDto
{
    public GetPatientDto? Details { get; set; }
    public IEnumerable<PatientGetBookingDto>? Bookings { get; set; }
}
