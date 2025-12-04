using System;
using System.Linq;

namespace Lagerverwaltung
{
    internal class Lager
    {
        public void FuegeMaterialHinzu(string name,  string lagerplatz, string artikelnummer, int bestand)
        {
            if (string.IsNullOrWhiteSpace(name) || bestand < 0)
            {
                Console.WriteLine("Ungültige Eingabe. Bitte Name und Bestand überprüfen.");
				Console.WriteLine("\nBitte weiter mit Enter");
				Console.ReadKey();
                return;
            }

            Material.MaterialListe.Add(new Material(name, artikelnummer, lagerplatz,  bestand));
            Material.SpeichereDaten();
            Console.WriteLine("Material wurde zum Bestand hinzugefügt.");
			Console.WriteLine("\nBitte weiter mit Enter");
			Console.ReadKey();
        }

        public void ZeigeMaterial()
        {
            Material.LadeDaten();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("          Aktueller Materialbestand:");
            Console.WriteLine("--------------------------------------------------");
            Console.ResetColor();

            if (Material.MaterialListe.Count == 0)
            {
                Console.WriteLine("Der Lagerbestand ist leer.");
            }
            else
            {
                for (int i = 0; i < Material.MaterialListe.Count; i++)
                {
                    var mat = Material.MaterialListe[i];
                    Console.WriteLine($"{i + 1} | Name: {mat.Bezeichnung}, Artikelnummer: {mat.Artikelnummer}, Lagerplatz: {mat.Lagerplatz}, Bestand: {mat.Bestand}\n");
                }
            }

            Console.WriteLine("\nBitte weiter mit Enter");
            Console.ReadKey();
        }
    }
}
