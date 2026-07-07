using JobBoard.Domain.Entities;
using JobBoard.Infrastructure.Uow;
using MediatR;

namespace JobBoard.Application.Features.JobPostings;

public record GetJobPostingByIdQuery(int Id) : IRequest<JobPosting?>;

public class GetJobPostingByIdQueryHandler : IRequestHandler<GetJobPostingByIdQuery, JobPosting?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetJobPostingByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<JobPosting?> Handle(GetJobPostingByIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_unitOfWork.JobPostings.GetById(request.Id));
    }
}
