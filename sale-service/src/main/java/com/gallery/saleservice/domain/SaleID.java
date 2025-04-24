package com.gallery.saleservice.domain;

public class SaleID {
    private int id;

    public SaleID() {
        this.id = 0;
    }

    public SaleID(int id) {
        this.id = id;
    }

    public SaleID(SaleID other) {
        this.id = other.id;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }
}
