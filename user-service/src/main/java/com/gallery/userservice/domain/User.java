package com.gallery.userservice.domain;

public class User {
    private UserID id;
    private String name;
    private String role;
    private String phone;

    public User() {
        this.id = new UserID();
        this.name = "";
        this.role = "";
        this.phone = "";
    }

    public User(UserID id, String name, String role, String phone) {
        this.id = id;
        this.name = name;
        this.role = role;
        this.phone = phone;
    }

    public UserID getId() { return id; }
    public void setId(UserID id) { this.id = id; }

    public String getName() { return name; }
    public void setName(String name) { this.name = name; }

    public String getRole() { return role; }
    public void setRole(String role) { this.role = role; }

    public String getPhone() { return phone; }
    public void setPhone(String phone) { this.phone = phone; }
}
