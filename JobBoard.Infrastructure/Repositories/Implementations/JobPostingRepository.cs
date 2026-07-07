using JobBoard.Domain.Entities;
using JobBoard.Infrastructure.Repositories.Interfaces;

namespace JobBoard.Infrastructure.Repositories.Implementations;

public class JobPostingRepository : Repository<JobPosting>, IJobPostingRepository
{
    public JobPostingRepository(JobBoardContext context) : base(context)
    {
    }
}
