package com.gallery.userservice.infrastructure;

import com.gallery.userservice.domain.IUserRepository;
import com.gallery.userservice.domain.User;
import jakarta.persistence.EntityManager;
import jakarta.persistence.PersistenceContext;
import jakarta.transaction.Transactional;
import org.springframework.stereotype.Repository;

import java.util.ArrayList;
import java.util.List;

@Repository
@Transactional
public class UserDAO implements IUserRepository {

    @PersistenceContext
    private EntityManager entityManager;

    @Override
    public List<User> findAll() {
        List<UserEntity> entities = entityManager.createQuery("FROM UserEntity", UserEntity.class).getResultList();
        List<User> results = new ArrayList<>();
        for (UserEntity entity : entities) {
            results.add(entity.toDomain());
        }
        return results;
    }

    @Override
    public User findById(int id) {
        UserEntity entity = entityManager.find(UserEntity.class, id);
        return entity != null ? entity.toDomain() : null;
    }

    @Override
    public boolean insert(User user) {
        try {
            entityManager.persist(new UserEntity(user));
            return true;
        } catch (Exception e) {
            return false;
        }
    }

    @Override
    public boolean update(User user) {
        try {
            entityManager.merge(new UserEntity(user));
            return true;
        } catch (Exception e) {
            return false;
        }
    }

    @Override
    public boolean delete(int id) {
        try {
            UserEntity entity = entityManager.find(UserEntity.class, id);
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
