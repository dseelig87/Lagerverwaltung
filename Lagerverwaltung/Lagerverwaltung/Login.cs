namespace Lagerverwaltung
{
    internal class Login

    {
        public List<Mitarbeiter> benutzer = new List<Mitarbeiter>();

        public Login()
        {

            //benutzer.Add(new Admin("admin", "1234"));
            benutzer.Add(new Mitarbeiter("max", "pass"));
        }

        public Mitarbeiter Anmelden()
        {
            Console.Clear();
            Console.WriteLine("=== Login ===");

            Console.Write("Mitarbeitername: ");
            string mitarbeitername = Console.ReadLine();

            Console.Write("Passwort: ");
            string passwort = Console.ReadLine();

            Mitarbeiter mitarbeiter = benutzer
                .FirstOrDefault(u => u.Mitarbeiter == mitarbeitername && u.Passwort == passwort);

            if (mitarbeiter == null)
            {
                Console.WriteLine("Login fehlgeschlagen!");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"Login erfolgreich! Willkommen {mitarbeiter.Mitarbeiter}.");
                Console.ReadKey();
            }

            return mitarbeiter;
        }
    }
}
