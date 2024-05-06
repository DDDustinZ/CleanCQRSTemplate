namespace COMPANY_NAME.PRODUCT.Core.Abstracts;

public interface IEntity<TId>
{
    public TId Id { get; set; }
}