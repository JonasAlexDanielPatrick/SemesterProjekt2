using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class ModelÅbentHusMægler
    {
        public int ID { get; }
        public String Navn { get; }
        public bool IsChecked { get; set; }

        public ModelÅbentHusMægler(int id, string navn)
        {
            ID = id;
            Navn = navn;
            IsChecked = false;
        }

    }
}
