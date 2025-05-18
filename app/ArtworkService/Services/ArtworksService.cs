using ArtworkService.Domain;
using ArtworkService.Domain.Contracts;
using ArtworkService.Domain.DTO;
using ArtworkService.Services.Exports;

namespace ArtworkService.Services
{
    public class ArtworksService
    {
        private readonly IArtworkDAO _artworkDAO;

        public ArtworksService(IArtworkDAO artworkDAO)
        {
            _artworkDAO = artworkDAO;
        }

        public List<Artwork> GetArtworks()
        {
            return _artworkDAO.ListArtworks();
        }

        public Artwork? GetArtwork(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return _artworkDAO.GetArtworkById(id);
        }

        public bool InsertArtwork(Artwork artwork)
        {
            if (artwork == null)
            {
                return false;
            }

            return _artworkDAO.InsertArtwork(artwork);
        }

        public bool UpdateArtwork(Artwork artwork)
        {
            if (artwork == null)
            {
                return false;
            }

            return _artworkDAO.UpdateArtwork(artwork);
        }

        public bool DeleteArtwork(int id)
        {
            if (id <= 0)
            {
                return false;
            }

            return _artworkDAO.DeleteArtwork(id);
        }

        public List<Artwork> SearchByTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return new List<Artwork>();
            }

            return _artworkDAO.SearchByTitle(title);
        }

        public List<Artwork> FilterByType(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                return new List<Artwork>();
            }

            return _artworkDAO.FilterByType(type);
        }

        public List<Artwork> FilterByArtistId(int artistId)
        {
            if (artistId <= 0)
            {
                return new List<Artwork>();
            }

            return _artworkDAO.FilterByArtistId(artistId);
        }

        public List<Artwork> FilterByMaxPrice(double maxPrice)
        {
            if (maxPrice < 0)
            {
                return new List<Artwork>();
            }

            return _artworkDAO.FilterByMaxPrice(maxPrice);
        }


        public (byte[] content, string contentType, string fileName)? ExportArtworks(string format)
        {
            var artworks = GetArtworks()
                .Select(art => new ArtworkDTO
                {
                    Id = art.Id,
                    Title = art.Title,
                    YearCreated = art.YearCreated,
                    Type = art.Type,
                    ArtistId = art.ArtistId,
                    Price = art.Price
                })
                .ToList();

            IExportStrategy? strategy = format?.ToLower() switch
            {
                "csv" => new CsvExportStrategy(),
                "json" => new JsonExportStrategy(),
                "xml" => new XmlExportStrategy(),
                _ => null
            };

            if (strategy == null)
                return null;

            var result = strategy.Export(artworks);
            var bytes = System.Text.Encoding.UTF8.GetBytes(result);
            var fileName = $"artworks_{DateTime.Now:yyyyMMdd_HHmmss}{strategy.FileExtension}";

            return (bytes, strategy.ContentType, fileName);

        }





        public List<ArtworkStatsDTO> GetStatsByType()
        {
            return _artworkDAO.ListArtworks()
                .GroupBy(a => a.Type)
                .Select(g => new ArtworkStatsDTO
                {
                    Label = g.Key,
                    Count = g.Count()
                })
                .ToList();
        }

        public List<ArtworkStatsDTO> GetStatsByArtist()
        {
            return _artworkDAO.ListArtworks()
                .GroupBy(a => a.ArtistId.ToString()) 
                .Select(g => new ArtworkStatsDTO
                {
                    Label = $"Artist {g.Key}",
                    Count = g.Count()
                })
                .ToList();
        }


}

}