using HotChocolate;
using JobBoard.Application.Features.Applicants;
using JobBoard.Application.Features.Applications;
using JobBoard.Application.Features.Companies;
using JobBoard.Application.Features.JobPostings;
using MediatR;

namespace JobBoard.API.GraphQL;

using JobBoard.Domain.Entities;

public class Mutation
{
    public async Task<Company> CreateCompany(string name, string industry, string city, [Service] IMediator mediator, CancellationToken cancellationToken)
    {
        return await mediator.Send(new CreateCompanyCommand(name, industry, city), cancellationToken);
    }

    public async Task<Company> UpdateCompany(int id, string name, string industry, string city, [Service] IMediator mediator, CancellationToken cancellationToken)
    {
        return await mediator.Send(new UpdateCompanyCommand(id, name, industry, city), cancellationToken);
    }

    public async Task<bool> DeleteCompany(int id, [Service] IMediator mediator, CancellationToken cancellationToken)
    {
        return await mediator.Send(new DeleteCompanyCommand(id), cancellationToken);
    }

    public async Task<JobPosting> CreateJobPosting(string title, string description, int positionsTotal, DateTime deadline, int companyId, [Service] IMediator mediator, CancellationToken cancellationToken)
    {
        return await mediator.Send(new CreateJobPostingCommand(title, description, positionsTotal, deadline, companyId), cancellationToken);
    }

    public async Task<JobPosting> UpdateJobPosting(int id, string title, string description, DateTime deadline, [Service] IMediator mediator, CancellationToken cancellationToken)
    {
        return await mediator.Send(new UpdateJobPostingCommand(id, title, description, deadline), cancellationToken);
    }

    public async Task<bool> DeleteJobPosting(int id, [Service] IMediator mediator, CancellationToken cancellationToken)
    {
        return await mediator.Send(new DeleteJobPostingCommand(id), cancellationToken);
    }

    public async Task<Applicant> CreateApplicant(string firstName, string lastName, string email, int yearsOfExperience, [Service] IMediator mediator, CancellationToken cancellationToken)
    {
        return await mediator.Send(new CreateApplicantCommand(firstName, lastName, email, yearsOfExperience), cancellationToken);
    }

    public async Task<Application> ApplyToJob(int jobPostingId, int applicantId, string? coverLetter, [Service] IMediator mediator, CancellationToken cancellationToken)
    {
        return await mediator.Send(new ApplyToJobCommand(jobPostingId, applicantId, coverLetter), cancellationToken);
    }

    public async Task<Application> AcceptApplication(int applicationId, [Service] IMediator mediator, CancellationToken cancellationToken)
    {
        return await mediator.Send(new AcceptApplicationCommand(applicationId), cancellationToken);
    }

    public async Task<Application> RejectApplication(int applicationId, [Service] IMediator mediator, CancellationToken cancellationToken)
    {
        return await mediator.Send(new RejectApplicationCommand(applicationId), cancellationToken);
    }

    public async Task<bool> DeleteApplication(int id, [Service] IMediator mediator, CancellationToken cancellationToken)
    {
        return await mediator.Send(new DeleteApplicationCommand(id), cancellationToken);
    }
}
