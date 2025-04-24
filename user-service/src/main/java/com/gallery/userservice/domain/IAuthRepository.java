package com.gallery.userservice.domain;

public interface IAuthRepository {
    Auth findByEmail(String email);
    boolean insert(Auth auth);
    boolean deleteByUserId(int userId);
}
