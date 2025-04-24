package com.gallery.artworkservice.controller;

import com.gallery.artworkservice.domain.Artwork;
import com.gallery.artworkservice.service.ArtworkService;
import com.gallery.artworkservice.service.export.ArtworkExporter;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/artworks")
public class ArtworkController {

    private final ArtworkService artworkService;

    @Autowired
    private ArtworkExporter exporter;

    public ArtworkController(ArtworkService artworkService) {
        this.artworkService = artworkService;
    }

    @GetMapping
    public ResponseEntity<List<Artwork>> getAllArtworks() {
        return ResponseEntity.ok(artworkService.getAllArtworks());
    }

    @GetMapping("/{id}")
    public ResponseEntity<Artwork> getArtworkById(@PathVariable int id) {
        Artwork artwork = artworkService.getArtworkById(id);
        if (artwork == null) return ResponseEntity.notFound().build();
        return ResponseEntity.ok(artwork);
    }

    @GetMapping("/search")
    public ResponseEntity<List<Artwork>> searchArtworks(@RequestParam String title) {
        return ResponseEntity.ok(artworkService.searchArtworksByTitle(title));
    }

    @GetMapping("/artist/{artistId}")
    public ResponseEntity<List<Artwork>> filterByArtist(@PathVariable int artistId) {
        return ResponseEntity.ok(artworkService.filterByArtist(artistId));
    }

    @GetMapping("/type/{type}")
    public ResponseEntity<List<Artwork>> filterByType(@PathVariable String type) {
        return ResponseEntity.ok(artworkService.filterByType(type));
    }

    @PostMapping
    public ResponseEntity<Void> createArtwork(@RequestBody Artwork artwork) {
        boolean created = artworkService.createArtwork(artwork);
        return created ? ResponseEntity.ok().build() : ResponseEntity.badRequest().build();
    }

    @PutMapping
    public ResponseEntity<Void> updateArtwork(@RequestBody Artwork artwork) {
        boolean updated = artworkService.updateArtwork(artwork);
        return updated ? ResponseEntity.ok().build() : ResponseEntity.badRequest().build();
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deleteArtwork(@PathVariable int id) {
        boolean deleted = artworkService.deleteArtwork(id);
        return deleted ? ResponseEntity.ok().build() : ResponseEntity.notFound().build();
    }


    //exports

    @GetMapping("/export/csv")
    public ResponseEntity<String> exportCsv() {
        List<Artwork> artworks = artworkService.getAllArtworks();
        return ResponseEntity.ok()
                .header("Content-Disposition", "attachment; filename=artworks.csv")
                .header("Content-Type", "text/csv")
                .body(exporter.exportToCsv(artworks));
    }

    @GetMapping("/export/json")
    public ResponseEntity<String> exportJson() {
        List<Artwork> artworks = artworkService.getAllArtworks();
        return ResponseEntity.ok()
                .header("Content-Disposition", "attachment; filename=artworks.json")
                .header("Content-Type", "application/json")
                .body(exporter.exportToJson(artworks));
    }

    @GetMapping("/export/doc")
    public ResponseEntity<byte[]> exportDoc() {
        List<Artwork> artworks = artworkService.getAllArtworks();
        return ResponseEntity.ok()
                .header("Content-Disposition", "attachment; filename=artworks.docx")
                .header("Content-Type", "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
                .body(exporter.exportToDoc(artworks));
    }
}
