package com.gallery.artistservice.domain;

import java.time.LocalDate;

public class Artist {
    private ArtistID id;
    private String name;
    private LocalDate birthDate;
    private String birthplace;
    private String nationality;
    private String photo;

    public Artist() {
        this.id = new ArtistID();
        this.name = "";
        this.birthDate = LocalDate.now();
        this.birthplace = "";
        this.nationality = "";
        this.photo = "";
    }

    public Artist(ArtistID id, String name, LocalDate birthDate, String birthplace, String nationality, String photo) {
        this.id = id;
        this.name = name;
        this.birthDate = birthDate;
        this.birthplace = birthplace;
        this.nationality = nationality;
        this.photo = photo;
    }

    public ArtistID getId() {
        return id;
    }

    public void setId(ArtistID id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public LocalDate getBirthDate() {
        return birthDate;
    }

    public void setBirthDate(LocalDate birthDate) {
        this.birthDate = birthDate;
    }

    public String getBirthplace() {
        return birthplace;
    }

    public void setBirthplace(String birthplace) {
        this.birthplace = birthplace;
    }

    public String getNationality() {
        return nationality;
    }

    public void setNationality(String nationality) {
        this.nationality = nationality;
    }

    public String getPhoto() {
        return photo;
    }

    public void setPhoto(String photo) {
        this.photo = photo;
    }
}
