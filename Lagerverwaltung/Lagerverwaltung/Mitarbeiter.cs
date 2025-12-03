using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagerverwaltung
{
     class Mitarbeiter : AlleMitarbeiter
    {
		public string Mitarbeitername { get; set; }
		public string Passwort { get; set; }
		public override bool IstAdmin => false;

		

		public Mitarbeiter(string mitarbeitername, string passwort) : base(mitarbeitername, passwort)
		{
			Mitarbeitername = mitarbeitername;
			Passwort = passwort;

		}	
		
			
		
		
	}

}
