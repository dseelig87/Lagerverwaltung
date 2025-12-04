using System;
using System.Collections.Generic;
using System.Linq;
namespace Lagerverwaltung
{
    internal class Login
    {
        private List<AlleMitarbeiter> _benutzer = new List<AlleMitarbeiter>();

        public Login()
        {
            SeedUsers();
        }

        private void SeedUsers()
        {
            // immer Admin verfügbar machen
            _benutzer.Add(new Admin("admin", "1234"));

            // gespeicherte Mitarbeiter aus Admin.mitarbeiterListe laden
            try
            {
                Admin.LadeMitarbeiter();
                foreach (var m in Admin.mitarbeiterListe)
                {
                    // Duplikate vermeiden (z.B. wenn derselbe Name schon vorhanden)
                    if (!_benutzer.Any(u => string.Equals(u.Mitarbeiter, m.Mitarbeiter, StringComparison.OrdinalIgnoreCase)))
                    {
                        _benutzer.Add(m);
                    }
                }
            }
            catch
            {
                // bei Fehlern einfach mit Default-Benutzern weiterarbeiten
            }

            // optional: Beispiel-Account, falls keine Mitarbeiter vorhanden sind
            if (!_benutzer.Any(u => u.Mitarbeiter.Equals("Max", StringComparison.OrdinalIgnoreCase)))
            {
                _benutzer.Add(new Mitarbeiter("Max", "pass"));
            }
        }

        public AlleMitarbeiter? Anmelden(int maxVersuche = 3)
        {
            for (int versuch = 1; versuch <= maxVersuche; versuch++)
            {
               
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("                             <><><><><><><><> Lagerverwaltung <><><><><><><><>");
				Console.ResetColor();
				Console.WriteLine("1. Login");
				Console.WriteLine("2. Beenden");
				Console.Write("Bitte wählen Sie eine Option: ");
				string mitarbeitername = Console.ReadLine() ?? string.Empty;

                Console.Write("Passwort: ");
                string passwort = ReadPassword();

                var mitarbeiter = _benutzer
                    .FirstOrDefault(u => string.Equals(u.Mitarbeiter, mitarbeitername, StringComparison.OrdinalIgnoreCase)
                                      && string.Equals(u.Passwort, passwort, StringComparison.Ordinal));

                if (mitarbeiter is null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Login fehlgeschlagen!");
                    Console.ResetColor();

                    if (versuch < maxVersuche)
                    {
                        Console.WriteLine($"Versuch {versuch} von {maxVersuche}. Drücken Sie eine Taste für einen weiteren Versuch.");
                        Console.ReadKey(true);
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Maximale Anzahl Versuche erreicht.");
                        Console.ReadKey(true);
                        return null;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Login erfolgreich! Willkommen {mitarbeiter.Mitarbeiter}.");
                    Console.ResetColor();
                    Console.ReadKey(true);
                    return mitarbeiter;
                }
            }

            return null;
        }

        private static string ReadPassword()
        {
            var pwd = new System.Text.StringBuilder();
            ConsoleKeyInfo key;
            while ((key = Console.ReadKey(true)).Key != ConsoleKey.Enter)
            {
                if (key.Key == ConsoleKey.Backspace)
                {
                    if (pwd.Length > 0)
                    {
                        pwd.Length--;
                        Console.Write("\b \b");
                    }
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    pwd.Append(key.KeyChar);
                    Console.Write('*');
                }
            }
            Console.WriteLine();
            return pwd.ToString();
        }
    }
}
