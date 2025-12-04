using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace Lagerverwaltung
{
    internal class Lager
    {
        public static List<Material> materialListe = new List<Material>();
        private static readonly string filePath = "Material.json";

        // Statischer Konstruktor lädt Material beim ersten Zugriff
        static Lager()
        {
            LadeMaterial();
        }

        public static void MaterialHinzufuegen()
        {
            Console.Write("Materialbezeichnung: ");
            string bezeichnung = Console.ReadLine();

            Console.Write("Menge: ");
            if (!int.TryParse(Console.ReadLine(), out int menge))
            {
                Console.WriteLine("Ungültige Menge, wird auf 0 gesetzt.");
                menge = 0;
            }

            Console.Write("Lagerplatz: ");
            string lagerplatz = Console.ReadLine();

            Console.Write("Artikelnummer: ");
            string artikelnummer = Console.ReadLine();

            Console.Write("Bestand: ");
            if (!int.TryParse(Console.ReadLine(), out int bestand))
            {
                Console.WriteLine("Ungültiger Bestand, wird auf 0 gesetzt.");
                bestand = 0;
            }

            if (!string.IsNullOrWhiteSpace(bezeichnung) && bestand >= 0)
            {
                materialListe.Add(new Material(bezeichnung, artikelnummer, lagerplatz, menge, bestand));
                SpeichereMaterial();
                Console.WriteLine("Material wurde zum Bestand hinzugefügt");
            }
            else
            {
                Console.WriteLine("Ungültige Eingabe. Bitte überprüfen Sie den Namen und den Bestand.");
            }

            Console.ReadKey();
        }

        public static void MaterialAnzeigen()
        {
            // Lade aktuelle Daten (falls außerhalb verändert)
            LadeMaterial();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("          Aktueller Materialbestand:");
            Console.WriteLine("--------------------------------------------------");
            Console.ResetColor();

            if (materialListe.Count == 0)
            {
                Console.WriteLine("Der Lagerbestand ist leer.");
            }
            else
            {
                for (int i = 0; i < materialListe.Count; i++)
                {
                    Material mat = materialListe[i];
                    Console.WriteLine($"{i + 1}. Name: {mat.Bezeichnug}, Artikelnummer: {mat.Artikelnummer}, Lagerplatz: {mat.Lagerplatz}, Menge: {mat.Menge}, Bestand: {mat.Bestand}");
                }
            }

            Console.WriteLine("\nDrücken Sie eine Taste, um fortzufahren.");
            Console.ReadKey();
        }
        // Vom Copilot fixen lassen
        private static void LadeMaterial()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    var list = JsonSerializer.Deserialize<List<Material>>(json);
                    materialListe = list ?? new List<Material>();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Fehler beim Laden der Materialien: {ex.Message}");
                Console.ResetColor();
                materialListe = materialListe ?? new List<Material>();
            }
        }
		// Vom Copilot fixen lassen
		private static void SpeichereMaterial()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(materialListe, options);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Fehler beim Speichern der Materialien: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}
