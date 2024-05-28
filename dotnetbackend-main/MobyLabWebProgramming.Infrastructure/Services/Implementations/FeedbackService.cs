using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class FeedbackService : IFeedbackService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;
    public FeedbackService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse> AddFeedback(FeedbackAddDTO feedback, CancellationToken cancellationToken = default)
    {

        var result = await _repository.GetAsync(new FeedbackSpec(feedback.Q1), cancellationToken);

        //if (result != null)
        //{
        //    return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The feedback already exists!", ErrorCodes.AuthorAlreadyExists));
        //}

        await _repository.AddAsync(new Feedback
        {
            Q1 = feedback.Q1,
            Q2 = feedback.Q2,
            Q3 = feedback.Q3,
            Q4 = feedback.Q4
        }, cancellationToken); // A new entity is created and persisted in the database.

        return ServiceResponse.ForSuccess();
    }

}
