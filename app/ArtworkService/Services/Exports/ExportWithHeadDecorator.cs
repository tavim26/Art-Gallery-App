using ArtworkService.Domain.Contracts;
using ArtworkService.Domain.DTO;

namespace ArtworkService.Services.Exports
{
    public class ExportWithHeaderDecorator : IExportStrategy
    {
        private readonly IExportStrategy _innerStrategy;

        public ExportWithHeaderDecorator(IExportStrategy innerStrategy)
        {
            _innerStrategy = innerStrategy;
        }

        public string Export(List<ArtworkDTO> artworks)
        {
            var originalExport = _innerStrategy.Export(artworks);
            var header = $"# Export generated at {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n";
            return header + originalExport;
        }

        public string ContentType => _innerStrategy.ContentType;
        public string FileExtension => _innerStrategy.FileExtension;
    }
}
