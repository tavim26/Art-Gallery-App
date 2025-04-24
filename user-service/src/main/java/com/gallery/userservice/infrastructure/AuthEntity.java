package com.gallery.userservice.infrastructure;

import com.gallery.userservice.domain.Auth;
import jakarta.persistence.*;

@Entity
@Table(name = "AUTH")
public class AuthEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;

    @Column(name = "UserId", nullable = false)
    private int userId;

    @Column(nullable = false, unique = true)
    private String email;

    @Column(name = "PasswordHash", nullable = false)
    private String passwordHash;

    public AuthEntity() {}

    public AuthEntity(Auth auth) {
        this.id = auth.getId();
        this.userId = auth.getUserId();
        this.email = auth.getEmail();
        this.passwordHash = auth.getPasswordHash();
    }

    public Auth toDomain() {
        return new Auth(
                this.id,
                this.userId,
                this.email,
                this.passwordHash
        );
    }

    // Getters & Setters
    public int getId() { return id; }
    public void setId(int id) { this.id = id; }

    public int getUserId() { return userId; }
    public void setUserId(int userId) { this.userId = userId; }

    public String getEmail() { return email; }
    public void setEmail(String email) { this.email = email; }

    public String getPasswordHash() { return passwordHash; }
    public void setPasswordHash(String passwordHash) { this.passwordHash = passwordHash; }
}
