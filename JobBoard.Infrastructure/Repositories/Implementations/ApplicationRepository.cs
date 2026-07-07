using JobBoard.Domain.Entities;
using JobBoard.Infrastructure.Repositories.Interfaces;

namespace JobBoard.Infrastructure.Repositories.Implementations;

public class ApplicationRepository : Repository<Application>, IApplicationRepository
{
    public ApplicationRepository(JobBoardContext context) : base(context)
    {
    }
}
