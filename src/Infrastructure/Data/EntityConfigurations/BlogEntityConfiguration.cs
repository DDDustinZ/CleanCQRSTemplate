using COMPANY_NAME.PRODUCT.Core.Blogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace COMPANY_NAME.PRODUCT.Infrastructure.Data.EntityConfigurations;

public class BlogEntityConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder.ToTable("Blog");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnType("bigint").IsRequired();
        builder.Property(x => x.Name).HasColumnType("nvarchar").HasMaxLength(Blog.NameMaxLength).IsRequired();
        builder.OwnsOne<AuthorName>(x => x.AuthorName, navigationBuilder =>
        {
            navigationBuilder.Property(x => x.First).HasMaxLength(AuthorName.MaxLength).IsRequired();
            navigationBuilder.Property(x => x.Last).HasMaxLength(AuthorName.MaxLength).IsRequired();
        });
    }
}