package com.gallery.artworkservice.domain;

import java.util.Objects;

public class ArtworkID {
    private int id;

    public ArtworkID() {
        this.id = 0;
    }

    public ArtworkID(int id) {
        this.id = id;
    }

    public ArtworkID(ArtworkID other) {
        this.id = other.id;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (!(o instanceof ArtworkID)) return false;
        ArtworkID that = (ArtworkID) o;
        return id == that.id;
    }

    @Override
    public int hashCode() {
        return Objects.hash(id);
    }
}
