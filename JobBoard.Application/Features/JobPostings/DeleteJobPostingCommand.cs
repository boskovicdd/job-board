using JobBoard.Infrastructure.Uow;
using MediatR;

namespace JobBoard.Application.Features.JobPostings;

public record DeleteJobPostingCommand(int Id) : IRequest<bool>;

public class DeleteJobPostingCommandHandler : IRequestHandler<DeleteJobPostingCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteJobPostingCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<bool> Handle(DeleteJobPostingCommand request, CancellationToken cancellationToken)
    {
        var jobPosting = _unitOfWork.JobPostings.GetById(request.Id);
        if (jobPosting is null)
        {
            return Task.FromResult(false);
        }

        _unitOfWork.JobPostings.Delete(jobPosting);
        _unitOfWork.SaveChanges();

        return Task.FromResult(true);
    }
}
