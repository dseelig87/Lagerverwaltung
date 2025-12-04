using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagerverwaltung
{
    internal class Lager
    {
        public static List<Material> materialListe = new List<Material>();




        public static void MaterialHinzufuegen(string bezeichnung, int menge, string lagerplatz, string artikelnummer, int bestand)
        {
            if (!string.IsNullOrWhiteSpace(bezeichnung) && bestand >= 0)
            {
                materialListe.Add(new Material(bezeichnung, artikelnummer, lagerplatz, menge, bestand));
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
                for (int i = 0; i < materialListe.Count; i++)
                {
                    Material mat = materialListe[i];
                    Console.WriteLine($"{i + 1}. Name: {mat.Bezeichnug}, Artikelnummer: {mat.Artikelnummer}, Lagerplatz: {mat.Lagerplatz}, Menge: {mat.Menge}, Bestand: {mat.Bestand}");
                }
        }
    }
}
