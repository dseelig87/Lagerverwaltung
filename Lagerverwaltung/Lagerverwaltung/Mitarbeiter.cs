using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Text.Json;

namespace Lagerverwaltung
{
	internal class Mitarbeiter : AlleMitarbeiter
	{
		public Mitarbeiter(string username, string password) : base(username, password)
		{
			
		}
	}

}
