using COMPANY_NAME.PRODUCT.Core.Abstracts;

namespace COMPANY_NAME.PRODUCT.Core.Blogs;

public class Blog : IEntity<int>
{
    public const int NameMaxLength = 255;
    
    public static Blog NewBlog(string name, AuthorName authorName)
    {
        return new Blog
        {
            Name = name,
            AuthorName = authorName
        };
    }

    private Blog()
    {
    }
    
    public int Id { get; set; }
    public string Name { get; private set; } = null!;
    public AuthorName AuthorName { get; private set; } = null!;
}