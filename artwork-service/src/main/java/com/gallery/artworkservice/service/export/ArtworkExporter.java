package com.gallery.artworkservice.service.export;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.gallery.artworkservice.domain.Artwork;
import org.apache.poi.xwpf.usermodel.*;

import org.springframework.stereotype.Component;

import java.io.ByteArrayOutputStream;
import java.nio.charset.StandardCharsets;
import java.util.List;

@Component
public class ArtworkExporter {

    public String exportToCsv(List<Artwork> artworks) {
        StringBuilder sb = new StringBuilder();
        sb.append("Id,Title,YearCreated,Type,ArtistId\n");
        for (Artwork a : artworks) {
            sb.append(a.getId().getId()).append(",")
                    .append(a.getTitle()).append(",")
                    .append(a.getYearCreated()).append(",")
                    .append(a.getType()).append(",")
                    .append(a.getArtistId()).append("\n");
        }
        return sb.toString();
    }

    public String exportToJson(List<Artwork> artworks) {
        try {
            ObjectMapper mapper = new ObjectMapper();
            return mapper.writerWithDefaultPrettyPrinter().writeValueAsString(artworks);
        } catch (Exception e) {
            return "{}";
        }
    }

    public byte[] exportToDoc(List<Artwork> artworks) {
        try (XWPFDocument document = new XWPFDocument();
             ByteArrayOutputStream out = new ByteArrayOutputStream()) {

            XWPFParagraph title = document.createParagraph();
            title.setAlignment(ParagraphAlignment.CENTER);
            XWPFRun run = title.createRun();
            run.setText("List of Artworks");
            run.setBold(true);
            run.setFontSize(16);

            for (Artwork a : artworks) {
                XWPFParagraph p = document.createParagraph();
                XWPFRun r = p.createRun();
                r.setText("ID: " + a.getId().getId() +
                        ", Title: " + a.getTitle() +
                        ", Year: " + a.getYearCreated() +
                        ", Type: " + a.getType() +
                        ", ArtistID: " + a.getArtistId());
                r.addBreak();
            }

            document.write(out);
            return out.toByteArray();

        } catch (Exception e) {
            return "Error creating DOC".getBytes(StandardCharsets.UTF_8);
        }
    }
}
