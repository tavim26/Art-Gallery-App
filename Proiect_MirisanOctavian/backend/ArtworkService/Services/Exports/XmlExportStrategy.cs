using ArtworkService.Domain.Contracts;
using ArtworkService.Domain.DTO;
using System.Xml.Serialization;

namespace ArtworkService.Services.Exports
{
    public class XmlExportStrategy : IExportStrategy
    {
        public string Export(List<ArtworkDTO> artworks)
        {
            var serializer = new XmlSerializer(typeof(List<ArtworkDTO>));
            using var sw = new StringWriter();
            serializer.Serialize(sw, artworks);
            return sw.ToString();
        }

        public string ContentType => "application/xml";
        public string FileExtension => ".xml";
    }
}
