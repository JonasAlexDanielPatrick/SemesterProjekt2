using System;

namespace Models
{
    class ModelÅbentHusMægler
    // Skrevet af Jonas
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
