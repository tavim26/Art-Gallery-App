﻿using ArtworkService.Domain;
using ArtworkService.Domain.Contracts;
using ArtworkService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArtworkService.Infrastructure
{
    public class ArtworkDAO : DbContext, IArtworkDAO
    {
        private DbSet<ArtworkEntity> Artworks => Set<ArtworkEntity>();

        public ArtworkDAO(DbContextOptions<ArtworkDAO> options) : base(options) { }

        public List<Artwork> ListArtworks()
        {
            try
            {
                return Artworks.Select(a => a.ToArtwork()).ToList();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"[InsertArtwork] Error listing artwork: {ex.Message}");

                return new List<Artwork>();
            }
        }


        public Artwork? GetArtworkById(int id)
        {
            try
            {
                var entity = Artworks.Find(id);
                return entity?.ToArtwork();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[InsertArtwork] Error geting by id artwork: {ex.Message}");
                return null;
            }
        }

        public bool InsertArtwork(Artwork artwork)
        {
            if (artwork == null || string.IsNullOrWhiteSpace(artwork.Title) || string.IsNullOrWhiteSpace(artwork.Type))
                return false;

            try
            {
                Artworks.Add(new ArtworkEntity(artwork));
                return SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[InsertArtwork] Error inserting artwork: {ex.Message}");
                return false;
            }
        }

        public bool UpdateArtwork(Artwork artwork)
        {
            if (artwork == null || string.IsNullOrWhiteSpace(artwork.Title) || string.IsNullOrWhiteSpace(artwork.Type))
                return false;

            try
            {
                var existing = Artworks.Find(artwork.Id);
                if (existing == null) return false;

                existing.Title = artwork.Title;
                existing.YearCreated = artwork.YearCreated;
                existing.Type = artwork.Type;
                existing.ArtistId = artwork.ArtistId;
                existing.Price = artwork.Price;

                return SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[InsertArtwork] Error updating artwork: {ex.Message}");
                return false;
            }
        }

        public bool DeleteArtwork(int id)
        {
            try
            {
                var entity = Artworks.Find(id);
                if (entity == null) return false;

                Artworks.Remove(entity);
                return SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[InsertArtwork] Error deleting artwork: {ex.Message}");
                return false;
            }
        }

        public List<Artwork> SearchByTitle(string title)
        {
            try
            {
                return Artworks
                    .Where(a => a.Title.Contains(title))
                    .OrderBy(a => a.YearCreated)
                    .Select(a => a.ToArtwork())
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[InsertArtwork] Error searching artwork: {ex.Message}");
                return new List<Artwork>();
            }
        }

        public List<Artwork> FilterByType(string type)
        {
            try
            {
                return Artworks
                    .Where(a => a.Type.Equals(type))
                    .OrderBy(a => a.YearCreated)
                    .Select(a => a.ToArtwork())
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[InsertArtwork] Error filtering artwork: {ex.Message}");
                return new List<Artwork>();
            }
        }

        public List<Artwork> FilterByArtistId(int artistId)
        {
            try
            {
                return Artworks
                    .Where(a => a.ArtistId == artistId)
                    .OrderBy(a => a.YearCreated)
                    .Select(a => a.ToArtwork())
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[InsertArtwork] Error filtering artwork: {ex.Message}");
                return new List<Artwork>();
            }
        }

        public List<Artwork> FilterByMaxPrice(double maxPrice)
        {
            try
            {
                return Artworks
                    .Where(a => a.Price <= maxPrice)
                    .OrderBy(a => a.Price)
                    .Select(a => a.ToArtwork())
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[InsertArtwork] Error filtering artwork: {ex.Message}");
                return new List<Artwork>();
            }
        }

   
    }
}
