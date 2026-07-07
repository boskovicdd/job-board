namespace JobBoard.Domain.Entities;

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Industry { get; set; } = null!;
    public string City { get; set; } = null!;

    public ICollection<JobPosting> JobPostings { get; set; } = new List<JobPosting>();
}
