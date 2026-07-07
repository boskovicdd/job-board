using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using JobBoard.Application.Features.Applicants;
using JobBoard.Application.Features.Applications;
using JobBoard.Application.Features.Companies;
using JobBoard.Application.Features.JobPostings;
using MediatR;

namespace JobBoard.API.GraphQL;

using JobBoard.Domain.Entities;

public class Query
{
    [UsePaging]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<Company>> Companies([Service] IMediator mediator, CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetCompaniesQuery(), cancellationToken);
    }

    public async Task<Company?> CompanyById(int id, [Service] IMediator mediator, CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetCompanyByIdQuery(id), cancellationToken);
    }

    [UsePaging]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<JobPosting>> JobPostings([Service] IMediator mediator, CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetJobPostingsQuery(), cancellationToken);
    }

    public async Task<JobPosting?> JobPostingById(int id, [Service] IMediator mediator, CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetJobPostingByIdQuery(id), cancellationToken);
    }

    [UsePaging]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<Applicant>> Applicants([Service] IMediator mediator, CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetApplicantsQuery(), cancellationToken);
    }

    public async Task<IEnumerable<Application>> ApplicationsByPosting(int jobPostingId, [Service] IMediator mediator, CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetApplicationsByPostingQuery(jobPostingId), cancellationToken);
    }
}
