package com.gallery.saleservice.service;

import com.gallery.saleservice.domain.ISaleRepository;
import com.gallery.saleservice.domain.Sale;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class SaleService {

    private final ISaleRepository saleRepository;

    public SaleService(ISaleRepository saleRepository) {
        this.saleRepository = saleRepository;
    }

    public List<Sale> getAllSales() {
        return saleRepository.findAll();
    }

    public Sale getSaleById(int id) {
        return saleRepository.findById(id);
    }

    public List<Sale> getSalesByEmployee(int employeeId) {
        return saleRepository.findByEmployee(employeeId);
    }

    public boolean recordSale(Sale sale) {
        if (sale.getArtworkId() <= 0 || sale.getEmployeeId() <= 0 || sale.getPrice().doubleValue() <= 0)
            return false;
        return saleRepository.insert(sale);
    }

    public boolean deleteSale(int id) {
        return saleRepository.delete(id);
    }
}
