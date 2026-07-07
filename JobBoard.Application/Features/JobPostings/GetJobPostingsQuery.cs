using JobBoard.Domain.Entities;
using JobBoard.Infrastructure.Uow;
using MediatR;

namespace JobBoard.Application.Features.JobPostings;

public record GetJobPostingsQuery : IRequest<IEnumerable<JobPosting>>;

public class GetJobPostingsQueryHandler : IRequestHandler<GetJobPostingsQuery, IEnumerable<JobPosting>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetJobPostingsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<IEnumerable<JobPosting>> Handle(GetJobPostingsQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_unitOfWork.JobPostings.GetAll("Company", "Applications.Applicant"));
    }
}
