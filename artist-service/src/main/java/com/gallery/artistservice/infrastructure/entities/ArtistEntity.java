package com.gallery.artistservice.infrastructure.entities;

import com.gallery.artistservice.domain.Artist;
import jakarta.persistence.*;

import java.time.LocalDate;

@Entity
@Table(name = "ARTIST")
public class ArtistEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;

    @Column(nullable = false)
    private String name;

    private LocalDate birthDate;
    private String birthplace;
    private String nationality;
    private String photo;

    public ArtistEntity() {}

    public ArtistEntity(Artist artist) {
        this.id = artist.getId().getId();
        this.name = artist.getName();
        this.birthDate = artist.getBirthDate();
        this.birthplace = artist.getBirthplace();
        this.nationality = artist.getNationality();
        this.photo = artist.getPhoto();
    }

    public Artist toDomain() {
        return new Artist(
                new com.gallery.artistservice.domain.ArtistID(this.id),
                this.name,
                this.birthDate,
                this.birthplace,
                this.nationality,
                this.photo
        );
    }

    // Getters și setters
    public int getId() { return id; }
    public void setId(int id) { this.id = id; }

    public String getName() { return name; }
    public void setName(String name) { this.name = name; }

    public LocalDate getBirthDate() { return birthDate; }
    public void setBirthDate(LocalDate birthDate) { this.birthDate = birthDate; }

    public String getBirthplace() { return birthplace; }
    public void setBirthplace(String birthplace) { this.birthplace = birthplace; }

    public String getNationality() { return nationality; }
    public void setNationality(String nationality) { this.nationality = nationality; }

    public String getPhoto() { return photo; }
    public void setPhoto(String photo) { this.photo = photo; }
}
