package com.gallery.userservice.service.notification;

import jakarta.mail.Message;
import jakarta.mail.MessagingException;
import jakarta.mail.PasswordAuthentication;
import jakarta.mail.Session;
import jakarta.mail.Transport;
import jakarta.mail.internet.InternetAddress;
import jakarta.mail.internet.MimeMessage;
import org.springframework.stereotype.Component;

import java.util.Properties;

@Component
public class EmailNotification implements NotificationStrategy
{

    private final String senderEmail = "your_email@gmail.com";
    private final String senderPassword = "your_app_password";

    @Override
    public void send(String recipientEmail, String messageText) {
        Properties props = new Properties();
        props.put("mail.smtp.auth", "true");
        props.put("mail.smtp.starttls.enable", "true");
        props.put("mail.smtp.host", "smtp.gmail.com");
        props.put("mail.smtp.port", "587");

        Session session = Session.getInstance(props, new jakarta.mail.Authenticator() {
            @Override
            protected PasswordAuthentication getPasswordAuthentication() {
                return new PasswordAuthentication(senderEmail, senderPassword);
            }
        });

        try {
            Message message = new MimeMessage(session);
            message.setFrom(new InternetAddress(senderEmail));
            message.setRecipients(
                    Message.RecipientType.TO,
                    InternetAddress.parse(recipientEmail)
            );
            message.setSubject("Notification - Art Gallery");
            message.setText(messageText);

            Transport.send(message);
            System.out.println("Email sent to: " + recipientEmail);
        } catch (MessagingException e) {
            System.err.println("Error sending email:  " + e.getMessage());
        }
    }
}
