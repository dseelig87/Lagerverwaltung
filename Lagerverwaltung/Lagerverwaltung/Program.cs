using System;
using System.Linq;

namespace Lagerverwaltung
{
    internal class Program
    {
        static Lager lager = new Lager();
        static Login login = new Login();

        static void Main(string[] args)
        {
            Material.LadeDaten();
            AlleMitarbeiter.LadeDaten();

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("                <<<<<><><><>>>>> Lagerverwaltung <<<<<><><><>>>>>");
                Console.WriteLine("--------------------------------------------------------------------------");
                Console.ResetColor();
				Console.WriteLine("\n1. Login");
                Console.WriteLine("2. Beenden");
                Console.Write("Bitte wählen Sie eine Option: ");
                var wahl = Console.ReadLine();

                if (wahl == "1")
                {
                    var user = login.Anmelden();
                    if (user == null) continue;

                    if (user.IstAdmin) AdminMenu(user);
                    else MitarbeiterMenu(user);

                    
                    Material.SpeichereDaten();
                    AlleMitarbeiter.SpeichereDaten();
                }
                else break;
            }
        }

        static void AdminMenu(AlleMitarbeiter user)
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"<<<<<><><><>>>>> Admin Bereich ({user.Username}) <<<<<><><><>>>>>");
                Console.ResetColor();
				Console.WriteLine("\n1. Material anzeigen");
                Console.WriteLine("2. Material hinzufügen");
                Console.WriteLine("3. Material bearbeiten");
                Console.WriteLine("4. Material löschen");
                Console.WriteLine("5. Benutzer anzeigen");
                Console.WriteLine("6. Benutzer hinzufügen");
                Console.WriteLine("7. Benutzer löschen");
                Console.WriteLine("8. Logout");
                Console.Write("Auswahl: ");

                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        lager.ZeigeMaterial();
                        break;
                    case "2":
                        Console.Write("Name: ");
                        var name = Console.ReadLine();
                        Console.Write("Lagerplatz: ");
                        var lp = Console.ReadLine();
                        Console.Write("Artikelnummer: ");
                        var an = Console.ReadLine();
                        Console.Write("Bestand: ");
                        var bestand = int.Parse(Console.ReadLine() ?? "0");
                        lager.NeuesMaterialHinzu(name, lp, an, bestand);
                        break;
                    case "3":
                        lager.BearbeiteMaterial();
                        break;
                    case "4":
                        lager.LoescheMaterial();
                        break;
                    case "5":
                        Console.Clear();
                        foreach (var b in AlleMitarbeiter.BenutzerListe)
                            Console.WriteLine($"{b.Username} (Admin: {b.IstAdmin})");
                        Console.ReadKey();
                        break;
                    case "6":
                        Console.Write("Username: ");
                        var u = Console.ReadLine();
                        Console.Write("Passwort: ");
                        var p = Console.ReadLine();
                        AlleMitarbeiter.BenutzerListe.Add(new AlleMitarbeiter(u, p, false));
                        AlleMitarbeiter.SpeichereDaten();
                        break;
                    case "7":
                        Console.Clear();
                        for (int i = 0; i < AlleMitarbeiter.BenutzerListe.Count; i++)
                            Console.WriteLine($"{i + 1}. {AlleMitarbeiter.BenutzerListe[i].Username} (Admin:{AlleMitarbeiter.BenutzerListe[i].IstAdmin})");
                        Console.Write("Nummer: ");
                        if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= AlleMitarbeiter.BenutzerListe.Count)
                        {
                            AlleMitarbeiter.BenutzerListe.RemoveAt(idx - 1);
                            AlleMitarbeiter.SpeichereDaten();
                        }
                        break;
                    case "8":
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("\aUngültige Eingabe!");
                        Console.ResetColor();
						Console.ReadKey();
                        break;
                }
            }
        }

        static void MitarbeiterMenu(AlleMitarbeiter user)
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine($"<<<<<><><><>>>>> Mitarbeiter Bereich ({user.Username}) <<<<<><><><>>>>>");
                Console.WriteLine("---------------------------------------------------------------------------");
                Console.ResetColor();
				Console.WriteLine("\n1. Material anzeigen");
                Console.WriteLine("2. Zum Warenkorb hinzufügen");
                Console.WriteLine("3. Warenkorb anzeigen / Checkout");
                Console.WriteLine("4. Logout");
                Console.Write("\nAuswahl: ");

                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        lager.ZeigeMaterial();
                        Console.WriteLine("\nDücke Enter um Fortzufahren" );
                        Console.ReadKey();
						break;
                    case "2":
                        lager.ZeigeMaterial();
                        Console.Write("Nummer wählen: ");
                        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= Material.MaterialListe.Count)
                        {
                            var mat = Material.MaterialListe[index - 1];
                            Console.Write("Anzahl: ");
                            if (int.TryParse(Console.ReadLine(), out int anzahl))
                            {
                                user.FuegeZuWarenkorb(mat, anzahl);
                                AlleMitarbeiter.SpeichereDaten();
                                Console.WriteLine("Material in den Warenkorb gelegt.");
                                Console.ReadKey();
                            }
                        }
                        break;
                    case "3":
                        Console.Clear();
                        if (user.Warenkorb.Items.Count == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
							Console.WriteLine("\aWarenkorb ist leer.");
                            Console.ResetColor();
							Console.ReadKey();
                            break;
                        }
                        foreach (var pos in user.Warenkorb.Items)
                        {
                            var mat = Material.MaterialListe.FirstOrDefault(m => m.Artikelnummer == pos.Artikelnummer);
                            Console.WriteLine($"{mat?.Bezeichnung ?? "Unbekannt"} - Menge: {pos.Menge}");
                        }
                        Console.WriteLine("1. Checkout");
                        Console.WriteLine("2. Zurück");
                        var c = Console.ReadLine();
                        if (c == "1")
                        {
                            user.Warenkorb.Checkout(Material.MaterialListe);
                            Material.SpeichereDaten();
                            AlleMitarbeiter.SpeichereDaten();
                        }
                        break;
                    case "4":
                        return;
                }
            }
        }
    }
}