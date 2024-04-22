using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Etelek
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            List<Leves> lev = new List<Leves>();
            lev=await etelek();
            Console.WriteLine($"{lev.Count()} leves található");
            int maxKaloria = int.MinValue;
            foreach (Leves levesek in lev)
            {
                if (levesek.Kaloria > maxKaloria)
                {
                    maxKaloria = (int)levesek.Kaloria;
                    await Console.Out.WriteLineAsync($"{levesek.Megnevezes} rendelkezik a legnagyobb kalóriával");
                }
            }
            Console.ReadKey();
        }
        private static async Task<List<Leves>> etelek()
        {
            List<Leves> lev = new List<Leves>();
            string url = $"https://retoolapi.dev/Kc6xuH/data";
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                lev = Leves.FromJson(jsonString).ToList();
            }
            return lev;
        }
    }
}
