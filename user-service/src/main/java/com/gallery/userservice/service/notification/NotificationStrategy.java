package com.gallery.userservice.service.notification;

public interface NotificationStrategy {
    void send(String destination, String message);
}
