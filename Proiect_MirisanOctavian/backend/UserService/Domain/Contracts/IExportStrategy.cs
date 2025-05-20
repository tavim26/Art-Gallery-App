namespace UserService.Domain.Contracts
{
    public interface IExportStrategy<T>
    {
        string Export(T data);
        string ContentType { get; }
        string FileExtension { get; }
    }

}
