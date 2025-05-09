using ArtworkService.Domain.Contracts;
using ArtworkService.Domain.DTO;

namespace ArtworkService.Services.Exports
{
    public class JsonExportStrategy : IExportStrategy
    {
        public string Export(List<ArtworkDTO> artworks)
        {
            return System.Text.Json.JsonSerializer.Serialize(artworks);
        }

        public string ContentType => "application/json";
        public string FileExtension => ".json";
    }

}
