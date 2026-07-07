using JobBoard.Domain.Enums;

namespace JobBoard.Domain.Entities;

public class JobPosting
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int PositionsTotal { get; set; }
    public int PositionsFilled { get; set; }
    public DateTime Deadline { get; set; }
    public PostingStatus Status { get; set; }

    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;

    public ICollection<Application> Applications { get; set; } = new List<Application>();
}
