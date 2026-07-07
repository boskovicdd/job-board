using JobBoard.Domain.Entities;
using JobBoard.Infrastructure.Uow;
using MediatR;

namespace JobBoard.Application.Features.Applicants;

public record GetApplicantsQuery : IRequest<IEnumerable<Applicant>>;

public class GetApplicantsQueryHandler : IRequestHandler<GetApplicantsQuery, IEnumerable<Applicant>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetApplicantsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<IEnumerable<Applicant>> Handle(GetApplicantsQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_unitOfWork.Applicants.GetAll("Applications.JobPosting"));
    }
}
