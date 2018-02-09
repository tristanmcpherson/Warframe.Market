namespace Warframe.Market.Model
{
    public class Buy
    {
        public bool online_ingame { get; set; }
        public bool online_status { get; set; }
        public string ingame_name { get; set; }
        public int price { get; set; }
        public int count { get; set; }
    }
}