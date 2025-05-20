using ArtworkService.Domain.DTO;

namespace ArtworkService.Domain.Contracts
{
    public interface IExportStrategy
    {
        string Export(List<ArtworkDTO> artworks);
        string ContentType { get; }
        string FileExtension { get; }
    }
}
