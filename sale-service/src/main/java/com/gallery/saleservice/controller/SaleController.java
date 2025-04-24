package com.gallery.saleservice.controller;

import com.gallery.saleservice.domain.Sale;
import com.gallery.saleservice.service.SaleService;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/sales")
public class SaleController {

    private final SaleService saleService;

    public SaleController(SaleService saleService) {
        this.saleService = saleService;
    }

    @GetMapping
    public ResponseEntity<List<Sale>> getAllSales() {
        return ResponseEntity.ok(saleService.getAllSales());
    }

    @GetMapping("/{id}")
    public ResponseEntity<Sale> getSaleById(@PathVariable int id) {
        Sale sale = saleService.getSaleById(id);
        return sale != null ? ResponseEntity.ok(sale) : ResponseEntity.notFound().build();
    }

    @GetMapping("/employee/{employeeId}")
    public ResponseEntity<List<Sale>> getSalesByEmployee(@PathVariable int employeeId) {
        return ResponseEntity.ok(saleService.getSalesByEmployee(employeeId));
    }

    @PostMapping
    public ResponseEntity<Void> recordSale(@RequestBody Sale sale) {
        boolean result = saleService.recordSale(sale);
        return result ? ResponseEntity.ok().build() : ResponseEntity.badRequest().build();
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deleteSale(@PathVariable int id) {
        boolean result = saleService.deleteSale(id);
        return result ? ResponseEntity.ok().build() : ResponseEntity.notFound().build();
    }
}
