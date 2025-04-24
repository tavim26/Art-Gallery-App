package com.gallery.artistservice.domain;

import java.util.List;

public interface IArtistRepository
{
    List<Artist> findAll();
    Artist findById(int id);
    List<Artist> searchByName(String name);
    boolean insert(Artist artist);
    boolean update(Artist artist);
    boolean delete(int id);
}
