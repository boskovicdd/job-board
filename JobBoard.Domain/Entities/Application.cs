using JobBoard.Domain.Enums;

namespace JobBoard.Domain.Entities;

public class Application
{
    public int Id { get; set; }
    public DateTime AppliedAt { get; set; }
    public string? CoverLetter { get; set; }
    public ApplicationStatus Status { get; set; }

    public int JobPostingId { get; set; }
    public JobPosting JobPosting { get; set; } = null!;

    public int ApplicantId { get; set; }
    public Applicant Applicant { get; set; } = null!;
}
