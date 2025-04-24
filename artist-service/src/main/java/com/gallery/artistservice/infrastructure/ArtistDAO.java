package com.gallery.artistservice.infrastructure;

import com.gallery.artistservice.domain.Artist;
import com.gallery.artistservice.domain.IArtistRepository;
import com.gallery.artistservice.infrastructure.entities.ArtistEntity;
import jakarta.persistence.EntityManager;
import jakarta.persistence.PersistenceContext;
import jakarta.transaction.Transactional;
import org.springframework.stereotype.Repository;

import java.util.ArrayList;
import java.util.List;

@Repository
@Transactional
public class ArtistDAO implements IArtistRepository {

    @PersistenceContext
    private EntityManager entityManager;

    @Override
    public List<Artist> findAll() {
        List<ArtistEntity> entities = entityManager.createQuery("FROM ArtistEntity", ArtistEntity.class).getResultList();
        List<Artist> results = new ArrayList<>();
        for (ArtistEntity entity : entities) {
            results.add(entity.toDomain());
        }
        return results;
    }

    @Override
    public Artist findById(int id) {
        ArtistEntity entity = entityManager.find(ArtistEntity.class, id);
        return entity != null ? entity.toDomain() : null;
    }

    @Override
    public List<Artist> searchByName(String name) {
        List<ArtistEntity> entities = entityManager
                .createQuery("FROM ArtistEntity WHERE LOWER(name) LIKE LOWER(:name)", ArtistEntity.class)
                .setParameter("name", "%" + name + "%")
                .getResultList();

        List<Artist> results = new ArrayList<>();
        for (ArtistEntity entity : entities) {
            results.add(entity.toDomain());
        }
        return results;
    }

    @Override
    public boolean insert(Artist artist) {
        try {
            entityManager.persist(new ArtistEntity(artist));
            return true;
        } catch (Exception e) {
            return false;
        }
    }

    @Override
    public boolean update(Artist artist) {
        try {
            entityManager.merge(new ArtistEntity(artist));
            return true;
        } catch (Exception e) {
            return false;
        }
    }

    @Override
    public boolean delete(int id) {
        try {
            ArtistEntity entity = entityManager.find(ArtistEntity.class, id);
            if (entity != null) {
                entityManager.remove(entity);
                return true;
            }
            return false;
        } catch (Exception e) {
            return false;
        }
    }
}
