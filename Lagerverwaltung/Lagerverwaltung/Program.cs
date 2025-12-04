using System.Text.Json;
using System.Xml;




namespace Lagerverwaltung
{
    internal class Program
    {
        static public string filePath = "Warenkorb.json";

		static Lager großesLager = new Lager();

		
        
        
        
        static void Main(string[] args)
        {
            bool laufend = true;

            //while (laufend)
            {

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("                             <><><><><><><><> Lagerverwaltung <><><><><><><><>");
                Console.WriteLine("--------------------------------------------------------------------------------------------------------");
                Console.ResetColor();
                Console.WriteLine("\n1. Login");      
                Console.WriteLine("\n2. Beenden");
                Console.WriteLine("______________________________________________________________________");
                Console.Write("Bitte wählen Sie eine Option: ");


			}
			

            
		}
    }
}