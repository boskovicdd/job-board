using JobBoard.Domain.Entities;
using JobBoard.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Infrastructure;

public class JobBoardContext : DbContext
{
    public JobBoardContext(DbContextOptions<JobBoardContext> options) : base(options)
    {
    }

    public DbSet<Company> Companies => Set<Company>();
    public DbSet<JobPosting> JobPostings => Set<JobPosting>();
    public DbSet<Applicant> Applicants => Set<Applicant>();
    public DbSet<Application> Applications => Set<Application>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<JobPosting>()
            .HasOne(jp => jp.Company)
            .WithMany(c => c.JobPostings)
            .HasForeignKey(jp => jp.CompanyId);

        modelBuilder.Entity<Application>()
            .HasOne(a => a.JobPosting)
            .WithMany(jp => jp.Applications)
            .HasForeignKey(a => a.JobPostingId);

        modelBuilder.Entity<Application>()
            .HasOne(a => a.Applicant)
            .WithMany(ap => ap.Applications)
            .HasForeignKey(a => a.ApplicantId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Application>()
            .HasIndex(a => new { a.JobPostingId, a.ApplicantId })
            .IsUnique();

        modelBuilder.Entity<Company>().HasData(
            new Company { Id = 1, Name = "TechCorp", Industry = "IT", City = "Belgrade" },
            new Company { Id = 2, Name = "FinancePro", Industry = "Finance", City = "Novi Sad" },
            new Company { Id = 3, Name = "BuildIt", Industry = "Construction", City = "Nis" }
        );

        modelBuilder.Entity<JobPosting>().HasData(
            new JobPosting
            {
                Id = 1,
                Title = "Backend Developer",
                Description = "Razvoj i odrzavanje backend servisa.",
                PositionsTotal = 3,
                PositionsFilled = 1,
                Deadline = new DateTime(2026, 9, 1),
                Status = PostingStatus.Open,
                CompanyId = 1
            },
            new JobPosting
            {
                Id = 2,
                Title = "Frontend Developer",
                Description = "Razvoj korisnickog interfejsa.",
                PositionsTotal = 2,
                PositionsFilled = 0,
                Deadline = new DateTime(2026, 9, 1),
                Status = PostingStatus.Open,
                CompanyId = 1
            },
            new JobPosting
            {
                Id = 3,
                Title = "Accountant",
                Description = "Vodjenje finansijske evidencije.",
                PositionsTotal = 1,
                PositionsFilled = 0,
                Deadline = new DateTime(2026, 5, 1),
                Status = PostingStatus.Closed,
                CompanyId = 2
            },
            new JobPosting
            {
                Id = 4,
                Title = "Site Engineer",
                Description = "Nadzor gradjevinskih radova.",
                PositionsTotal = 2,
                PositionsFilled = 0,
                Deadline = new DateTime(2026, 6, 1),
                Status = PostingStatus.Open,
                CompanyId = 3
            }
        );

        modelBuilder.Entity<Applicant>().HasData(
            new Applicant { Id = 1, FirstName = "Marko", LastName = "Markovic", Email = "marko.markovic@example.com", YearsOfExperience = 2 },
            new Applicant { Id = 2, FirstName = "Jovana", LastName = "Jovanovic", Email = "jovana.jovanovic@example.com", YearsOfExperience = 5 },
            new Applicant { Id = 3, FirstName = "Petar", LastName = "Petrovic", Email = "petar.petrovic@example.com", YearsOfExperience = 1 },
            new Applicant { Id = 4, FirstName = "Ana", LastName = "Anic", Email = "ana.anic@example.com", YearsOfExperience = 3 }
        );

        modelBuilder.Entity<Application>().HasData(
            new Application
            {
                Id = 1,
                AppliedAt = new DateTime(2026, 4, 1),
                CoverLetter = "Zainteresovan sam za poziciju Frontend Developer-a.",
                Status = ApplicationStatus.Pending,
                JobPostingId = 2,
                ApplicantId = 1
            },
            new Application
            {
                Id = 2,
                AppliedAt = new DateTime(2026, 3, 15),
                CoverLetter = "Imam petogodisnje iskustvo u backend razvoju.",
                Status = ApplicationStatus.Accepted,
                JobPostingId = 1,
                ApplicantId = 2
            },
            new Application
            {
                Id = 3,
                AppliedAt = new DateTime(2026, 4, 10),
                CoverLetter = null,
                Status = ApplicationStatus.Rejected,
                JobPostingId = 3,
                ApplicantId = 3
            }
        );
    }
}
