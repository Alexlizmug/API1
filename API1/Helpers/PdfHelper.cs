using API1.Modeloak;
using System.ComponentModel;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Drawing;
using QuestPDF.Previewer;
using QuestPDF.Elements;
using NHibernate.Hql.Ast;
using Microsoft.AspNetCore.Mvc.RazorPages;


public static class PdfHelper
{
    public static byte[] GenerateFakturaPdf(Faktura faktura, Zerbitzua zerbitzua, IList<Eskaerak> eskaera)
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(10);
                page.ContinuousSize(164);
                page.DefaultTextStyle(x => x.FontSize(12).FontColor("#373F47"));
                page.Header().Column(col =>
                {
                    col.Item().Text("Tricode Software S.L.").Bold().FontSize(18).FontColor("#373F47");
                });

                page.Content().Column(col =>
                {
                col.Item().LineHorizontal(1).LineColor(Colors.White);
                col.Item().Text($"Zerbitzuaren ID: {zerbitzua.Id}");
                col.Item().Text("Produktuak: ").Bold().FontSize(14);
                foreach(var e in eskaera)
                    {
                        col.Item().Text($"· {e.Izena}: {e.Prezioa}€").FontColor("#373F47");
                    }
                    col.Item().Text($"Data: {DateTime.Now:yyyy-MM-dd}");
                    col.Item().Text($"Prezio totala: {faktura.PrezioTotala}€").Bold();
                    col.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);

                    col.Spacing(25);
                    col.Item().Text("Argi Kalea 12, 20240 Ordizia, Gipuzkoa");
                    col.Item().Text("info@tricodesoft.eus");
                    col.Item().Text("+34 943 03 67 69");


                    col.Item().Text("Tricode Software S.L.-k faktura honetan jasotako zerbitzu guztiak kalitate-estandar" +
                        "zorrotzen arabera eskaini direla bermatzen du. Bezeroen datuen pribatutasuna zaintzen dugu eta" +
                        "indarreko araudi guztia betetzen dugu datuen babesari dagokionez.");

                    col.Item().LineHorizontal(1).LineColor("#E9C4C7");

                    col.Item().Text("Eskerrik asko zure konfiantzagatik!").Italic().FontColor(Colors.Grey.Darken1);
                });
                page.Footer().AlignCenter().Text("API - Fakturak").FontSize(10).FontColor(Colors.Grey.Darken2);
            });
        });
        return document.GeneratePdf();
    }
}
