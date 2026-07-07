using HotChocolate;

namespace JobBoard.API.GraphQL;

public class InvalidOperationExceptionFilter : IErrorFilter
{
    public IError OnError(IError error)
    {
        if (error.Exception is InvalidOperationException exception)
        {
            return error.WithMessage(exception.Message);
        }

        return error;
    }
}
