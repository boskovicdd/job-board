using JobBoard.Domain.Entities;
using JobBoard.Infrastructure.Uow;
using MediatR;

namespace JobBoard.Application.Features.Companies;

public record GetCompanyByIdQuery(int Id) : IRequest<Company?>;

public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, Company?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCompanyByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<Company?> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_unitOfWork.Companies.GetById(request.Id));
    }
}
