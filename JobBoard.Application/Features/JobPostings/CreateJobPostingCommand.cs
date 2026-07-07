using JobBoard.Domain.Entities;
using JobBoard.Domain.Enums;
using JobBoard.Infrastructure.Uow;
using MediatR;

namespace JobBoard.Application.Features.JobPostings;

public record CreateJobPostingCommand(
    string Title,
    string Description,
    int PositionsTotal,
    DateTime Deadline,
    int CompanyId) : IRequest<JobPosting>;

public class CreateJobPostingCommandHandler : IRequestHandler<CreateJobPostingCommand, JobPosting>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateJobPostingCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<JobPosting> Handle(CreateJobPostingCommand request, CancellationToken cancellationToken)
    {
        var company = _unitOfWork.Companies.GetById(request.CompanyId)
            ?? throw new InvalidOperationException("Company not found");

        var jobPosting = new JobPosting
        {
            Title = request.Title,
            Description = request.Description,
            PositionsTotal = request.PositionsTotal,
            PositionsFilled = 0,
            Deadline = request.Deadline,
            Status = PostingStatus.Open,
            CompanyId = company.Id
        };

        _unitOfWork.JobPostings.Add(jobPosting);
        _unitOfWork.SaveChanges();

        return Task.FromResult(jobPosting);
    }
}
