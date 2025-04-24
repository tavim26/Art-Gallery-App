package com.gallery.saleservice.infrastructure;

import com.gallery.saleservice.domain.Sale;
import jakarta.persistence.*;

import java.math.BigDecimal;
import java.time.LocalDateTime;

@Entity
@Table(name = "SALE")
public class SaleEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;

    @Column(name = "ArtworkId", nullable = false)
    private int artworkId;

    @Column(name = "EmployeeId", nullable = false)
    private int employeeId;

    @Column(name = "SaleDate", nullable = false)
    private LocalDateTime saleDate;

    @Column(nullable = false)
    private BigDecimal price;

    public SaleEntity() {}

    public SaleEntity(Sale sale) {
        this.id = sale.getId().getId();
        this.artworkId = sale.getArtworkId();
        this.employeeId = sale.getEmployeeId();
        this.saleDate = sale.getSaleDate();
        this.price = sale.getPrice();
    }

    public Sale toDomain() {
        return new Sale(
                new com.gallery.saleservice.domain.SaleID(this.id),
                this.artworkId,
                this.employeeId,
                this.saleDate,
                this.price
        );
    }

    // Getters & setters
    public int getId() { return id; }
    public void setId(int id) { this.id = id; }

    public int getArtworkId() { return artworkId; }
    public void setArtworkId(int artworkId) { this.artworkId = artworkId; }

    public int getEmployeeId() { return employeeId; }
    public void setEmployeeId(int employeeId) { this.employeeId = employeeId; }

    public LocalDateTime getSaleDate() { return saleDate; }
    public void setSaleDate(LocalDateTime saleDate) { this.saleDate = saleDate; }

    public BigDecimal getPrice() { return price; }
    public void setPrice(BigDecimal price) { this.price = price; }
}
