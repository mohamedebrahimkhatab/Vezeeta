namespace Vezeeta.Data.Parameters;

public class PatientParameters
{
    public PaginationParameters PaginationParameters { get; set; } = new();
    public NameParameters NameParameters { get; set; } = new();
}
