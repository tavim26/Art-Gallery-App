package com.gallery.artworkservice.infrastructure;

import jakarta.persistence.*;
import java.util.ArrayList;
import java.util.List;

@Entity
@Table(name = "ARTWORK")
public class ArtworkEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;

    @Column(name = "Title",nullable = false)
    private String title;

    @Column(name = "YearCreated")
    private int yearCreated;

    @Column(name = "Type", nullable = false)
    private String type;

    @Column(name = "ArtistId")
    private int artistId;

    @OneToMany(mappedBy = "artwork", cascade = CascadeType.ALL, orphanRemoval = true)
    private List<ArtworkImageEntity> images = new ArrayList<>();

    public ArtworkEntity() {}

    public ArtworkEntity(String title, int yearCreated, String type, int artistId) {
        this.title = title;
        this.yearCreated = yearCreated;
        this.type = type;
        this.artistId = artistId;
    }

    // Getters and setters
    public int getId() { return id; }
    public void setId(int id) { this.id = id; }

    public String getTitle() { return title; }
    public void setTitle(String title) { this.title = title; }

    public int getYearCreated() { return yearCreated; }
    public void setYearCreated(int yearCreated) { this.yearCreated = yearCreated; }

    public String getType() { return type; }
    public void setType(String type) { this.type = type; }

    public int getArtistId() { return artistId; }
    public void setArtistId(int artistId) { this.artistId = artistId; }

    public List<ArtworkImageEntity> getImages() { return images; }
    public void setImages(List<ArtworkImageEntity> images) { this.images = images; }
}
