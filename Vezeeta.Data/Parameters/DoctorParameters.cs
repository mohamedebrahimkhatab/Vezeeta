namespace Vezeeta.Data.Parameters;

public class DoctorParameters : PaginationParameters
{
    public string NameQuery { get; set; } = "";
    public int SpecializeId { get; set; }
}
