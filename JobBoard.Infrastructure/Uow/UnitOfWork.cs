using JobBoard.Infrastructure.Repositories.Implementations;
using JobBoard.Infrastructure.Repositories.Interfaces;

namespace JobBoard.Infrastructure.Uow;

public class UnitOfWork : IUnitOfWork
{
    private readonly JobBoardContext _context;
    private bool _disposed;

    public UnitOfWork(JobBoardContext context)
    {
        _context = context;
        Companies = new CompanyRepository(_context);
        JobPostings = new JobPostingRepository(_context);
        Applicants = new ApplicantRepository(_context);
        Applications = new ApplicationRepository(_context);
    }

    public ICompanyRepository Companies { get; }
    public IJobPostingRepository JobPostings { get; }
    public IApplicantRepository Applicants { get; }
    public IApplicationRepository Applications { get; }

    public int SaveChanges() => _context.SaveChanges();

    public void Dispose()
    {
        if (!_disposed)
        {
            _context.Dispose();
            _disposed = true;
        }
    }
}
