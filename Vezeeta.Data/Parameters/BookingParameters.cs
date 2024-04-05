using Vezeeta.Core.Enums;

namespace Vezeeta.Data.Parameters;

public class BookingParameters : PaginationParameters
{
    public Days Day { get; set; }
}
