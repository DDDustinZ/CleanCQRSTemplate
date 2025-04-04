using COMPANY_NAME.PRODUCT.UseCases.Blogs;
using MassTransit;

namespace COMPANY_NAME.PRODUCT.Web.Blogs;

public class BlogUpdatedConsumer : IConsumer<BlogUpdated>
{
    public Task Consume(ConsumeContext<BlogUpdated> context)
    {
        Console.WriteLine($"Blog updated: {context.Message.Id}");
        return Task.CompletedTask;
    }
}