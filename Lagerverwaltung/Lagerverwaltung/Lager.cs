using System;
using System.Linq;

namespace Lagerverwaltung
{
    internal class Lager
    {
        public void FuegeMaterialHinzu(string name, int menge, string lagerplatz, string artikelnummer, int bestand)
        {
            if (string.IsNullOrWhiteSpace(name) || bestand < 0)
            {
                Console.WriteLine("Ungültige Eingabe. Bitte Name und Bestand überprüfen.");
                Console.ReadKey();
                return;
            }

            Material.MaterialListe.Add(new Material(name, artikelnummer, lagerplatz, bestand));
            Material.SpeichereDaten();
            Console.WriteLine("Material wurde zum Bestand hinzugefügt.");
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
                    Console.WriteLine($"{i + 1}. Artikelnummer: {mat.Artikelnummer} | Name: {mat.Bezeichnung}, Lagerplatz: {mat.Lagerplatz}, Bestand: {mat.Bestand}");
                }
            }

            Console.ReadKey();
        }

        public void BearbeiteMaterial()
        {
            Material.LadeDaten();
            if (Material.MaterialListe.Count == 0)
            {
                Console.WriteLine("Kein Material vorhanden zum Bearbeiten.");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine("----- Material bearbeiten -----");
            for (int i = 0; i < Material.MaterialListe.Count; i++)
            {
                var m = Material.MaterialListe[i];
                Console.WriteLine($"{i + 1}. {m.Bezeichnung} (Artikelnummer: {m.Artikelnummer}, Lagerplatz: {m.Lagerplatz}, Bestand: {m.Bestand})");
            }

            Console.Write("Nummer des zu bearbeitenden Materials: ");
            if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > Material.MaterialListe.Count)
            {
                Console.WriteLine("Ungültige Nummer.");
                Console.ReadKey();
                return;
            }

            var material = Material.MaterialListe[index - 1];

            Console.WriteLine($"Aktuelle Bezeichnung: {material.Bezeichnung}");
            Console.Write("Neue Bezeichnung (Enter = unverändert): ");
            var input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) material.Bezeichnung = input;

            Console.WriteLine($"Aktuelle Artikelnummer: {material.Artikelnummer}");
            Console.Write("Neue Artikelnummer (Enter = unverändert): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) material.Artikelnummer = input;

            Console.WriteLine($"Aktueller Lagerplatz: {material.Lagerplatz}");
            Console.Write("Neuer Lagerplatz (Enter = unverändert): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) material.Lagerplatz = input;

            Console.WriteLine($"Aktueller Bestand: {material.Bestand}");
            Console.Write("Neuer Bestand (Enter = unverändert): ");
            input = Console.ReadLine();
            if (int.TryParse(input, out int neuerBestand))
            {
                if (neuerBestand >= 0) material.Bestand = neuerBestand;
                else Console.WriteLine("Bestand darf nicht negativ sein. Wert unverändert.");
            }
            else if (!string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Ungültige Eingabe für Bestand. Wert unverändert.");
            }

            Material.SpeichereDaten();
            Console.WriteLine("Material erfolgreich aktualisiert.");
            Console.ReadKey();
        }

        public void LoescheMaterial()
        {
            Material.LadeDaten();
            if (Material.MaterialListe.Count == 0)
            {
                Console.WriteLine("Kein Material vorhanden zum Löschen.");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine("----- Material löschen -----");
            for (int i = 0; i < Material.MaterialListe.Count; i++)
            {
                var m = Material.MaterialListe[i];
                Console.WriteLine($"{i + 1}. {m.Bezeichnung} (Artikelnummer: {m.Artikelnummer}, Lagerplatz: {m.Lagerplatz}, Bestand: {m.Bestand})");
            }

            Console.Write("Nummer des zu löschenden Materials: ");
            if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > Material.MaterialListe.Count)
            {
                Console.WriteLine("Ungültige Nummer.");
                Console.ReadKey();
                return;
            }

            var material = Material.MaterialListe[index - 1];
            Console.Write($"Bist du sicher, dass du '{material.Bezeichnung}' löschen möchtest? (j/n): ");
            var confirm = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(confirm) && (confirm.Equals("j", StringComparison.OrdinalIgnoreCase) || confirm.Equals("y", StringComparison.OrdinalIgnoreCase)))
            {
                Material.MaterialListe.RemoveAt(index - 1);
                Material.SpeichereDaten();
                Console.WriteLine("Material gelöscht.");
            }
            else
            {
                Console.WriteLine("Löschen abgebrochen.");
            }

            Console.ReadKey();
        }
    }
}
