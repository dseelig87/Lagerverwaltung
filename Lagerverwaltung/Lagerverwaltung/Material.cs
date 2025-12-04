using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagerverwaltung
{
    internal class Material
    {
        public string Name { get; set; }
        public string Artikelnummer { get; set; }
        public string Lagerplatz { get; set; }
		public int Menge { get; set; }
        public int Bestand { get; set; }    

		public Material(string name, string artikelnummer,string lagerplatz, int menge, int bestand)
        {
            Name = name;
            Artikelnummer = artikelnummer;
            Lagerplatz = lagerplatz;
			Menge = menge;
            Bestand = bestand;
		}
	}
}
