package com.gallery.saleservice.domain;

import java.util.List;

public interface ISaleRepository {
    List<Sale> findAll();
    Sale findById(int id);
    List<Sale> findByEmployee(int employeeId);
    boolean insert(Sale sale);
    boolean delete(int id);
}
