package com.gallery.artistservice.controller;

import com.gallery.artistservice.domain.Artist;
import com.gallery.artistservice.service.ArtistService;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/artists")
public class ArtistController {

    private final ArtistService artistService;

    public ArtistController(ArtistService artistService) {
        this.artistService = artistService;
    }

    @GetMapping
    public ResponseEntity<List<Artist>> getAllArtists() {
        return ResponseEntity.ok(artistService.getAllArtists());
    }

    @GetMapping("/{id}")
    public ResponseEntity<Artist> getArtistById(@PathVariable int id) {
        Artist artist = artistService.getArtistById(id);
        return artist != null ? ResponseEntity.ok(artist) : ResponseEntity.notFound().build();
    }

    @GetMapping("/search")
    public ResponseEntity<List<Artist>> searchByName(@RequestParam String name) {
        return ResponseEntity.ok(artistService.searchArtistsByName(name));
    }

    @PostMapping
    public ResponseEntity<Void> createArtist(@RequestBody Artist artist) {
        boolean created = artistService.createArtist(artist);
        return created ? ResponseEntity.ok().build() : ResponseEntity.badRequest().build();
    }

    @PutMapping
    public ResponseEntity<Void> updateArtist(@RequestBody Artist artist) {
        boolean updated = artistService.updateArtist(artist);
        return updated ? ResponseEntity.ok().build() : ResponseEntity.badRequest().build();
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deleteArtist(@PathVariable int id) {
        boolean deleted = artistService.deleteArtist(id);
        return deleted ? ResponseEntity.ok().build() : ResponseEntity.notFound().build();
    }
}
