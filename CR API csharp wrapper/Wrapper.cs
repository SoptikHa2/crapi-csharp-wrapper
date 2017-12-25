using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace CRAPI
{
    /// <summary>
    /// Class that is used by this wrapper. Do not touch!
    /// </summary>
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
        
        public ClanPlayer[] GetTopPlayers()
        {
            string output = Get(Endpoints.Top, "players");
            return Parse<ClanPlayer[]>(output);
        }

        public PlayerClan[] GetTopClans()
        {
            string output = Get(Endpoints.Top, "clans");
            return Parse<PlayerClan[]>(output);
        }

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

        /// <summary>
        /// Deserialize JSON
        /// </summary>
        private static T Parse<T>(string input) where T : class
        {
            return JsonConvert.DeserializeObject<T>(input);
        }
    }
}
