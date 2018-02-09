using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warframe.Market.Model
{
    public class Item
    {
        public string item_name { get; set; }

        // Don't really care
        public string item_type { get; set; }

        //// Same as wiki_link?
        public string item_wiki { get; set; }

        //public string category { get; set; }
        //// Don't really care
        public int mod_max_rank { get; set; }

        //// Don't really care
        public string rarity { get; set; }

        //// Could be useful
        public string wiki_link { get; set; }

        public override string ToString()
        {
            return item_name;
        }
    }
}
