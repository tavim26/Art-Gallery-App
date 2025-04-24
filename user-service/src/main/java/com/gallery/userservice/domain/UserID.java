package com.gallery.userservice.domain;

public class UserID {
    private int id;

    public UserID() { this.id = 0; }
    public UserID(int id) { this.id = id; }
    public UserID(UserID other) { this.id = other.id; }

    public int getId() { return id; }
    public void setId(int id) { this.id = id; }
}
