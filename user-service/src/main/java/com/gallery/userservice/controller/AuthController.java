package com.gallery.userservice.controller;

import com.gallery.userservice.domain.Auth;
import com.gallery.userservice.service.AuthService;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api/auth")
public class AuthController {

    private final AuthService authService;

    public AuthController(AuthService authService) {
        this.authService = authService;
    }

    @PostMapping("/signup")
    public ResponseEntity<Void> signUp(@RequestBody Auth auth) {
        boolean result = authService.signUp(auth);
        return result ? ResponseEntity.ok().build() : ResponseEntity.badRequest().build();
    }

    @PostMapping("/login")
    public ResponseEntity<Void> login(@RequestParam String email, @RequestParam String password) {
        boolean result = authService.login(email, password);
        return result ? ResponseEntity.ok().build() : ResponseEntity.status(401).build();
    }

    @PostMapping("/logout")
    public ResponseEntity<Void> logout() {
        // Doar clientul șterge tokenul în acest caz (nu avem sesiuni persistente)
        return ResponseEntity.ok().build();
    }
}
