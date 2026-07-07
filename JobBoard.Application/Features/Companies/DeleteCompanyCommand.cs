using JobBoard.Infrastructure.Uow;
using MediatR;

namespace JobBoard.Application.Features.Companies;

public record DeleteCompanyCommand(int Id) : IRequest<bool>;

public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCompanyCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<bool> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = _unitOfWork.Companies.GetById(request.Id);
        if (company is null)
        {
            return Task.FromResult(false);
        }

        _unitOfWork.Companies.Delete(company);
        _unitOfWork.SaveChanges();

        return Task.FromResult(true);
    }
}
