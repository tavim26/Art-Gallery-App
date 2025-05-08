namespace SaleService.Domain.Contracts
{
    public interface IExportStrategy
    {
        byte[] Export(Sale sale);
        string GetFileExtension(); 
        string GetMimeType();     
    }
}