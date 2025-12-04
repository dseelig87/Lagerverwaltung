using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Lagerverwaltung
{


    class Admin : AlleMitarbeiter
    {
       
        static public string MitarbeiterFile = "Mitarbeiter.json";
        static public List<Mitarbeiter> mitarbeiterListe = new List<Mitarbeiter>();

        public Admin(string user, string pw) : base(user, pw) { }
        public override bool IstAdmin => true;


        public void AdminFunktion()
        {
            LadeMitarbeiter();

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== Admin Bereich ===");
                Console.WriteLine("1. Material anzeigen");
                Console.WriteLine("2. Material hinzufügen");
                Console.WriteLine("3. Mitarbeiter anzeigen");
                Console.WriteLine("4. Mitarbeiter hinzufügen");
                Console.WriteLine("5. Mitarbeiter löschen");
                Console.WriteLine("6. Speicher und Beenden");
                Console.Write("Auswahl: ");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Lager.MaterialAnzeigen();
                        break;

                    case "2":
                        Lager.MaterialHinzufuegen();
                        break;

                    case "3":
                        ZeigeMitarbeiter();
                        break;

                    case "4":
                        NeuenMitarbeiterAnlegen();
                        break;

                    case "5":
                        LöscheMitarbeiter();
                        break;

                    case "6":
                        SpeichereMitarbeiter();
                        return;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ungültige Auswahl. Bitte erneut versuchen.");
                        Console.ReadKey();
                        Console.ResetColor();
                        break;
                }
            }
        }

        static void ZeigeMitarbeiter()
        {
            Console.Clear();
            Console.WriteLine("   Mitarbeiterliste");
            Console.WriteLine("=======================");
            if (mitarbeiterListe.Count == 0)
            {
                Console.WriteLine("Keine Mitarbeiter vorhanden.");
            }
            else
            {
                foreach (Mitarbeiter mitarbeiter in mitarbeiterListe)
                {
                    Console.WriteLine($"\n\nVorname: {mitarbeiter.Mitarbeitername} \nPasswort: {mitarbeiter.Passwort}");
                }
            }
            Console.WriteLine("\nDrücken Sie eine Taste, um zum Hauptmenü zurückzukehren.");
            Console.ReadKey();
        }

        static void NeuenMitarbeiterAnlegen()
        {
            Console.Clear();
            Console.WriteLine("Neuen Mitarbeiter anlegen");
            Console.WriteLine("<><><><><><><><><><><><><>");

            Mitarbeiter neuerMitarbeiter = new Mitarbeiter("mitarbeitername", "passwort");

            Console.Write("Mitarbeitername: ");
            neuerMitarbeiter.Mitarbeitername = Console.ReadLine();

            Console.Write("Passwort ");
            neuerMitarbeiter.Passwort = Console.ReadLine();


            mitarbeiterListe.Add(neuerMitarbeiter);
            SpeichereMitarbeiter();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Mitarbeiter erfolgreich angelegt.");
            Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("\nDrücken Sie eine Taste, um fortzufahren.");
            Console.ReadKey();
        }

        
        public static void LadeMitarbeiter()
        {
            if (System.IO.File.Exists(MitarbeiterFile))
            {
                string json = System.IO.File.ReadAllText(MitarbeiterFile);
                mitarbeiterListe = JsonSerializer.Deserialize<List<Mitarbeiter>>(json) ?? new List<Mitarbeiter>();
            }
        }

        static void LöscheMitarbeiter()
        {
            Console.Clear();
            Console.WriteLine("Mitarbeiter löschen");
            for (int i = 0; i < mitarbeiterListe.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {mitarbeiterListe[i].Mitarbeitername} {mitarbeiterListe[i].Passwort}");
            }
            Console.Write("Geben Sie die Nummer des zu löschenden Mitarbeiters ein: ");
            if (int.TryParse(Console.ReadLine(), out int idx))
            {
                int index = idx - 1;
                if (index >= 0 && index < mitarbeiterListe.Count)
                {
                    mitarbeiterListe.RemoveAt(index);
                    SpeichereMitarbeiter();
                    Console.WriteLine("Mitarbeiter erfolgreich gelöscht. Drücken Sie eine Taste, um fortzufahren.");
                }
                else
                {
                    Console.WriteLine("Ungültige Nummer. Drücken Sie eine Taste, um fortzufahren.");
                }
            }
            else
            {
                Console.WriteLine("Ungültige Eingabe. Drücken Sie eine Taste, um fortzufahren.");
            }
            Console.ReadKey();
        }

        
        static void SpeichereMitarbeiter()
        {
            string json = JsonSerializer.Serialize(mitarbeiterListe, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(MitarbeiterFile, json);
        }
    }
}










