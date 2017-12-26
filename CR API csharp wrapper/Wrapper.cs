using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace CRAPI
{
    public class Wrapper
    {
        private const string domain = "http://api.cr-api.com/";

        private readonly string key;


        /// <summary>
        /// All current API endpoints
        /// </summary>
        public enum Endpoints
        {
            Player,
            Top,
            Clan,
            Constants,
            Version
        }


        public Wrapper(string devkey)
        {
            this.key = devkey;
        }

        #region GetFromAPI

        public Player GetPlayer(string tag)
        {
            string output = Get(Endpoints.Player, tag);
            return Parse<Player>(output);
        }

        public Clan GetClan(string tag)
        {
            string output = Get(Endpoints.Clan, tag);
            return Parse<Clan>(output);
        }

        public SimplifiedPlayer[] GetTopPlayers()
        {
            string output = Get(Endpoints.Top, "players");
            return Parse<SimplifiedPlayer[]>(output);
        }

        public SimplifiedClan[] GetTopClans()
        {
            string output = Get(Endpoints.Top, "clans");
            return Parse<SimplifiedClan[]>(output);
        }

        #endregion

        #region GetFromAPIasync

        public async Task<Player> GetPlayerAsync(string tag)
        {
            Task<string> output = GetAsync(Endpoints.Player, tag);
            return Parse<Player>(await output);
        }

        public async Task<Clan> GetClanAsync(string tag)
        {
            Task<string> output = GetAsync(Endpoints.Clan, tag);
            return Parse<Clan>(await output);
        }

        public async Task<SimplifiedPlayer[]> GetTopPlayersAsync()
        {
            Task<string> output = GetAsync(Endpoints.Top, "players");
            return Parse<SimplifiedPlayer[]>(await output);
        }

        public async Task<SimplifiedClan[]> GetTopClansAsync()
        {
            Task<string> output = GetAsync(Endpoints.Top, "clans");
            return Parse<SimplifiedClan[]>(await output);
        }

        #endregion

        /// <summary>
        /// Get direct output from CR API
        /// </summary>
        /// <param name="endpoint">Endpoint to use</param>
        /// <param name="parameter">Parameter to endpoint (like player ID)</param>
        /// <returns></returns>
        private string Get(Endpoints endpoint, string parameter)
        {
            return Get(domain + endpoint.ToString() + "/" + parameter);
        }

        private async Task<string> GetAsync(Endpoints endpoint, string parameter)
        {
            Task<string> tReq = GetAsync(domain + endpoint.ToString() + "/" + parameter);
            return await tReq;
        }

        private string Get(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            req.Headers.Add("auth", key);
            WebResponse myResponse = req.GetResponse();
            StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
            string result = sr.ReadToEnd();
            sr.Close();
            myResponse.Close();

            return result;
        }

        private async Task<string> GetAsync(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            req.Headers.Add("auth", key);
            Task<WebResponse> myResponse = req.GetResponseAsync();
            WebResponse response = await myResponse;
            StreamReader sr = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8);
            string result = sr.ReadToEnd();
            sr.Close();
            response.Close();

            return result;
        }

        /// <summary>
        /// Deserialize JSON
        /// </summary>
        private static T Parse<T>(string input) where T : class
        {
            return JsonConvert.DeserializeObject<T>(input);
        }
    }
}
