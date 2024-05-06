using System.Reflection;
using FastEndpoints;

namespace COMPANY_NAME.PRODUCT.Web.Root;

public class GetVersion : EndpointWithoutRequest<GetVersion.Response>
{
    public new record Response(string? Version);

    public override void Configure()
    {
        Get("/version");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = new Response(Assembly.GetExecutingAssembly().GetName().Version?.ToString());
        await SendOkAsync(response, ct);
    }
}