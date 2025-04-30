using ArtworkService.Domain;
using ArtworkService.Domain.Contracts;
using ArtworkService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ArtworkService.Infrastructure
{
    public class ArtworkDAO : DbContext, IArtworkDAO
    {
        private DbSet<ArtworkEntity> _artworksSet { get; set; }
        private DbSet<ArtworkImageEntity> _artworkImagesSet { get; set; }

        public ArtworkDAO(DbContextOptions<ArtworkDAO> options)
            : base(options) { }

        public List<Artwork> Artworks()
        {
            try
            {
                List<Artwork> artworks = new List<Artwork>();
                if (_artworksSet != null)
                    foreach (var artworkEntity in _artworksSet)
                        artworks.Add(artworkEntity.ToArtwork());
                return artworks;
            }
            catch
            {
                return new List<Artwork>();
            }
        }

        public Artwork? GetArtworkById(int id)
        {
            try
            {
                ArtworkEntity artworkEntity = _artworksSet.First(a => a.Id == id);
                if (artworkEntity != null)
                    return artworkEntity.ToArtwork();
                return null;
            }
            catch
            {
                return null;
            }
        }

        public List<ArtworkImage> GetArtworkImages(int artworkId)
        {
            try
            {
                List<ArtworkImage> images = new List<ArtworkImage>();
                if (_artworkImagesSet != null)
                {
                    var query = _artworkImagesSet.Where(img => img.ArtworkId == artworkId);
                    foreach (var imgEntity in query)
                        images.Add(imgEntity.ToArtworkImage());
                }
                return images;
            }
            catch
            {
                return new List<ArtworkImage>();
            }
        }

        public bool InsertArtwork(Artwork artwork)
        {
            if (artwork.Title == null || artwork.Type == null)
                return false;
            try
            {
                _artworksSet.Add(new ArtworkEntity(artwork));
                if (this.SaveChanges() > 0)
                    return true;
            }
            catch
            {
                return false;
            }
            return false;
        }

        public bool UpdateArtwork(Artwork artwork)
        {
            if (artwork.Title == null || artwork.Type == null)
                return false;
            try
            {
                _artworksSet.Update(new ArtworkEntity(artwork));
                if (this.SaveChanges() > 0)
                    return true;
            }
            catch
            {
                return false;
            }
            return false;
        }

        public bool DeleteArtwork(int id)
        {
            try
            {
                var artwork = _artworksSet.FirstOrDefault(a => a.Id == id);
                if (artwork != null)
                {
                    _artworksSet.Remove(artwork);
                    return this.SaveChanges() > 0;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }


        public List<Artwork> SearchByTitle(string title)
        {
            try
            {
                List<Artwork> artworks = new List<Artwork>();
                if (_artworksSet != null)
                {
                    var query = _artworksSet
                        .Where(a => a.Title.Contains(title))
                        .OrderBy(a => a.YearCreated);

                    foreach (var artworkEntity in query)
                        artworks.Add(artworkEntity.ToArtwork());
                }
                return artworks;
            }
            catch
            {
                return new List<Artwork>();
            }
        }

        public List<Artwork> FilterByType(string type)
        {
            try
            {
                List<Artwork> artworks = new List<Artwork>();
                if (_artworksSet != null)
                {
                    var query = _artworksSet
                        .Where(a => a.Type.Equals(type))
                        .OrderBy(a => a.YearCreated);

                    foreach (var artworkEntity in query)
                        artworks.Add(artworkEntity.ToArtwork());
                }
                return artworks;
            }
            catch
            {
                return new List<Artwork>();
            }
        }


        public List<Artwork> FilterByArtistId(int artistId)
        {
            try
            {
                List<Artwork> artworks = new List<Artwork>();
                if (_artworksSet != null)
                {
                    var query = _artworksSet
                        .Where(a => a.ArtistId == artistId)
                        .OrderBy(a => a.YearCreated);

                    foreach (var artworkEntity in query)
                        artworks.Add(artworkEntity.ToArtwork());
                }
                return artworks;
            }
            catch
            {
                return new List<Artwork>();
            }
        }




        public List<Artwork> FilterByMaxPrice(double maxPrice)
        {
            try
            {
                List<Artwork> artworks = new List<Artwork>();
                if (_artworksSet != null)
                {
                    var query = _artworksSet
                        .Where(a => a.Price <= maxPrice)
                        .OrderBy(a => a.Price);

                    foreach (var artworkEntity in query)
                        artworks.Add(artworkEntity.ToArtwork());
                }
                return artworks;
            }
            catch
            {
                return new List<Artwork>();
            }
        }

    }



}
