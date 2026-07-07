using JobBoard.Domain.Enums;
using JobBoard.Infrastructure.Uow;
using MediatR;

namespace JobBoard.Application.Features.Applications;

using JobBoard.Domain.Entities;

public record AcceptApplicationCommand(int ApplicationId) : IRequest<Application>;

public class AcceptApplicationCommandHandler : IRequestHandler<AcceptApplicationCommand, Application>
{
    private readonly IUnitOfWork _unitOfWork;

    public AcceptApplicationCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<Application> Handle(AcceptApplicationCommand request, CancellationToken cancellationToken)
    {
        var application = _unitOfWork.Applications.GetById(request.ApplicationId)
            ?? throw new InvalidOperationException("Application not found");

        if (application.Status != ApplicationStatus.Pending)
        {
            throw new InvalidOperationException("Only pending applications can be accepted");
        }

        var jobPosting = _unitOfWork.JobPostings.GetById(application.JobPostingId)
            ?? throw new InvalidOperationException("Job posting not found");

        application.Status = ApplicationStatus.Accepted;
        jobPosting.PositionsFilled += 1;

        if (jobPosting.PositionsFilled >= jobPosting.PositionsTotal)
        {
            jobPosting.Status = PostingStatus.Closed;

            var remainingPending = _unitOfWork.Applications.Find(a =>
                a.JobPostingId == jobPosting.Id &&
                a.Status == ApplicationStatus.Pending &&
                a.Id != application.Id);

            foreach (var pending in remainingPending)
            {
                pending.Status = ApplicationStatus.Rejected;
                _unitOfWork.Applications.Update(pending);
            }
        }

        _unitOfWork.Applications.Update(application);
        _unitOfWork.JobPostings.Update(jobPosting);
        _unitOfWork.SaveChanges();

        return Task.FromResult(application);
    }
}
