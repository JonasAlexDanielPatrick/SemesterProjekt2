
namespace Models
// Skrevet af Jonas
{
    class ModelTestDataOmråde
    {
        public string Navn { get; }
        public int Postnr { get; }
        public string By { get; }
        public float PrisFaktor { get; }

        public ModelTestDataOmråde(string navn, int postnr, string by, float prisfaktor)
        {
            Navn = navn;
            Postnr = postnr;
            By = by;
            PrisFaktor = prisfaktor;
        }

    }
}
