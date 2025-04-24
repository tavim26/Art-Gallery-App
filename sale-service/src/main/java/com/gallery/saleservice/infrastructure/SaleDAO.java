package com.gallery.saleservice.infrastructure;

import com.gallery.saleservice.domain.ISaleRepository;
import com.gallery.saleservice.domain.Sale;
import jakarta.persistence.EntityManager;
import jakarta.persistence.PersistenceContext;
import jakarta.transaction.Transactional;
import org.springframework.stereotype.Repository;

import java.util.ArrayList;
import java.util.List;

@Repository
@Transactional
public class SaleDAO implements ISaleRepository {

    @PersistenceContext
    private EntityManager entityManager;

    @Override
    public List<Sale> findAll() {
        List<SaleEntity> entities = entityManager
                .createQuery("FROM SaleEntity", SaleEntity.class)
                .getResultList();

        List<Sale> results = new ArrayList<>();
        for (SaleEntity e : entities) {
            results.add(e.toDomain());
        }
        return results;
    }

    @Override
    public Sale findById(int id) {
        SaleEntity entity = entityManager.find(SaleEntity.class, id);
        return entity != null ? entity.toDomain() : null;
    }

    @Override
    public List<Sale> findByEmployee(int employeeId) {
        List<SaleEntity> entities = entityManager
                .createQuery("FROM SaleEntity WHERE employeeId = :empId", SaleEntity.class)
                .setParameter("empId", employeeId)
                .getResultList();

        List<Sale> results = new ArrayList<>();
        for (SaleEntity e : entities) {
            results.add(e.toDomain());
        }
        return results;
    }

    @Override
    public boolean insert(Sale sale) {
        try {
            entityManager.persist(new SaleEntity(sale));
            return true;
        } catch (Exception e) {
            return false;
        }
    }

    @Override
    public boolean delete(int id) {
        try {
            SaleEntity entity = entityManager.find(SaleEntity.class, id);
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
