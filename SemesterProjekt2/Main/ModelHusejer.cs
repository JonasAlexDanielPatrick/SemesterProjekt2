using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class ModelHusejer
    {
        public int ID { get; }
        public string Navn { get; }
        public string Email { get; }
        public string Telefon { get; }


        public ModelHusejer(int id, string navn, string email, string telefon)
        {
            ID = id;
            Navn = navn;
            Email = email; 
            Telefon = telefon;
        }

    }
}
