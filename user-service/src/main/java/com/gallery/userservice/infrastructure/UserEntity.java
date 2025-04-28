package com.gallery.userservice.infrastructure;

import com.gallery.userservice.domain.User;
import jakarta.persistence.*;

@Entity
@Table(name = "USERS")
public class UserEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;

    @Column(nullable = false)
    private String name;

    @Column(nullable = false)
    private String role;

    private String phone;

    public UserEntity() {}

    public UserEntity(User user) {
        this.id = user.getId().getId();
        this.name = user.getName();
        this.role = user.getRole();
        this.phone = user.getPhone();
    }

    public User toDomain() {
        return new User(
                new com.gallery.userservice.domain.UserID(this.id),
                this.name,
                this.role,
                this.phone
        );
    }

    // Getters & Setters
    public int getId() { return id; }
    public void setId(int id) { this.id = id; }

    public String getName() { return name; }
    public void setName(String name) { this.name = name; }

    public String getRole() { return role; }
    public void setRole(String role) { this.role = role; }

    public String getPhone() { return phone; }
    public void setPhone(String phone) { this.phone = phone; }
}
