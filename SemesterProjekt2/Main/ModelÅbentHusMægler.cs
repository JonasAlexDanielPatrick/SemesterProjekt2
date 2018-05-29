using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class ModelÅbentHusMægler
    {
        public int Id { get; }
        public String Navn { get; }

        public ModelÅbentHusMægler(int id, string navn)
        {
            Id = id;
            Navn = navn;
        }

    }
}
