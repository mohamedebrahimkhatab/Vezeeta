namespace Vezeeta.Data.Parameters;

public class DoctorParameters
{
    public PaginationParameters PaginationParameters { get; set; } = new();
    public NameParameters NameParameters { get; set; } = new();
    public int SpecializeId { get; set; }
}
