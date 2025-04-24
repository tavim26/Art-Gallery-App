package com.gallery.userservice.domain;

public class Auth {
    private int id;
    private int userId;
    private String email;
    private String passwordHash;

    public Auth() {}

    public Auth(int id, int userId, String email, String passwordHash) {
        this.id = id;
        this.userId = userId;
        this.email = email;
        this.passwordHash = passwordHash;
    }

    public int getId() { return id; }
    public void setId(int id) { this.id = id; }

    public int getUserId() { return userId; }
    public void setUserId(int userId) { this.userId = userId; }

    public String getEmail() { return email; }
    public void setEmail(String email) { this.email = email; }

    public String getPasswordHash() { return passwordHash; }
    public void setPasswordHash(String passwordHash) { this.passwordHash = passwordHash; }
}
