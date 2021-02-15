using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CS_Node_App.Models;
using Newtonsoft.Json;

namespace CS_Node_App.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        public class Node
        {
            public string ip { get; set; }
            public string publicKey { get; set; }
            public string country { get; set; }
            public string countryName { get; set; }
            public string city { get; set; }
            public string version { get; set; }
            public int platform { get; set; }
            public int countTrust { get; set; }
            public DateTime timeRegistration { get; set; }
            public int timeActive { get; set; }
            public double latitude { get; set; }
            public double longitude { get; set; }
            public bool active { get; set; }
            public string totalFee { get; set; }
            public int timesWriter { get; set; }
        }

        public class Root
        {
            public int page { get; set; }
            public List<Node> nodes { get; set; }
            public int onlineCount { get; set; }
            public int offlineCount { get; set; }
            public bool haveNextPage { get; set; }
            public int lastPage { get; set; }
            public string numStr { get; set; }
        }
        readonly List<Item> items;

        public  MockDataStore()
        {
            
            items = new List<Item>()
            {
                /*
                new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." }*/
            };
            setitem();

        }

        public async Task setitem()
        {
            var data = await fetchtotallist_Nodes();
            var nodes = data.Item2;
            foreach(var x in nodes)
            {
                var k = new Item()
                {
                    Id = Guid.NewGuid().ToString(),
                    Text = x.publicKey,
                    Description = x.country,
                    Iso2 = x.country
                    
                };
                items.Add(k);

            }
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public static async Task<(bool, List<Node>)> fetchtotallist_Nodes()
        {
            try
            {
                var url = "https://monitor.credits.com/CreditsNetwork/Api/GetNodesData?page=1&limit=100";
                var result_data = _download_serialized_json_data<Root>(url);
                var nodelist = result_data.nodes;
                var currentpage = 1;
                if (result_data.haveNextPage)
                {
                    currentpage = 2;
                    if (result_data.lastPage - 1 > 0)
                    {
                        foreach (int value in Enumerable.Range(1, result_data.lastPage - 1))
                        {
                            url = "https://monitor.credits.com/CreditsNetwork/Api/GetNodesData?page=" + currentpage + "&limit=100";
                            nodelist.AddRange(_download_serialized_json_data<Root>(url).nodes);
                            currentpage = currentpage + 1;
                        }

                    }
                    else
                    {
                        url = "https://monitor.credits.com/CreditsNetwork/Api/GetNodesData?page=2&limit=100";
                        nodelist.AddRange(_download_serialized_json_data<Root>(url).nodes);
                    }


                }

                return (true, nodelist);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var nodelist = new List<Node>();
                return (false, nodelist);
            }
        }
        private static T _download_serialized_json_data<T>(string url) where T : new()
        {
            using (var w = new WebClient())
            {
                var json_data = string.Empty;
                // attempt to download JSON data as a string
                try
                {
                    json_data = w.DownloadString(url);
                }
                catch (Exception) { }
                // if string with JSON data is not empty, deserialize it to class and return its instance 
                return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<T>(json_data) : new T();
            }
        }
    }
}