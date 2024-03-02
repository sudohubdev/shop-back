//call http request to https://sudohub.dev/api/discover
using System.Text;
using Microsoft.AspNetCore.Mvc;
using MvcProxy.Models;

namespace shop_back.Server.Services
{
    public static class DiscoverService
    {
        private static string APIKEYPATH = "/https/apikey";
        private static string APIKEY
        {
            get
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    throw new Exception("SudoHub discover isn't available in development mode");
                }
                string key = File.ReadAllText(APIKEYPATH);
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(key));
            }
        }
        public static async Task<IActionResult> GetDiscover()
        {
            DiscoverModel discover = new DiscoverModel();
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + APIKEY);
            //send post json
            var response = await client.PostAsJsonAsync("https://sudohub.dev/api/Discover", discover);
            //get response
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return new OkObjectResult(result);
            }
            else
            {
                return new BadRequestObjectResult("Failed to get Discover");
            }
        }
        //start an infinite loop to keep the discover updated every 10 seconds
        public static async Task StartDiscover()
        {
            while (true)
            {
                try
                {
                    await GetDiscover();
                    await Task.Delay(10000);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
