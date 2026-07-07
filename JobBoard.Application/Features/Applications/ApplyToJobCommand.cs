using JobBoard.Domain.Enums;
using JobBoard.Infrastructure.Uow;
using MediatR;

namespace JobBoard.Application.Features.Applications;

using JobBoard.Domain.Entities;

public record ApplyToJobCommand(int JobPostingId, int ApplicantId, string? CoverLetter) : IRequest<Application>;

public class ApplyToJobCommandHandler : IRequestHandler<ApplyToJobCommand, Application>
{
    private readonly IUnitOfWork _unitOfWork;

    public ApplyToJobCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<Application> Handle(ApplyToJobCommand request, CancellationToken cancellationToken)
    {
        var jobPosting = _unitOfWork.JobPostings.GetById(request.JobPostingId)
            ?? throw new InvalidOperationException("Job posting not found");

        if (jobPosting.Status != PostingStatus.Open)
        {
            throw new InvalidOperationException("Job posting is closed");
        }

        if (jobPosting.Deadline < DateTime.Now)
        {
            throw new InvalidOperationException("Job posting deadline has passed");
        }

        var applicant = _unitOfWork.Applicants.GetById(request.ApplicantId)
            ?? throw new InvalidOperationException("Applicant not found");

        var alreadyApplied = _unitOfWork.Applications
            .Find(a => a.JobPostingId == jobPosting.Id && a.ApplicantId == applicant.Id)
            .Any();

        if (alreadyApplied)
        {
            throw new InvalidOperationException("Applicant has already applied to this job posting");
        }

        var application = new Application
        {
            JobPostingId = jobPosting.Id,
            ApplicantId = applicant.Id,
            CoverLetter = request.CoverLetter,
            Status = ApplicationStatus.Pending,
            AppliedAt = DateTime.Now
        };

        _unitOfWork.Applications.Add(application);
        _unitOfWork.SaveChanges();

        return Task.FromResult(application);
    }
}
