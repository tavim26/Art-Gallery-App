package com.gallery.userservice.infrastructure;

import com.gallery.userservice.domain.Auth;
import com.gallery.userservice.domain.IAuthRepository;
import jakarta.persistence.EntityManager;
import jakarta.persistence.NoResultException;
import jakarta.persistence.PersistenceContext;
import jakarta.transaction.Transactional;
import org.springframework.stereotype.Repository;

@Repository
@Transactional
public class AuthDAO implements IAuthRepository {

    @PersistenceContext
    private EntityManager entityManager;

    @Override
    public Auth findByEmail(String email) {
        try {
            AuthEntity entity = entityManager
                    .createQuery("FROM AuthEntity WHERE email = :email", AuthEntity.class)
                    .setParameter("email", email)
                    .getSingleResult();
            return entity.toDomain();
        } catch (NoResultException e) {
            return null;
        }
    }

    @Override
    public boolean insert(Auth auth) {
        try {
            entityManager.persist(new AuthEntity(auth));
            return true;
        } catch (Exception e) {
            return false;
        }
    }

    @Override
    public boolean deleteByUserId(int userId) {
        try {
            entityManager
                    .createQuery("DELETE FROM AuthEntity WHERE userId = :userId")
                    .setParameter("userId", userId)
                    .executeUpdate();
            return true;
        } catch (Exception e) {
            return false;
        }
    }
}
