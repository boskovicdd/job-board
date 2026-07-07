using JobBoard.Domain.Entities;
using JobBoard.Infrastructure.Uow;
using MediatR;

namespace JobBoard.Application.Features.Companies;

public record UpdateCompanyCommand(int Id, string Name, string Industry, string City) : IRequest<Company>;

public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, Company>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCompanyCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<Company> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = _unitOfWork.Companies.GetById(request.Id)
            ?? throw new InvalidOperationException("Company not found");

        company.Name = request.Name;
        company.Industry = request.Industry;
        company.City = request.City;

        _unitOfWork.Companies.Update(company);
        _unitOfWork.SaveChanges();

        return Task.FromResult(company);
    }
}
