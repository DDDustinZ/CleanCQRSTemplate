using System.Net;
using COMPANY_NAME.PRODUCT.Core.Exceptions;
using FastEndpoints;

namespace COMPANY_NAME.PRODUCT.Web.EndpointProcessors;

public class RecordNotFoundPostProcessor : IGlobalPostProcessor
{
    public async Task PostProcessAsync(IPostProcessorContext context, CancellationToken ct)
    {
        if (!context.HasExceptionOccurred || context.ExceptionDispatchInfo.SourceException.GetType() != typeof(RecordNotFoundException))
        {
            return;
        }

        context.MarkExceptionAsHandled();

        var exception = (RecordNotFoundException) context.ExceptionDispatchInfo.SourceException;
        var response = new ErrorResponse
        {
            Message = exception.Message,
            StatusCode = (int) HttpStatusCode.NotFound
        };
        
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
        context.HttpContext.Response.ContentType = "application/problem+json";
        await context.HttpContext.Response.WriteAsJsonAsync(response, ct);
    }
}