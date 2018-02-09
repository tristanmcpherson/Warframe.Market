namespace Warframe.Market.Model
{
    public class Response
    {
        public Sell[] sell { get; set; }
        public Buy[] buy { get; set; }
        public bool render_rank { get; set; }
    }
}