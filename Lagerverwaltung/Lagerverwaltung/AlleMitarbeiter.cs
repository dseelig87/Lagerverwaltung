using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Lagerverwaltung
{
    internal class AlleMitarbeiter
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IstAdmin { get; set; }
        public Warenkorb Warenkorb { get; } = new Warenkorb();

        public static string FilePath { get; } = "Mitarbeiter.json";
        public static List<AlleMitarbeiter> BenutzerListe { get; private set; } = new List<AlleMitarbeiter>();

        public AlleMitarbeiter() { }

        public AlleMitarbeiter(string username, string password, bool istAdmin = false)
        {
            Username = username;
            Password = password;
            IstAdmin = istAdmin;
        }

        public static void LadeDaten()
        {
            if (System.IO.File.Exists(FilePath))
            {
                var json = System.IO.File.ReadAllText(FilePath);
                BenutzerListe = JsonSerializer.Deserialize<List<AlleMitarbeiter>>(json) ?? new List<AlleMitarbeiter>();
            }
            else
            {
                // Default-Account beim ersten Start
                BenutzerListe = new List<AlleMitarbeiter> { new AlleMitarbeiter("Dave", "1234", true) };
                SpeichereDaten();
            }
        }

        public static void SpeichereDaten()
        {
            var json = JsonSerializer.Serialize(BenutzerListe, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(FilePath, json);
        }

        public void FuegeZuWarenkorb(Material material, int anzahl = 1)
        {
            Warenkorb.Add(material, anzahl);
        }
    }
}
