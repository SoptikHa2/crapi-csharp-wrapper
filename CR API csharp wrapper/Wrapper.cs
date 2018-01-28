using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace CRAPI
{
    public class Wrapper
    {
        private const string domain = "http://api.cr-api.com/";

        private readonly string key;
        private Cacher cache;

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

        /// <summary>
        /// All locations that can be used in TopClans. Select "None" to ignore location. _EU is Europe, _NA is North America, _SA is South America, _AS is Asia, _AU is Australia, _AF is Africa, _INT is International.
        /// Full list can be seen here: https://raw.githubusercontent.com/cr-api/cr-api-data/master/json/regions.json
        /// </summary>
        public enum Locations
        {
            None, _EU, _NA, _SA, _AS, _AU, _AF, _INT, AF, AX, AL, DZ, AS, AD, AO, AI, AQ, AG, AR, AM, AW, AC, AU, AT, AZ, BS, BH, BD, BB, BY, BE, BZ, BJ, BM, BT, BO, BA, BW, BV, BR, IO, VG, BN, BG, BF, BI, KH, CM, CA, IC, CV, BQ, KY, CF, EA, TD, CL, CN, CX, CC, CO, KM, CG, CD, CK, CR, CI, HR, CU, CW, CY, CZ, DK, DG, DJ, DM, DO, EC, EG, SV, GQ, ER, EE, ET, FK, FO, FJ, FI, FR, GF, PF, TF, GA, GM, GE, DE, GH, GI, GR, GL, GD, GP, GU, GT, GG, GN, GW, GY, HT, HM, HN, HK, HU, IS, IN, ID, IR, IQ, IE, IM, IL, IT, JM, JP, JE, JO, KZ, KE, KI, XK, KW, KG, LA, LV, LB, LS, LR, LY, LI, LT, LU, MO, MK, MG, MW, MY, MV, ML, MT, MH, MQ, MR, MU, YT, MX, FM, MD, MC, MN, ME, MS, MA, MZ, MM, NA, NR, NP, NL, NC, NZ, NI, NE, NG, NU, NF, KP, MP, NO, OM, PK, PW, PS, PA, PG, PY, PE, PH, PN, PL, PT, PR, QA, RE, RO, RU, RW, BL, SH, KN, LC, MF, PM, WS, SM, ST, SA, SN, RS, SC, SL, SG, SX, SK, SI, SB, SO, ZA, KR, SS, ES, LK, VC, SD, SR, SJ, SZ, SE, CH, SY, TW, TJ, TZ, TH, TL, TG, TK, TO, TT, TA, TN, TR, TM, TC, TV, UM, VI, UG, UA, AE, GB, US, UY, UZ, VU, VA, VE, VN, WF, EH, YE, ZM, ZW
        }

        /// <summary>
        /// Initialize wrapper for CR API
        /// </summary>
        /// <param name="devkey">Private developer key</param>
        /// <param name="cacheDuration">Wrapper cache duration in minutes. 0 or less = no cache</param>
        public Wrapper(string devkey, int cacheDuration = 1)
        {
            this.key = devkey;
            this.cache = new Cacher(cacheDuration);
        }

        #region GetFromAPI

        /// <summary>
        /// Get instance of Player from API
        /// </summary>
        /// <param name="tag">Player's tag. Must be upper-case</param>
        /// <param name="include">Optional parameter, may be null. Specifies fields to be included in response. Everything else is dropped. This parameter and/or [exclude] parameter must be NULL.</param>
        /// <param name="exclude">Optional parameter, may be null. Specifies fields to be dropped from response. Everything else is delivered. This parameter and/or [include] parameter must be NULL.</param>
        /// <returns></returns>
        public Player GetPlayer(string tag, string[] include = null, string[] exclude = null)
        {
            string query = String.Empty;
            if (include != null && exclude != null)
                throw new ArgumentException("At least one of parameters (include, exclude) must be NULL", "include, exclude");
            if (include != null)
                query += "?keys=" + String.Join(",", include);
            if (exclude != null)
                query += "?exclude=" + String.Join(",", exclude);
            string output = Get(Endpoints.Player, tag + query);
            return Parse<Player>(output);
        }

        /// <summary>
        /// Get instatnce of Clan from API
        /// </summary>
        /// <param name="tag">Clan tag. Must be upper-case</param>
        /// <param name="include">Optional parameter, may be null. Specifies fields to be included in response. Everything else is dropped. This parameter and/or [exclude] parameter must be NULL.</param>
        /// <param name="exclude">Optional parameter, may be null. Specifies fields to be dropped from response. Everything else is delivered. This parameter and/or [include] parameter must be NULL.</param>
        /// <returns></returns>
        public Clan GetClan(string tag, string[] include = null, string[] exclude = null)
        {
            string query = String.Empty;
            if (include != null && exclude != null)
                throw new ArgumentException("At least one of parameters (include, exclude) must be NULL", "include, exclude");
            if (include != null)
                query += "?keys=" + String.Join(",", include);
            if (exclude != null)
                query += "?exclude=" + String.Join(",", exclude);
            string output = Get(Endpoints.Clan, tag + query);
            return Parse<Clan>(output);
        }

        /// <summary>
        /// Get top players in clash royale. Returned players are instances of SimplifiedPlayer, thus contain only basic information
        /// </summary>
        /// <param name="include">Optional parameter, may be null. Specifies fields to be included in response. Everything else is dropped. This parameter and/or [exclude] parameter must be NULL.</param>
        /// <param name="exclude">Optional parameter, may be null. Specifies fields to be dropped from response. Everything else is delivered. This parameter and/or [include] parameter must be NULL.</param>
        /// <returns></returns>
        public SimplifiedPlayer[] GetTopPlayers(string[] include = null, string[] exclude = null)
        {
            string query = String.Empty;
            if (include != null && exclude != null)
                throw new ArgumentException("At least one of parameters (include, exclude) must be NULL", "include, exclude");
            if (include != null)
                query += "?keys=" + String.Join(",", include);
            if (exclude != null)
                query += "?exclude=" + String.Join(",", exclude);
            string output = Get(Endpoints.Top, "players" + query);
            return Parse<SimplifiedPlayer[]>(output);
        }

        /// <summary>
        /// Get top clans in clash royale. Returned clans are instances of SimplifiedClan, thus contain only basic information
        /// </summary>
        /// <param name="include">Optional parameter, may be null. Specifies fields to be included in response. Everything else is dropped. This parameter and/or [exclude] parameter must be NULL.</param>
        /// <param name="exclude">Optional parameter, may be null. Specifies fields to be dropped from response. Everything else is delivered. This parameter and/or [include] parameter must be NULL.</param>
        /// <param name="region">Optional parameter, default value is Locations.None. Specifies region, only clans from selected region will be returned. Select Locations.None to return clans from whole world.</param>
        /// <returns></returns>
        public SimplifiedClan[] GetTopClans(string[] include = null, string[] exclude = null, Locations region = Locations.None)
        {
            string query = String.Empty;
            if (include != null && exclude != null)
                throw new ArgumentException("At least one of parameters (include, exclude) must be NULL", "include, exclude");
            if (include != null)
                query += "?keys=" + String.Join(",", include);
            if (exclude != null)
                query += "?exclude=" + String.Join(",", exclude);
            string output = Get(Endpoints.Top, region != Locations.None ? "clans/" + region.ToString() + query : "clans" + query);
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
        /// <param name="include">Optional parameter, may be null. Specifies fields to be included in response. Everything else is dropped. This parameter and/or [exclude] parameter must be NULL.</param>
        /// <param name="exclude">Optional parameter, may be null. Specifies fields to be dropped from response. Everything else is delivered. This parameter and/or [include] parameter must be NULL.</param>
        /// <returns></returns>
        public SimplifiedClan[] SearchForClans(string name, int? score, int? minMembers, int? maxMembers, string[] include = null, string[] exclude = null)
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

            string q = String.Empty;
            q += "?" + queries[0];
            for (int i = 1; i < queries.Count; i++)
                q += "&" + queries[i];

            string query = String.Empty;
            if (include != null && exclude != null)
                throw new ArgumentException("At least one of parameters (include, exclude) must be NULL", "include, exclude");
            if (include != null)
                query += "?keys=" + String.Join(",", include);
            if (exclude != null)
                query += "?exclude=" + String.Join(",", exclude);

            string output = Get(Endpoints.Clan, "search" + query + q);
            return Parse<SimplifiedClan[]>(output);
        }

        /// <summary>
        /// Get instance of Tournament
        /// </summary>
        /// <param name="tag">TAG of tournament</param>
        /// <param name="include">Optional parameter, may be null. Specifies fields to be included in response. Everything else is dropped. This parameter and/or [exclude] parameter must be NULL.</param>
        /// <param name="exclude">Optional parameter, may be null. Specifies fields to be dropped from response. Everything else is delivered. This parameter and/or [include] parameter must be NULL.</param>
        /// <returns></returns>
        public Tournament GetTournament(string tag, string[] include = null, string[] exclude = null)
        {
            string query = String.Empty;
            if (include != null && exclude != null)
                throw new ArgumentException("At least one of parameters (include, exclude) must be NULL", "include, exclude");
            if (include != null)
                query += "?keys=" + String.Join(",", include);
            if (exclude != null)
                query += "?exclude=" + String.Join(",", exclude);

            string output = Get(Endpoints.Tournaments, tag + query);
            return Parse<Tournament>(output);
        }

        #endregion

        #region GetFromAPIasync

        /// <summary>
        /// Get instance of Player from API async
        /// </summary>
        /// <param name="tag">Player's tag. Must be upper-case</param>
        /// <param name="include">Optional parameter, may be null. Specifies fields to be included in response. Everything else is dropped. This parameter and/or [exclude] parameter must be NULL.</param>
        /// <param name="exclude">Optional parameter, may be null. Specifies fields to be dropped from response. Everything else is delivered. This parameter and/or [include] parameter must be NULL.</param>
        /// <returns></returns>
        public async Task<Player> GetPlayerAsync(string tag, string[] include = null, string[] exclude = null)
        {
            string query = String.Empty;
            if (include != null && exclude != null)
                throw new ArgumentException("At least one of parameters (include, exclude) must be NULL", "include, exclude");
            if (include != null)
                query += "?keys=" + String.Join(",", include);
            if (exclude != null)
                query += "?exclude=" + String.Join(",", exclude);
            Task<string> output = GetAsync(Endpoints.Player, tag + query);
            return Parse<Player>(await output);
        }

        /// <summary>
        /// Get instance of Clan from API async
        /// </summary>
        /// <param name="tag">Clan tag. Must be upper-case</param>
        /// <param name="include">Optional parameter, may be null. Specifies fields to be included in response. Everything else is dropped. This parameter and/or [exclude] parameter must be NULL.</param>
        /// <param name="exclude">Optional parameter, may be null. Specifies fields to be dropped from response. Everything else is delivered. This parameter and/or [include] parameter must be NULL.</param>
        /// <returns></returns>
        public async Task<Clan> GetClanAsync(string tag, string[] include = null, string[] exclude = null)
        {
            Task<string> output = GetAsync(Endpoints.Clan, tag);
            return Parse<Clan>(await output);
        }

        /// <summary>
        /// Get top players in clash royale async. Returned players are instances of SimplifiedPlayer, thus contain only basic information
        /// </summary>
        /// <param name="include">Optional parameter, may be null. Specifies fields to be included in response. Everything else is dropped. This parameter and/or [exclude] parameter must be NULL.</param>
        /// <param name="exclude">Optional parameter, may be null. Specifies fields to be dropped from response. Everything else is delivered. This parameter and/or [include] parameter must be NULL.</param>
        /// <returns></returns>
        public async Task<SimplifiedPlayer[]> GetTopPlayersAsync(string[] include = null, string[] exclude = null)
        {
            string query = String.Empty;
            if (include != null && exclude != null)
                throw new ArgumentException("At least one of parameters (include, exclude) must be NULL", "include, exclude");
            if (include != null)
                query += "?keys=" + String.Join(",", include);
            if (exclude != null)
                query += "?exclude=" + String.Join(",", exclude);
            Task<string> output = GetAsync(Endpoints.Top, "players" + query);
            return Parse<SimplifiedPlayer[]>(await output);
        }

        /// <summary>
        /// Get top clans in clash royale async. Returned clans are instances of SimplifiedClan, thus contain only basic information
        /// </summary>
        /// <param name="include">Optional parameter, may be null. Specifies fields to be included in response. Everything else is dropped. This parameter and/or [exclude] parameter must be NULL.</param>
        /// <param name="exclude">Optional parameter, may be null. Specifies fields to be dropped from response. Everything else is delivered. This parameter and/or [include] parameter must be NULL.</param>
        /// <param name="region">Optional parameter, default value is Locations.None. Specifies region, only clans from selected region will be returned. Select Locations.None to return clans from whole world.</param>
        /// <returns></returns>
        public async Task<SimplifiedClan[]> GetTopClansAsync(string[] include = null, string[] exclude = null, Locations region = Locations.None)
        {
            string query = String.Empty;
            if (include != null && exclude != null)
                throw new ArgumentException("At least one of parameters (include, exclude) must be NULL", "include, exclude");
            if (include != null)
                query += "?keys=" + String.Join(",", include);
            if (exclude != null)
                query += "?exclude=" + String.Join(",", exclude);
            Task<string> output = GetAsync(Endpoints.Top, region == Locations.None ? "clans" + query : "clans/" + region.ToString() + query);
            return Parse<SimplifiedClan[]>(await output);
        }

        /// <summary>
        /// Search for clans and get array of clan info async. You need to pass AT LEAST one
        /// parameter. If you do not want to pass some parameter, enter NULL instead
        /// </summary>
        /// <param name="name">Name to search. If you do not want to input this one, enter NULL</param>
        /// <param name="score">Minimum clan score. If you do not want to input this one, enter NULL</param>
        /// <param name="minMembers">Minimum members in clan. 0-50. If you do not want to input this one, enter NULL</param>
        /// <param name="maxMembers">Maximum members in clan. 0-60. If you do not want to input this one, enter NULL</param>
        /// <param name="include">Optional parameter, may be null. Specifies fields to be included in response. Everything else is dropped. This parameter and/or [exclude] parameter must be NULL.</param>
        /// <param name="exclude">Optional parameter, may be null. Specifies fields to be dropped from response. Everything else is delivered. This parameter and/or [include] parameter must be NULL.</param>
        /// <returns></returns>
        public async Task<SimplifiedClan[]> SearchForClansAsync(string name, int? score, int? minMembers, int? maxMembers, string[] include = null, string[] exclude = null)
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

            string q = String.Empty;
            q += "?" + queries[0];
            for (int i = 1; i < queries.Count; i++)
                q += "&" + queries[i];

            string query = String.Empty;
            if (include != null && exclude != null)
                throw new ArgumentException("At least one of parameters (include, exclude) must be NULL", "include, exclude");
            if (include != null)
                query += "?keys=" + String.Join(",", include);
            if (exclude != null)
                query += "?exclude=" + String.Join(",", exclude);

            Task<string> output = GetAsync(Endpoints.Clan, "search" + query + q);
            return Parse<SimplifiedClan[]>(await output);
        }

        /// <summary>
        /// Get instance of Tournament async
        /// </summary>
        /// <param name="tag">TAG of tournament</param>
        /// <param name="include">Optional parameter, may be null. Specifies fields to be included in response. Everything else is dropped. This parameter and/or [exclude] parameter must be NULL.</param>
        /// <param name="exclude">Optional parameter, may be null. Specifies fields to be dropped from response. Everything else is delivered. This parameter and/or [include] parameter must be NULL.</param>
        /// <returns></returns>
        public async Task<Tournament> GetTournamentAsync(string tag, string[] include = null, string[] exclude = null)
        {
            string query = String.Empty;
            if (include != null && exclude != null)
                throw new ArgumentException("At least one of parameters (include, exclude) must be NULL", "include, exclude");
            if (include != null)
                query += "?keys=" + String.Join(",", include);
            if (exclude != null)
                query += "?exclude=" + String.Join(",", exclude);
            Task<string> output = GetAsync(Endpoints.Tournaments, tag + query);
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
            try
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
            catch (WebException e)
            {
                WebResponse wr = e.Response;

                // Cannot connect to api servers -> API servers are down or user is disconnected
                if (wr == null)
                    throw e;

                StreamReader sr = new StreamReader(wr.GetResponseStream());
                BadResponse returnedException = Parse<BadResponse>(sr.ReadToEnd()) as BadResponse;
                APIException exception = new APIException(returnedException.status, returnedException.message, returnedException.error);
                throw exception;
            }
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

    #region Cache

    internal class Cacher
    {
        private int cacheDuration;
        private int cacheCheckInterval;
        private Dictionary<CacheKey, CacheDetails> cache;
        private DateTime lastCacheCheck;

        public Cacher(int cacheDuration, int cacheCheckInterval = 5)
        {
            this.cacheDuration = cacheDuration;
            this.cacheCheckInterval = cacheCheckInterval;
            if (cacheDuration > 0)
            {
                cache = new Dictionary<CacheKey, CacheDetails>();
                lastCacheCheck = DateTime.Now;
            }
        }

        public T GetFromCache<T>(string ID)
        {
            if (cacheDuration <= 0)
                return default(T);

            var result = cache.Where(kvp => kvp.Key.ID == ID && kvp.Key.typeOfObject == typeof(T));

            if (result.Count() == 0)
                return default(T);

            var cacheResult = result.Single();

            if ((DateTime.Now - cacheResult.Value.cached).TotalMinutes > cacheDuration)
                return default(T);

            try
            {
                string fileString = File.ReadAllText(cacheResult.Value.path);
                return (T)CacheSerializer.DeserializeObject(fileString);
            }
            catch
            {
                return default(T);
            }
        }

        public void Update<T>(T objectToCache, string ID)
        {
            if (cacheDuration <= 0)
                return;

            var existingCache = cache.Where(kvp => kvp.Key.ID == ID && kvp.Key.typeOfObject == typeof(T));

            if (existingCache.Count() > 0)
                cache.Remove(existingCache.Single().Key);

            try
            {
                string pathToFile = Path.Combine(Path.GetTempPath(), $"crapiwrapper-{ID}.{typeof(T).ToString()}");

                File.WriteAllText(pathToFile, CacheSerializer.SerializeObject(objectToCache));

                cache.Add(new CacheKey() { ID = ID, typeOfObject = typeof(T) }, new CacheDetails() { cached = DateTime.Now, path = pathToFile });
            }
            catch { }

            if ((DateTime.Now - lastCacheCheck).TotalMinutes > cacheCheckInterval)
                RemoveOutdatedCache();
        }

        private void RemoveOutdatedCache()
        {
            var outdatedCaches = cache.Where(kvp => (DateTime.Now - kvp.Value.cached).Minutes > cacheDuration);

            foreach (var oCache in outdatedCaches)
            {
                try
                {
                    File.Delete(oCache.Value.path);
                    cache.Remove(oCache.Key);
                }
                catch { }
            }

            lastCacheCheck = DateTime.Now;
        }

        ~Cacher()
        {
            foreach (string path in cache.Select(x => x.Value.path))
                try
                {
                    File.Delete(path);
                }
                catch { }
        }
    }

    internal static class CacheSerializer
    {
        public static string SerializeObject(object o)
        {
            if (!o.GetType().IsSerializable)
            {
                return null;
            }

            using (MemoryStream stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, o);
                return Convert.ToBase64String(stream.ToArray());
            }
        }

        public static object DeserializeObject(string str)
        {
            byte[] bytes = Convert.FromBase64String(str);

            using (MemoryStream stream = new MemoryStream(bytes))
            {
                return new BinaryFormatter().Deserialize(stream);
            }
        }
    }

    internal struct CacheKey
    {
        public string ID;
        public Type typeOfObject;
    }

    internal struct CacheDetails
    {
        public DateTime cached;
        public string path;
    }

    #endregion

    /// <summary>
    /// This is only used to gather info from API reponse error responses. Do not use.
    /// </summary>
    class BadResponse
    {
        public bool error;
        public int status;
        public string message;
    }
}
