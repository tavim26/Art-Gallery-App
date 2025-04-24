package com.gallery.userservice.controller;

import com.gallery.userservice.service.notification.NotificationService;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api/notify")
public class NotificationController {

    private final NotificationService notificationService;

    public NotificationController(NotificationService notificationService) {
        this.notificationService = notificationService;
    }

    @PostMapping("/email")
    public ResponseEntity<Void> notifyByEmail(@RequestParam String to, @RequestParam String message) {
        notificationService.notifyByEmail(to, message);
        return ResponseEntity.ok().build();
    }

    @PostMapping("/sms")
    public ResponseEntity<Void> notifyBySms(@RequestParam String to, @RequestParam String message) {
        notificationService.notifyBySms(to, message);
        return ResponseEntity.ok().build();
    }
}
