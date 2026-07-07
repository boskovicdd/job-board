using JobBoard.Infrastructure.Repositories.Interfaces;

namespace JobBoard.Infrastructure.Uow;

public interface IUnitOfWork : IDisposable
{
    ICompanyRepository Companies { get; }
    IJobPostingRepository JobPostings { get; }
    IApplicantRepository Applicants { get; }
    IApplicationRepository Applications { get; }

    int SaveChanges();
}
