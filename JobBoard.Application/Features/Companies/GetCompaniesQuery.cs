using JobBoard.Domain.Entities;
using JobBoard.Infrastructure.Uow;
using MediatR;

namespace JobBoard.Application.Features.Companies;

public record GetCompaniesQuery : IRequest<IEnumerable<Company>>;

public class GetCompaniesQueryHandler : IRequestHandler<GetCompaniesQuery, IEnumerable<Company>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCompaniesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<IEnumerable<Company>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_unitOfWork.Companies.GetAll("JobPostings.Applications.Applicant"));
    }
}
