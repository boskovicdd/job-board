using JobBoard.Domain.Entities;
using JobBoard.Infrastructure.Repositories.Interfaces;

namespace JobBoard.Infrastructure.Repositories.Implementations;

public class CompanyRepository : Repository<Company>, ICompanyRepository
{
    public CompanyRepository(JobBoardContext context) : base(context)
    {
    }
}
