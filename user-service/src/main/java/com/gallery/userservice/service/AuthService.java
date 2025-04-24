package com.gallery.userservice.service;

import com.gallery.userservice.domain.Auth;
import com.gallery.userservice.domain.IAuthRepository;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.stereotype.Service;

@Service
public class AuthService {

    private final IAuthRepository authRepository;
    private final BCryptPasswordEncoder encoder;

    public AuthService(IAuthRepository authRepository) {
        this.authRepository = authRepository;
        this.encoder = new BCryptPasswordEncoder();
    }

    public boolean signUp(Auth auth) {
        if (auth.getEmail() == null || auth.getPasswordHash() == null)
            return false;


        String hashedPassword = encoder.encode(auth.getPasswordHash());
        Auth hashedAuth = new Auth(0, auth.getUserId(), auth.getEmail(), hashedPassword);

        return authRepository.insert(hashedAuth);
    }

    public boolean login(String email, String password) {
        Auth auth = authRepository.findByEmail(email);
        if (auth == null) return false;

        return encoder.matches(password, auth.getPasswordHash());
    }
}
