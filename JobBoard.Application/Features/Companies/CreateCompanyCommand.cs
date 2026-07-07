using JobBoard.Domain.Entities;
using JobBoard.Infrastructure.Uow;
using MediatR;

namespace JobBoard.Application.Features.Companies;

public record CreateCompanyCommand(string Name, string Industry, string City) : IRequest<Company>;

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, Company>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCompanyCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<Company> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = new Company
        {
            Name = request.Name,
            Industry = request.Industry,
            City = request.City
        };

        _unitOfWork.Companies.Add(company);
        _unitOfWork.SaveChanges();

        return Task.FromResult(company);
    }
}
