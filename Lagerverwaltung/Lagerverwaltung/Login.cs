using System;
using System.Linq;

namespace Lagerverwaltung
{
    internal class Login
    {
        public Login()
        {
            AlleMitarbeiter.LadeDaten();
        }

        public AlleMitarbeiter Anmelden()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("   ========== Login =========");
            Console.WriteLine("_________________________________");
            Console.ResetColor();
			

			Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Passwort: ");
            string password = Console.ReadLine();

            var user = AlleMitarbeiter.BenutzerListe
                .FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user == null)
            {
                Console.WriteLine("Login fehlgeschlagen!");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"Login erfolgreich! Willkommen {user.Username}.");
                Console.ReadKey();
            }

            return user;
        }
    }
}
