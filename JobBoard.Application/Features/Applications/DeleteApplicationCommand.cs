using JobBoard.Infrastructure.Uow;
using MediatR;

namespace JobBoard.Application.Features.Applications;

public record DeleteApplicationCommand(int Id) : IRequest<bool>;

public class DeleteApplicationCommandHandler : IRequestHandler<DeleteApplicationCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteApplicationCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<bool> Handle(DeleteApplicationCommand request, CancellationToken cancellationToken)
    {
        var application = _unitOfWork.Applications.GetById(request.Id);
        if (application is null)
        {
            return Task.FromResult(false);
        }

        _unitOfWork.Applications.Delete(application);
        _unitOfWork.SaveChanges();

        return Task.FromResult(true);
    }
}
