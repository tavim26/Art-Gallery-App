using ArtworkService.Domain.Contracts;
using ArtworkService.Domain.DTO;

namespace ArtworkService.Services.Exports
{
    public class CsvExportStrategy : IExportStrategy
    {
        public string Export(List<ArtworkDTO> artworks)
        {
            var lines = new List<string> { "Id,Title,YearCreated,Type,ArtistId,Price" };
            lines.AddRange(artworks.Select(a =>
                $"{a.Id},{a.Title},{a.YearCreated},{a.Type},{a.ArtistId},{a.Price.ToString("F2")}"));
            return string.Join("\n", lines);
        }

        public string ContentType => "text/csv";
        public string FileExtension => ".csv";
    }

}
