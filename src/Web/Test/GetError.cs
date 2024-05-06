using FastEndpoints;

namespace COMPANY_NAME.PRODUCT.Web.Test;

public class GetError : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("/test/error");
        AllowAnonymous();
    }

    public override Task HandleAsync(CancellationToken ct)
    {
        throw new Exception("Test Exception");
    }
}