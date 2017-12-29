using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            Version,
            Tournaments
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

        /// <summary>
        /// Search for clans and get array of clan info. You need to pass AT LEAST one
        /// parameter. If you do not want to pass some parameter, enter NULL instead
        /// </summary>
        /// <param name="name">Name to search. If you do not want to input this one, enter NULL</param>
        /// <param name="score">Minimum clan score. If you do not want to input this one, enter NULL</param>
        /// <param name="minMembers">Minimum members in clan. 0-50. If you do not want to input this one, enter NULL</param>
        /// <param name="maxMembers">Maximum members in clan. 0-60. If you do not want to input this one, enter NULL</param>
        /// <returns></returns>
        public SimplifiedClan[] SearchForClans(string name, int? score, int? minMembers, int? maxMembers)
        {
            List<string> queries = new List<string>(4);

            if (name != null)
                if (name.Length < 3)
                    throw new ArgumentException("Parameter must contain at least 3 characters. If you do not want to search using this parameter, pass NULL instead.", "name");

            if (minMembers != null)
                if (minMembers < 0 || minMembers > 50)
                    throw new ArgumentOutOfRangeException("Parameter must be in range 0-50. Value: " + minMembers, "minMembers");

            if (maxMembers != null)
                if (maxMembers < 0 || maxMembers > 60)
                    throw new ArgumentOutOfRangeException("Parameter must be in range 0-60. Value: " + maxMembers, "maxMembers");


            if (name != null)
                queries.Add("name=" + name);
            if (score != null)
                queries.Add("score=" + score);
            if (minMembers != null)
                queries.Add("minMembers=" + minMembers);
            if (maxMembers != null)
                queries.Add("maxMembers=" + maxMembers);

            if (queries.Count == 0)
                throw new ArgumentException("At least one parameter must be not-null!");

            string q = "?" + queries[0]; ;

            for (int i = 1; i < queries.Count; i++)
                q += "&" + queries[i];

            string output = Get(Endpoints.Clan, "search" + q);
            return Parse<SimplifiedClan[]>(output);
        }

        public Tournament GetTournament(string tag)
        {
            string output = Get(Endpoints.Tournaments, tag);
            return Parse<Tournament>(output);
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

        public async Task<SimplifiedClan[]> SearchForClansAsync(string name, int? score, int? minMembers, int? maxMembers)
        {
            List<string> queries = new List<string>(4);

            if (name != null)
                if (name.Length < 3)
                    throw new ArgumentException("Parameter must contain at least 3 characters. If you do not want to search using this parameter, pass NULL instead.", "name");

            if (minMembers != null)
                if (minMembers < 0 || minMembers > 50)
                    throw new ArgumentOutOfRangeException("Parameter must be in range 0-50. Value: " + minMembers, "minMembers");

            if (maxMembers != null)
                if (maxMembers < 0 || maxMembers > 60)
                    throw new ArgumentOutOfRangeException("Parameter must be in range 0-60. Value: " + maxMembers, "maxMembers");


            if (name != null)
                queries.Add("name=" + name);
            if (score != null)
                queries.Add("score=" + score);
            if (minMembers != null)
                queries.Add("minMembers=" + minMembers);
            if (maxMembers != null)
                queries.Add("maxMembers=" + maxMembers);

            if (queries.Count == 0)
                throw new ArgumentException("At least one parameter must be not-null!");

            string q = "?" + queries[0]; ;

            for (int i = 1; i < queries.Count; i++)
                q += "&" + queries[i];

            Task<string> output = GetAsync(Endpoints.Clan, "search" + q);
            return Parse<SimplifiedClan[]>(await output);
        }

        public async Task<Tournament> GetTournamentAsync(string tag)
        {
            Task<string> output = GetAsync(Endpoints.Tournaments, tag);
            return Parse<Tournament>(await output);
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
