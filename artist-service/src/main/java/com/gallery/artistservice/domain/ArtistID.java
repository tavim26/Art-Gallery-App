package com.gallery.artistservice.domain;

public class ArtistID {
    private int id;

    public ArtistID() {
        this.id = 0;
    }

    public ArtistID(int id) {
        this.id = id;
    }

    public ArtistID(ArtistID other) {
        this.id = other.id;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }
}
