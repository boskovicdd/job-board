using JobBoard.Domain.Entities;
using JobBoard.Infrastructure.Uow;
using MediatR;

namespace JobBoard.Application.Features.Applicants;

public record CreateApplicantCommand(string FirstName, string LastName, string Email, int YearsOfExperience) : IRequest<Applicant>;

public class CreateApplicantCommandHandler : IRequestHandler<CreateApplicantCommand, Applicant>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateApplicantCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<Applicant> Handle(CreateApplicantCommand request, CancellationToken cancellationToken)
    {
        var emailTaken = _unitOfWork.Applicants.Find(a => a.Email == request.Email).Any();
        if (emailTaken)
        {
            throw new InvalidOperationException("Email is already in use");
        }

        var applicant = new Applicant
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            YearsOfExperience = request.YearsOfExperience
        };

        _unitOfWork.Applicants.Add(applicant);
        _unitOfWork.SaveChanges();

        return Task.FromResult(applicant);
    }
}
