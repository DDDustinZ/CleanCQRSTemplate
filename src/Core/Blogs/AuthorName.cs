namespace COMPANY_NAME.PRODUCT.Core.Blogs;

public record AuthorName(string First, string Last)
{
    public const int MinLength = 2;
    public const int MaxLength = 255;
};