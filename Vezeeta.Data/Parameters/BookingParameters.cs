using Vezeeta.Core.Enums;

namespace Vezeeta.Data.Parameters;

public class BookingParameters
{
    public PaginationParameters paginationParameters {  get; set; } = new PaginationParameters();
    public Days Day { get; set; }
}
