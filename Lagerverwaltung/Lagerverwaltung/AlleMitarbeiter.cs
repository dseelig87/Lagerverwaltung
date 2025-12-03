using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagerverwaltung
{
    abstract class AlleMitarbeiter
    {
        public string Mitarbeiter { get; }
		public string Passwort { get; }
        public abstract bool IstAdmin { get; }
		private List<Material> warenkorb = new List<Material>();

		

        public AlleMitarbeiter(string mitarbeiter, string passwort)
        {
            Mitarbeiter = mitarbeiter;
            Passwort = passwort;
        }

        public void FuegeZuWarenkorbHinzu(Material material)
        {
            warenkorb.Add(material);
        }

        public void EntferneAusWarenkorb(Material material)
        {
            warenkorb.Remove(material);
        }

        public List<Material> WarenkorbAnzeigen()
        {
            return warenkorb;
        }

        public void LeereWarenkorb()
        {
            warenkorb.Clear();

        } 

        public void ZeigeWarenkorbInhalt()
        {
            if (warenkorb.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;

				Console.WriteLine("============> Der Warenkorb ist leer. <==============");
                Console.ResetColor();
			}
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("Inhalt des Warenkorbs:");
                Console.ResetColor();
				foreach (var material in warenkorb)
                {
                    Console.WriteLine($"- {material.Bezeichnug} (Artikelnummer: {material.Artikelnummer}, Menge: {material.Menge})");
                }
            }

            Console.ReadKey();
		}
	}   

}

