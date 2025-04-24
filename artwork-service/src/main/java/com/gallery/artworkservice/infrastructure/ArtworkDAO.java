package com.gallery.artworkservice.infrastructure;

import com.gallery.artworkservice.domain.*;

import jakarta.persistence.EntityManager;
import jakarta.persistence.PersistenceContext;
import jakarta.transaction.Transactional;
import org.springframework.stereotype.Repository;

import java.util.ArrayList;
import java.util.List;

@Repository
@Transactional
public class ArtworkDAO implements IArtworkRepository {

    @PersistenceContext
    private EntityManager entityManager;

    @Override
    public List<Artwork> findAll() {
        List<ArtworkEntity> entities = entityManager.createQuery("SELECT a FROM ArtworkEntity a", ArtworkEntity.class).getResultList();
        return mapToDomainList(entities);
    }

    @Override
    public List<Artwork> filterByArtist(int artistId) {
        List<ArtworkEntity> entities = entityManager
                .createQuery("SELECT a FROM ArtworkEntity a WHERE a.artistId = :artistId", ArtworkEntity.class)
                .setParameter("artistId", artistId)
                .getResultList();
        return mapToDomainList(entities);
    }

    @Override
    public List<Artwork> filterByType(String type) {
        List<ArtworkEntity> entities = entityManager
                .createQuery("SELECT a FROM ArtworkEntity a WHERE a.type = :type", ArtworkEntity.class)
                .setParameter("type", type)
                .getResultList();
        return mapToDomainList(entities);
    }

    @Override
    public List<Artwork> searchByTitle(String title) {
        List<ArtworkEntity> entities = entityManager
                .createQuery("SELECT a FROM ArtworkEntity a WHERE LOWER(a.title) LIKE LOWER(:title)", ArtworkEntity.class)
                .setParameter("title", "%" + title + "%")
                .getResultList();
        return mapToDomainList(entities);
    }

    @Override
    public Artwork findById(int id) {
        ArtworkEntity entity = entityManager.find(ArtworkEntity.class, id);
        if (entity == null) return null;
        return mapToDomain(entity);
    }

    @Override
    public boolean insert(Artwork artwork) {
        try {
            ArtworkEntity entity = mapToEntity(artwork);
            entityManager.persist(entity);
            return true;
        } catch (Exception e) {
            return false;
        }
    }

    @Override
    public boolean update(Artwork artwork) {
        try {
            ArtworkEntity entity = mapToEntity(artwork);
            entityManager.merge(entity);
            return true;
        } catch (Exception e) {
            return false;
        }
    }

    @Override
    public boolean delete(int id) {
        try {
            ArtworkEntity entity = entityManager.find(ArtworkEntity.class, id);
            if (entity != null) {
                entityManager.remove(entity);
                return true;
            }
            return false;
        } catch (Exception e) {
            return false;
        }
    }

    // === Mapping Helpers ===

    private Artwork mapToDomain(ArtworkEntity entity) {
        List<ArtworkImage> images = new ArrayList<>();
        for (ArtworkImageEntity imgEntity : entity.getImages()) {
            images.add(new ArtworkImage(
                    imgEntity.getId(),
                    entity.getId(),
                    imgEntity.getImageUrl()
            ));
        }
        return new Artwork(
                new ArtworkID(entity.getId()),
                entity.getTitle(),
                entity.getYearCreated(),
                entity.getType(),
                entity.getArtistId(),
                images
        );
    }

    private List<Artwork> mapToDomainList(List<ArtworkEntity> entities) {
        List<Artwork> result = new ArrayList<>();
        for (ArtworkEntity entity : entities) {
            result.add(mapToDomain(entity));
        }
        return result;
    }

    private ArtworkEntity mapToEntity(Artwork artwork) {
        ArtworkEntity entity = new ArtworkEntity(
                artwork.getTitle(),
                artwork.getYearCreated(),
                artwork.getType(),
                artwork.getArtistId()
        );
        for (ArtworkImage img : artwork.getImages()) {
            ArtworkImageEntity imgEntity = new ArtworkImageEntity(img.getImageUrl(), entity);
            entity.getImages().add(imgEntity);
        }
        return entity;
    }
}
