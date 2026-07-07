using JobBoard.Domain.Entities;
using JobBoard.Infrastructure.Repositories.Interfaces;

namespace JobBoard.Infrastructure.Repositories.Implementations;

public class ApplicantRepository : Repository<Applicant>, IApplicantRepository
{
    public ApplicantRepository(JobBoardContext context) : base(context)
    {
    }
}
