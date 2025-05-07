using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using SaleService.Domain;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

public static class PdfGenerator
{
    public static byte[] GenerateSalePdf(Sale sale)
    {
        var doc = new PdfDocument();
        var page = doc.AddPage();
        var gfx = XGraphics.FromPdfPage(page);
        var font = new XFont("Arial", 14);

        gfx.DrawString("Sale Receipt", font, XBrushes.Black, new XRect(0, 20, page.Width, 0), XStringFormats.TopCenter);
        gfx.DrawString($"Sale ID: {sale.Id}", font, XBrushes.Black, new XPoint(40, 80));
        gfx.DrawString($"Artwork ID: {sale.ArtworkId}", font, XBrushes.Black, new XPoint(40, 110));
        gfx.DrawString($"Employee ID: {sale.EmployeeId}", font, XBrushes.Black, new XPoint(40, 140));
        gfx.DrawString($"Date: {sale.SaleDate:yyyy-MM-dd}", font, XBrushes.Black, new XPoint(40, 170));
        gfx.DrawString($"Price: {sale.Price} €", font, XBrushes.Black, new XPoint(40, 200));

        using var stream = new MemoryStream();
        doc.Save(stream);
        return stream.ToArray();
    }
}
