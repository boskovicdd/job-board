using JobBoard.Domain.Entities;
using JobBoard.Infrastructure.Uow;
using MediatR;

namespace JobBoard.Application.Features.JobPostings;

public record UpdateJobPostingCommand(int Id, string Title, string Description, DateTime Deadline) : IRequest<JobPosting>;

public class UpdateJobPostingCommandHandler : IRequestHandler<UpdateJobPostingCommand, JobPosting>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateJobPostingCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<JobPosting> Handle(UpdateJobPostingCommand request, CancellationToken cancellationToken)
    {
        var jobPosting = _unitOfWork.JobPostings.GetById(request.Id)
            ?? throw new InvalidOperationException("Job posting not found");

        jobPosting.Title = request.Title;
        jobPosting.Description = request.Description;
        jobPosting.Deadline = request.Deadline;

        _unitOfWork.JobPostings.Update(jobPosting);
        _unitOfWork.SaveChanges();

        return Task.FromResult(jobPosting);
    }
}
