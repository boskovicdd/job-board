using JobBoard.Infrastructure.Uow;
using MediatR;

namespace JobBoard.Application.Features.Applications;

using JobBoard.Domain.Entities;

public record GetApplicationsByPostingQuery(int JobPostingId) : IRequest<IEnumerable<Application>>;

public class GetApplicationsByPostingQueryHandler : IRequestHandler<GetApplicationsByPostingQuery, IEnumerable<Application>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetApplicationsByPostingQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<IEnumerable<Application>> Handle(GetApplicationsByPostingQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_unitOfWork.Applications.Find(a => a.JobPostingId == request.JobPostingId, "Applicant", "JobPosting"));
    }
}
