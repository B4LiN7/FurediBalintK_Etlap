using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etlap
{
    public class Etel
    {
        public int Id { get; set; }
        public string Nev { get; set; }
        public string Leiras { get; set; }
        public int Ar { get; set; }
        public string Kategoria { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Nev} ({Leiras}) (Kategória: {Kategoria}) {Ar} Ft";
        }
    }
}
