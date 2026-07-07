namespace JobBoard.Domain.Entities;

public class Applicant
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int YearsOfExperience { get; set; }

    public ICollection<Application> Applications { get; set; } = new List<Application>();
}
