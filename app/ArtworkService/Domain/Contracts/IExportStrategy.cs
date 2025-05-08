using ArtworkService.Domain.DTO;

namespace ArtworkService.Services.Exports
{
    public interface IExportStrategy
    {
        string Export(List<ArtworkDTO> artworks);
        string ContentType { get; }
        string FileExtension { get; }
    }
}
