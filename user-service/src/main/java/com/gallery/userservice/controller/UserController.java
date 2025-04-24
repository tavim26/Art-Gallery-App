package com.gallery.userservice.controller;

import com.gallery.userservice.domain.User;
import com.gallery.userservice.service.UserService;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/users")
public class UserController {

    private final UserService userService;

    public UserController(UserService userService) {
        this.userService = userService;
    }

    @GetMapping
    public ResponseEntity<List<User>> getAllUsers() {
        return ResponseEntity.ok(userService.getAllUsers());
    }

    @GetMapping("/{id}")
    public ResponseEntity<User> getUserById(@PathVariable int id) {
        User user = userService.getUserById(id);
        return user != null ? ResponseEntity.ok(user) : ResponseEntity.notFound().build();
    }

    @PostMapping
    public ResponseEntity<Void> createUser(@RequestBody User user) {
        boolean result = userService.createUser(user);
        return result ? ResponseEntity.ok().build() : ResponseEntity.badRequest().build();
    }

    @PutMapping
    public ResponseEntity<Void> updateUser(@RequestBody User user) {
        boolean result = userService.updateUser(user);
        return result ? ResponseEntity.ok().build() : ResponseEntity.badRequest().build();
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deleteUser(@PathVariable int id) {
        boolean result = userService.deleteUser(id);
        return result ? ResponseEntity.ok().build() : ResponseEntity.notFound().build();
    }
}
