using JobBoard.Domain.Enums;
using JobBoard.Infrastructure.Uow;
using MediatR;

namespace JobBoard.Application.Features.Applications;

using JobBoard.Domain.Entities;

public record RejectApplicationCommand(int ApplicationId) : IRequest<Application>;

public class RejectApplicationCommandHandler : IRequestHandler<RejectApplicationCommand, Application>
{
    private readonly IUnitOfWork _unitOfWork;

    public RejectApplicationCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<Application> Handle(RejectApplicationCommand request, CancellationToken cancellationToken)
    {
        var application = _unitOfWork.Applications.GetById(request.ApplicationId)
            ?? throw new InvalidOperationException("Application not found");

        if (application.Status != ApplicationStatus.Pending)
        {
            throw new InvalidOperationException("Only pending applications can be rejected");
        }

        application.Status = ApplicationStatus.Rejected;

        _unitOfWork.Applications.Update(application);
        _unitOfWork.SaveChanges();

        return Task.FromResult(application);
    }
}
