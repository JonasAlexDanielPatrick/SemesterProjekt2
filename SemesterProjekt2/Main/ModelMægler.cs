using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class ModelMægler
    {
        public int ID { get; }
        public string Navn { get; }
        public string Email { get; }
        public string Telefon { get; }


        public ModelMægler(int id, string navn, string email, string telefon)
        {
            ID = id;
            Navn = navn;
            Email = email;
            Telefon = telefon;
        }

    }
}
