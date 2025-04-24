package com.gallery.userservice.domain;

import java.util.List;

public interface IUserRepository {
    List<User> findAll();
    User findById(int id);
    boolean insert(User user);
    boolean update(User user);
    boolean delete(int id);
}
