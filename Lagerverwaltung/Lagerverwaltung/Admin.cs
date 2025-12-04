using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Lagerverwaltung
{
    internal class Admin : AlleMitarbeiter
    {
        public Admin(string username, string password) : base(username, password)
        {
            IstAdmin = true;
        }
    }
}
