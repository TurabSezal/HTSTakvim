using HTSTakvim.Models;


namespace HTSTakvim.App_Classes
{
    public class Context
    {
        private static Entities baglanti;

        public static Entities Baglanti
        {
            get
            {
                if (baglanti == null)
                    baglanti = new Entities();
                return baglanti;
            }
            set { baglanti = value; }
        }

    }
}