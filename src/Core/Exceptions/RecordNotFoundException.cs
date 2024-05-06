namespace COMPANY_NAME.PRODUCT.Core.Exceptions;

public class RecordNotFoundException : Exception
{
    public RecordNotFoundException(int recordId, string message) : base(message)
    {
        RecordId = recordId;
    }
    
    public int RecordId { get; }
}