package com.gallery.userservice.service.notification;

import org.springframework.stereotype.Component;

import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.nio.charset.StandardCharsets;

@Component
public class SmsNotification implements NotificationStrategy
{

    @Override
    public void send(String phone, String message)
    {
        try {
            String apiUrl = "https://textbelt.com/text";
            String postData = "phone=" + phone +
                    "&message=" + message +
                    "&key=textbelt";

            byte[] postDataBytes = postData.getBytes(StandardCharsets.UTF_8);

            HttpURLConnection conn = (HttpURLConnection) new URL(apiUrl).openConnection();
            conn.setRequestMethod("POST");
            conn.setDoOutput(true);
            conn.setRequestProperty("Content-Type", "application/x-www-form-urlencoded");
            conn.setRequestProperty("Content-Length", String.valueOf(postDataBytes.length));

            OutputStream out = conn.getOutputStream();
            out.write(postDataBytes);
            out.flush();
            out.close();

            int responseCode = conn.getResponseCode();
            System.out.println("SMS sent, HTTP response: " + responseCode);

        } catch (Exception e) {
            System.err.println("Sms sending error " + e.getMessage());
        }
    }
}
