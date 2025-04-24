package com.gallery.artworkservice.domain;

import java.util.List;

public interface IArtworkRepository
{
    List<Artwork> findAll();
    List<Artwork> filterByArtist(int artistId);
    List<Artwork> filterByType(String type);
    List<Artwork> searchByTitle(String title);
    Artwork findById(int id);
    boolean insert(Artwork artwork);
    boolean update(Artwork artwork);
    boolean delete(int id);
}
