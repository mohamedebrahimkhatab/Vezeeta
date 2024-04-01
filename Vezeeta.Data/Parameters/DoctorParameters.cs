namespace Vezeeta.Data.Parameters;

public class DoctorParameters : QueryStringParameters
{
    //public int MinYearOfBirth { get; set; }
    //public int MaxYearOfBirth { get; set; } = DateTime.Now.Year;

    //public bool ValidYearRange => MaxYearOfBirth >= MinYearOfBirth;

    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
}
