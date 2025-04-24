package com.gallery.artworkservice.infrastructure;

import jakarta.persistence.*;

@Entity
@Table(name = "ARTWORK_IMAGE")
public class ArtworkImageEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;

    @Column(name = "ImageUrl", nullable = false)
    private String imageUrl;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "ArtworkId", nullable = false)
    private ArtworkEntity artwork;

    public ArtworkImageEntity() {}

    public ArtworkImageEntity(String imageUrl, ArtworkEntity artwork) {
        this.imageUrl = imageUrl;
        this.artwork = artwork;
    }

    // Getters and setters
    public int getId() { return id; }
    public void setId(int id) { this.id = id; }

    public String getImageUrl() { return imageUrl; }
    public void setImageUrl(String imageUrl) { this.imageUrl = imageUrl; }

    public ArtworkEntity getArtwork() { return artwork; }
    public void setArtwork(ArtworkEntity artwork) { this.artwork = artwork; }
}
