using System.ComponentModel;

namespace Warframe.Market.Model
{
    public class Sell
    {
        public bool online_ingame { get; set; }

        public bool online_status { get; set; }

        [DisplayName("User")]
        public string ingame_name { get; set; }

        [DisplayName("Price")]
        public int price { get; set; }

        [DisplayName("Count")]
        public int count { get; set; }
    }
}