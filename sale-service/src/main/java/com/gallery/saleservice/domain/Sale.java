package com.gallery.saleservice.domain;

import java.math.BigDecimal;
import java.time.LocalDateTime;

public class Sale {
    private SaleID id;
    private int artworkId;
    private int employeeId;
    private LocalDateTime saleDate;
    private BigDecimal price;

    public Sale() {
        this.id = new SaleID();
        this.artworkId = 0;
        this.employeeId = 0;
        this.saleDate = LocalDateTime.now();
        this.price = BigDecimal.ZERO;
    }

    public Sale(SaleID id, int artworkId, int employeeId, LocalDateTime saleDate, BigDecimal price) {
        this.id = id;
        this.artworkId = artworkId;
        this.employeeId = employeeId;
        this.saleDate = saleDate;
        this.price = price;
    }

    public SaleID getId() {
        return id;
    }

    public void setId(SaleID id) {
        this.id = id;
    }

    public int getArtworkId() {
        return artworkId;
    }

    public void setArtworkId(int artworkId) {
        this.artworkId = artworkId;
    }

    public int getEmployeeId() {
        return employeeId;
    }

    public void setEmployeeId(int employeeId) {
        this.employeeId = employeeId;
    }

    public LocalDateTime getSaleDate() {
        return saleDate;
    }

    public void setSaleDate(LocalDateTime saleDate) {
        this.saleDate = saleDate;
    }

    public BigDecimal getPrice() {
        return price;
    }

    public void setPrice(BigDecimal price) {
        this.price = price;
    }
}
