using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Lagerverwaltung
{
    internal class WarenkorbPosition
    {
        public string Artikelnummer { get; set; }
        public int Menge { get; set; }
    }

    internal class Warenkorb
    {
        public List<WarenkorbPosition> Items { get; set; } = new List<WarenkorbPosition>();
        public static string FilePath { get; } = "Warenkorb.json";

        public void Add(Material m, int menge = 1)
        {
            if (m == null || menge <= 0) return;
            var pos = Items.FirstOrDefault(i => i.Artikelnummer == m.Artikelnummer);
            if (pos == null)
                Items.Add(new WarenkorbPosition { Artikelnummer = m.Artikelnummer, Menge = menge });
            else
                pos.Menge += menge;
        }

        public void Checkout(List<Material> materialien)
        {
            foreach (var item in Items)
            {
                var mat = materialien.FirstOrDefault(m => m.Artikelnummer == item.Artikelnummer);
                if (mat == null)
                {
                    Console.WriteLine($"Material mit Id {item.Artikelnummer} nicht gefunden.");
                    continue;
                }

                if (mat.Bestand >= item.Menge)
                {
                    mat.Bestand -= item.Menge;
                }
                else
                {
                    Console.WriteLine($"Nicht genügend Bestand von {mat.Bezeichnung}. Gewünscht: {item.Menge}, Vorhanden: {mat.Bestand}");
                }
            }

            Items.Clear();
            Console.WriteLine("Checkout abgeschlossen. Bestand wurde aktualisiert.");
			Console.WriteLine("\nBitte weiter mit Enter");
			Console.ReadKey();
        }

        public void LadeDaten()
        {
            if (System.IO.File.Exists(FilePath))
            {
                var json = System.IO.File.ReadAllText(FilePath);
                var loaded = JsonSerializer.Deserialize<List<WarenkorbPosition>>(json);
                Items = loaded ?? new List<WarenkorbPosition>();
            }
        }

        public void SpeichereDaten()
        {
            var json = JsonSerializer.Serialize(Items, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(FilePath, json);
        }
    }
}
