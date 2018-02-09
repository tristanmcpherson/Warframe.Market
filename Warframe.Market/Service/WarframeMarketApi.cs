using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Warframe.Market.Model;

namespace Warframe.Market.Service
{
    public class WarframeMarketApi
    {
        public const string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36";

        public const string BaseUrl = "http://warframe.market";

        public readonly FlurlClient Client;

        public WarframeMarketApi()
        {
            Client = new FlurlClient(c => { c.CookiesEnabled = true; });
            Client.WithHeader("UserAgent", UserAgent);
        }

        public async Task Login(string token)
        {
            Client.WithHeader("remember_token", token);

            await BaseUrl
                .WithClient(Client)
                .GetAsync();
        }

        public async Task<List<Item>> GetItems()
        {
            var items = await BaseUrl.
                AppendPathSegments("api", "get_all_items_v2")
                .WithClient(Client)
                .GetJsonAsync<List<Item>>();

            return items.ToList();
        }

        public async Task<Response> GetListingForItem(Item item)
        {
            return (await BaseUrl.
                AppendPathSegments("api", "get_orders", item.item_type, item.item_name)
                .WithClient(Client)
                .GetJsonAsync<Rootobject>()).response;
        }

        public async Task GetLowestSell(string itemName)
        {

        }
    }
}