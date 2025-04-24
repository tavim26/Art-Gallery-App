package com.gallery.artworkservice.domain;

import java.util.List;

public class Artwork {
    private ArtworkID id;
    private String title;
    private int yearCreated;
    private String type;
    private int artistId;
    private List<ArtworkImage> images;




    public Artwork() {
        this.id = new ArtworkID();
        this.title = "";
        this.yearCreated = 0;
        this.type = "";
        this.artistId = 0;
    }

    public Artwork(ArtworkID id, String title, int yearCreated, String type, int artistId, List<ArtworkImage> images) {
        this.id = id;
        this.title = title;
        this.yearCreated = yearCreated;
        this.type = type;
        this.artistId = artistId;
        this.images = images;
    }


    public ArtworkID getId() {
        return id;
    }

    public void setId(ArtworkID id) {
        this.id = id;
    }

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public int getYearCreated() {
        return yearCreated;
    }

    public void setYearCreated(int yearCreated) {
        this.yearCreated = yearCreated;
    }

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }

    public int getArtistId() {
        return artistId;
    }

    public void setArtistId(int artistId) {
        this.artistId = artistId;
    }

    public List<ArtworkImage> getImages() {
        return images;
    }

    public void setImages(List<ArtworkImage> images) {
        this.images = images;
    }
}
