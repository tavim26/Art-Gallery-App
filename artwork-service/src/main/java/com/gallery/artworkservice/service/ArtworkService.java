package com.gallery.artworkservice.service;

import com.gallery.artworkservice.domain.Artwork;
import com.gallery.artworkservice.domain.IArtworkRepository;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class ArtworkService {

    private final IArtworkRepository artworkRepository;

    public ArtworkService(IArtworkRepository artworkRepository) {
        this.artworkRepository = artworkRepository;
    }

    public List<Artwork> getAllArtworks() {
        return artworkRepository.findAll();
    }

    public Artwork getArtworkById(int id) {
        return artworkRepository.findById(id);
    }

    public List<Artwork> searchArtworksByTitle(String title) {
        return artworkRepository.searchByTitle(title);
    }

    public List<Artwork> filterByArtist(int artistId) {
        return artworkRepository.filterByArtist(artistId);
    }

    public List<Artwork> filterByType(String type) {
        return artworkRepository.filterByType(type);
    }

    public boolean createArtwork(Artwork artwork) {
        if (artwork.getTitle() == null || artwork.getTitle().isEmpty()) return false;
        if (artwork.getYearCreated() < 1000 || artwork.getYearCreated() > 2100) return false;
        if (artwork.getImages() == null || artwork.getImages().isEmpty()) return false;
        return artworkRepository.insert(artwork);
    }

    public boolean updateArtwork(Artwork artwork) {
        if (artwork.getId() == null || artwork.getId().getId() <= 0) return false;
        return artworkRepository.update(artwork);
    }

    public boolean deleteArtwork(int id) {
        return artworkRepository.delete(id);
    }
}
