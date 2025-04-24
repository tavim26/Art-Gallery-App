package com.gallery.artistservice.service;

import com.gallery.artistservice.domain.Artist;
import com.gallery.artistservice.domain.IArtistRepository;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class ArtistService {

    private final IArtistRepository artistRepository;

    public ArtistService(IArtistRepository artistRepository) {
        this.artistRepository = artistRepository;
    }

    public List<Artist> getAllArtists() {
        return artistRepository.findAll();
    }

    public Artist getArtistById(int id) {
        return artistRepository.findById(id);
    }

    public List<Artist> searchArtistsByName(String name) {
        return artistRepository.searchByName(name);
    }

    public boolean createArtist(Artist artist) {
        if (artist.getName() == null || artist.getName().isEmpty()) return false;
        return artistRepository.insert(artist);
    }

    public boolean updateArtist(Artist artist) {
        if (artist.getId() == null || artist.getId().getId() <= 0) return false;
        return artistRepository.update(artist);
    }

    public boolean deleteArtist(int id) {
        return artistRepository.delete(id);
    }
}
