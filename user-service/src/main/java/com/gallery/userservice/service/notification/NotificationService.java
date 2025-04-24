package com.gallery.userservice.service.notification;

import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class NotificationService {

    private final List<NotificationStrategy> strategies;

    public NotificationService(List<NotificationStrategy> strategies) {
        this.strategies = strategies;
    }

    public void notifyByEmail(String email, String message) {
        for (NotificationStrategy strategy : strategies) {
            if (strategy instanceof EmailNotification && email != null) {
                strategy.send(email, message);
            }
        }
    }

    public void notifyBySms(String phone, String message) {
        for (NotificationStrategy strategy : strategies) {
            if (strategy instanceof SmsNotification && phone != null) {
                strategy.send(phone, message);
            }
        }
    }
}
