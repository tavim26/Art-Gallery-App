package com.gallery.artworkservice.domain;

public class ArtworkImage {
    private int id;
    private int artworkId;
    private String imageUrl;

    public ArtworkImage() {}

    public ArtworkImage(int id, int artworkId, String imageUrl) {
        this.id = id;
        this.artworkId = artworkId;
        this.imageUrl = imageUrl;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public int getArtworkId() {
        return artworkId;
    }

    public void setArtworkId(int artworkId) {
        this.artworkId = artworkId;
    }

    public String getImageUrl() {
        return imageUrl;
    }

    public void setImageUrl(String imageUrl) {
        this.imageUrl = imageUrl;
    }
}
