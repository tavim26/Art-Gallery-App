package com.gallery.userservice.service;

import com.gallery.userservice.domain.IUserRepository;
import com.gallery.userservice.domain.User;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class UserService {

    private final IUserRepository userRepository;

    public UserService(IUserRepository userRepository) {
        this.userRepository = userRepository;
    }

    public List<User> getAllUsers() {
        return userRepository.findAll();
    }

    public User getUserById(int id) {
        return userRepository.findById(id);
    }

    public boolean createUser(User user) {
        if (user.getName() == null || user.getRole() == null) return false;
        return userRepository.insert(user);
    }

    public boolean updateUser(User user) {
        return userRepository.update(user);
    }

    public boolean deleteUser(int id) {
        return userRepository.delete(id);
    }
}
