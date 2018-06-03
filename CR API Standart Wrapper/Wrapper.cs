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
        private const string domain = "http://api.royaleapi.com/";

        private readonly string key;
        private Cacher cache;

        /// <summary>
        /// Last successful server response. Request something and access this to get server JSON response
        /// </summary>
        public string ServerResponse { get; private set; }
        /// <summary>
        /// Remaining requests in this minute. There are 3 requests/second allowed. This value is updated after every request.
        /// </summary>
        public int RequestsRemaining { get; private set; }
        /// <summary>
        /// Should wrapper throttle calls by user? Exceeding request limits is now penalized by API.
        /// </summary>
        private bool Throttle;
        private DateTime LastRequestTimestamp;

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
            None, _EU = 57000000, _NA, _SA, _AS, _AU, _AF, _INT, AF, AX, AL, DZ, AS, AD, AO, AI, AQ, AG, AR, AM, AW, AC, AU, AT, AZ, BS, BH, BD, BB, BY, BE, BZ, BJ, BM, BT, BO, BA, BW, BV, BR, IO, VG, BN, BG, BF, BI, KH, CM, CA, IC, CV, BQ, KY, CF, EA, TD, CL, CN, CX, CC, CO, KM, CG, CD, CK, CR, CI, HR, CU, CW, CY, CZ, DK, DG, DJ, DM, DO, EC, EG, SV, GQ, ER, EE, ET, FK, FO, FJ, FI, FR, GF, PF, TF, GA, GM, GE, DE, GH, GI, GR, GL, GD, GP, GU, GT, GG, GN, GW, GY, HT, HM, HN, HK, HU, IS, IN, ID, IR, IQ, IE, IM, IL, IT, JM, JP, JE, JO, KZ, KE, KI, XK, KW, KG, LA, LV, LB, LS, LR, LY, LI, LT, LU, MO, MK, MG, MW, MY, MV, ML, MT, MH, MQ, MR, MU, YT, MX, FM, MD, MC, MN, ME, MS, MA, MZ, MM, NA, NR, NP, NL, NC, NZ, NI, NE, NG, NU, NF, KP, MP, NO, OM, PK, PW, PS, PA, PG, PY, PE, PH, PN, PL, PT, PR, QA, RE, RO, RU, RW, BL, SH, KN, LC, MF, PM, WS, SM, ST, SA, SN, RS, SC, SL, SG, SX, SK, SI, SB, SO, ZA, KR, SS, ES, LK, VC, SD, SR, SJ, SZ, SE, CH, SY, TW, TJ, TZ, TH, TL, TG, TK, TO, TT, TA, TN, TR, TM, TC, TV, UM, VI, UG, UA, AE, GB, US, UY, UZ, VU, VA, VE, VN, WF, EH, YE, ZM, ZW
        }

        /// <summary>
        /// Initialize wrapper for CR API
        /// </summary>
        /// <param name="devkey">Private developer key</param>
        /// <param name="throttleCallsOnWrapperLevel">As exceeding call limit is now penalized, wrapper will by default throttle calls</param>
        /// <param name="cacheDuration">Wrapper cache duration in minutes. 0 or less = no cache</param>
        public Wrapper(string devkey, bool throttleCallsOnWrapperLevel = true, int cacheDuration = 0)
        {
            this.key = devkey;
            this.Throttle = throttleCallsOnWrapperLevel;
            this.LastRequestTimestamp = DateTime.Now;
            this.cache = new Cacher(cacheDuration);
        }

        #region GetFromAPI

        /// <summary>
        /// Get instance of Player from API
        /// </summary>
        /// <param name="tag">Player's tag. Must be upper-case</param>
        /// <param name="includeChestCycleInRequest">Include chest cycle in player object. This consumes additional request (see <see cref="RequestsRemaining"/>)</param>
        /// <param name="includeBattlesDataInRequest">Include battles data in player object. This consumes additional request (see <see cref="RequestsRemaining"/>)</param>
        /// <param name="include">Optional parameter, may be null. Specifies fields to be included in response. Everything else is dropped. This parameter and/or [exclude] parameter must be NULL.</param>
        /// <param name="exclude">Optional parameter, may be null. Specifies fields to be dropped from response. Everything else is delivered. This parameter and/or [include] parameter must be NULL.</param>
        /// <returns></returns>
        public Player GetPlayer(string tag, bool includeChestCycleInRequest, bool includeBattlesDataInRequest, string[] include = null, string[] exclude = null)
        {
            try
            {
                Task<Player> task = GetPlayerAsync(tag, includeChestCycleInRequest, includeBattlesDataInRequest, include, exclude);
                task.Wait();
                return task.Result;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        /// <summary>
        /// Get few Player instances from API
        /// </summary>
        /// <param name="tags">Player tags, must be upper-case. Do not pass hundreds of tags, as timeout is set to 20 seconds</param>
        /// <param name="includeChestCycleInRequest">Include chest cycle in player object. This consumes additional request (see <see cref="RequestsRemaining"/>)</param>
        /// <param name="includeBattlesDataInRequest">Include battles data in player object. This consumes additional request (see <see cref="RequestsRemaining"/>)</param>
        /// <param name="include">Optional parameter, may be null. Specifies fields to be included in response. Everything else is dropped. This parameter and/or [exclude] parameter must be NULL.</param>
        /// <param name="exclude">Optional parameter, may be null. Specifies fields to be dropped from response. Everything else is delivered. This parameter and/or [include] parameter must be NULL.</param>
        /// <returns></returns>
        public Player[] GetPlayer(string[] tags, bool includeChestCycleInRequest, bool includeBattlesDataInRequest, string[] include = null, string[] exclude = null)
        {
            try
            {
                Task<Player[]> task = GetPlayerAsync(tags, includeChestCycleInRequest, includeBattlesDataInRequest, include, exclude);
                task.Wait();
                return task.Result;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
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
            try
            {
                Task<Clan> task = GetClanAsync(tag, include, exclude);
                task.Wait();
                return task.Result;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        /// <summary>
        /// Get few instatnces of Clan from API
        /// </summary>
        /// <param name="tag">Clan tags. Must be upper-case. Do not pass hundreds of tags, as timeout is set to 20 seconds</param>
        /// <param name="include">Optional parameter, may be null. Specifies fields to be included in response. Everything else is dropped. This parameter and/or [exclude] parameter must be NULL.</param>
        /// <param name="exclude">Optional parameter, may be null. Specifies fields to be dropped from response. Everything else is delivered. This parameter and/or [include] parameter must be NULL.</param>
        /// <returns></returns>
        public Clan[] GetClan(string[] tags, string[] include = null, string[] exclude = null)
        {
            try
            {
                Task<Clan[]> task = GetClanAsync(tags, include, exclude);
                task.Wait();
                return task.Result;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        /// <summary>
        /// Get currently running clan war
        /// </summary>
        /// <param name="tag">Tag of clan</param>
        /// <param name="include">Optional parameter, may be null. Specifies fields to be included in response. Everything else is dropped. This parameter and/or [exclude] parameter must be NULL.</param>
        /// <param name="exclude">Optional parameter, may be null. Specifies fields to be dropped from response. Everything else is delivered. This parameter and/or [include] parameter must be NULL.</param>
        /// <returns></returns>
        public ClanWarInfo GetClanWar(string tag, string[] include = null, string[] exclude = null)
        {
            try
            {
                Task<ClanWarInfo> task = GetClanWarAsync(tag, include, exclude);
                task.Wait();
                return task.Result;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        /// <summary>
        /// Get past clan wars of clan
        /// </summary>
        /// <param name="tag">Tag of clan</param>
        /// <param name="include">Optional parameter, may be null. Specifies fields to be included in response. Everything else is dropped. This parameter and/or [exclude] parameter must be NULL.</param>
        /// <param name="exclude">Optional parameter, may be null. Specifies fields to be dropped from response. Everything else is delivered. This parameter and/or [include] parameter must be NULL.</param>
        /// <returns></returns>
        public ClanWarsRecord[] GetPastClanWars(string tag, string[] include = null, string[] exclude = null)
        {
            try
            {
                Task<ClanWarsRecord[]> task = GetPastClanWarsAsync(tag, include, exclude);
                task.Wait();
                return task.Result;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        /// <summary>
        /// Get top players in clash royale. Returned players are instances of SimplifiedPlayer, thus contain only basic information
        /// </summary>
        /// <param name="region">Optional parameter, feault value is Locations.None. Specifies from what country should be top players returned. This CANNOT be location starting with "_" (like _EU, _NA, etc). In that case, Location.None will be used.</param>
        /// <param name="include">Optional parameter, may be null. Specifies fields to be included in response. Everything else is dropped. This parameter and/or [exclude] parameter must be NULL.</param>
        /// <param name="exclude">Optional parameter, may be null. Specifies fields to be dropped from response. Everything else is delivered. This parameter and/or [include] parameter must be NULL.</param>
        /// <returns></returns>
        public SimplifiedPlayer[] GetTopPlayers(Locations region = Locations.None, string[] include = null, string[] exclude = null)
        {
            try
            {
                Task<SimplifiedPlayer[]> task = GetTopPlayersAsync(region, include, exclude);
                task.Wait();
                return task.Result;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        /// <summary>
        /// Get top clans in clash royale. Returned clans are instances of SimplifiedClan, thus contain only basic information
        /// </summary>
        /// <param name="region">Optional parameter, default value is Locations.None. Specifies region, only clans from selected region will be returned. Select Locations.None to return clans from whole world.</param>
        /// <param name="include">Optional parameter, may be null. Specifies fields to be included in response. Everything else is dropped. This parameter and/or [exclude] parameter must be NULL.</param>
        /// <param name="exclude">Optional parameter, may be null. Specifies fields to be dropped from response. Everything else is delivered. This parameter and/or [include] parameter must be NULL.</param>
        /// <returns></returns>
        public SimplifiedClan[] GetTopClans(Locations region = Locations.None, string[] include = null, string[] exclude = null)
        {
            try
            {
                Task<SimplifiedClan[]> task = GetTopClansAsync(region, include, exclude);
                task.Wait();
                return task.Result;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        /// <summary>
        /// Search for clans and get array of clan info. You need to pass AT LEAST one
        /// parameter. If you do not want to pass some parameter, enter NULL instead.
        /// </summary>
        /// <param name="name">Name to search. If you do not want to input this one, enter NULL</param>
        /// <param name="score">Minimum clan score. If you do not want to input this one, enter NULL</param>
        /// <param name="minMembers">Minimum members in clan. 0-50. If you do not want to input this one, enter NULL</param>
        /// <param name="maxMembers">Maximum members in clan. 0-60. If you do not want to input this one, enter NULL</param>
        /// <param name="region">Optional parameter, default value is Locations.None. Enter another one to search only for clans in region.</param>
        /// <param name="include">Optional parameter, may be null. Specifies fields to be included in response. Everything else is dropped. This parameter and/or [exclude] parameter must be NULL.</param>
        /// <param name="exclude">Optional parameter, may be null. Specifies fields to be dropped from response. Everything else is delivered. This parameter and/or [include] parameter must be NULL.</param>
        /// <returns></returns>
        public SimplifiedClan[] SearchForClans(string name, int? score, int? minMembers, int? maxMembers, Locations region = Locations.None, int? max = null, string[] include = null, string[] exclude = null)
        {
            try
            {
                Task<SimplifiedClan[]> task = SearchForClansAsync(name, score, minMembers, maxMembers, region, max, include, exclude);
                task.Wait();
                return task.Result;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
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
            try
            {
                Task<Tournament> task = GetTournamentAsync(tag, include, exclude);
                task.Wait();
                return task.Result;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        /// <summary>
        /// Get currently open tournaments
        /// </summary>
        public SimplifiedTournament[] GetOpenTournaments(string[] include = null, string[] exclude = null)
        {
            try
            {
                Task<SimplifiedTournament[]> task = GetOpenTournamentsAsync(include, exclude);
                task.Wait();
                return task.Result;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        /// <summary>
        /// Search for tournaments based on their name
        /// </summary>
        public Tournament[] SearchForTournaments(string name, string[] include = null, string[] exclude = null)
        {
            try
            {
                Task<Tournament[]> task = SearchForTournamentsAsync(name, include, exclude);
                task.Wait();
                return task.Result;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        /// <summary>
        /// Get clan history. You have to enable tracking on clan to be able to do this. See docs.royaleapi.com/#/endpoints/clan_history?id=how-to-be-included .
        /// This method returns dictionary, where key (string) is time and value (ClanHistoryRecord) is clan snapshot from the date
        /// </summary>
        /// <param name="tag">Clan tag</param>
        /// <param name="days">How many days should the returned history have</param>
        /// <param name="include">Optional parameter, may be null. Specifies fields to be included in response. Everything else is dropped. This parameter and/or [exclude] parameter must be NULL.</param>
        /// <param name="exclude">Optional parameter, may be null. Specifies fields to be dropped from response. Everything else is delivered. This parameter and/or [include] parameter must be NULL.</param>
        /// <returns></returns>
        public Dictionary<string, ClanHistoryRecord> GetClanHistory(string tag, int days = 7, string[] include = null, string[] exclude = null)
        {
            try
            {
                Task<Dictionary<string, ClanHistoryRecord>> task = GetClanHistoryAsync(tag, days, include, exclude);
                task.Wait();
                return task.Result;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        #endregion

        #region GetFromAPIasync

        /// <summary>
        /// Get instance of Player from API async
        /// </summary>
        /// <param name="tag">Player's tag. Must be upper-case</param>
        /// <param name="includeChestCycleInRequest">Include chest cycle in player object. This consumes additional request (see <see cref="RequestsRemaining"/>)</param>
        /// <param name="includeBattlesDataInRequest">Include battles data in player object. This consumes additional request (see <see cref="RequestsRemaining"/>)</param>
        /// <param name="include">Optional parameter, may be null. Specifies fields to be included in response. Everything else is dropped. This parameter and/or [exclude] parameter must be NULL.</param>
        /// <param name="exclude">Optional parameter, may be null. Specifies fields to be dropped from response. Everything else is delivered. This parameter and/or [include] parameter must be NULL.</param>
        /// <returns></returns>
        public async Task<Player> GetPlayerAsync(string tag, bool includeChestCycleInRequest, bool includeBattlesDataInRequest, string[] include = null, string[] exclude = null)
        {
            string query = String.Empty;
            if (include != null && exclude != null)
                throw new ArgumentException("At least one of parameters (include, exclude) must be NULL", "include, exclude");
            if (include != null)
                query += "?keys=" + String.Join(",", include);
            if (exclude != null)
                query += "?exclude=" + String.Join(",", exclude);

            Player cachedResult = cache.GetFromCache<Player>(tag + String.Join("", include ?? new string[0]) + String.Join("", exclude ?? new string[0]) + includeChestCycleInRequest + includeBattlesDataInRequest);
            if (cachedResult != null)
                return cachedResult;

            Task<string> output = GetAsync(Endpoints.Player, tag + query);
            Player result = Parse<Player>(await output);
            if (includeChestCycleInRequest)
                result.chestCycle = await GetPlayerChestCycle(tag);
            if (includeBattlesDataInRequest)
                result.battles = await GetPlayerBattles(tag);

            cache.Update(result, tag + String.Join("", include ?? new string[0]) + String.Join("", exclude ?? new string[0]) + includeChestCycleInRequest + includeBattlesDataInRequest);

            return result;
        }

        /// <summary>
        /// Get few Player instances from API
        /// </summary>
        /// <param name="tags">Player tags, must be upper-case. Do not pass hundreds of tags, as timeout is set to 20 seconds</param>
        /// <param name="includeChestCycleInRequest">Include chest cycle in player object. This consumes additional request (see <see cref="RequestsRemaining"/>)</param>
        /// <param name="includeBattlesDataInRequest">Include battles data in player object. This consumes additional request (see <see cref="RequestsRemaining"/>)</param>
        /// <param name="include">Optional parameter, may be null. Specifies fields to be included in response. Everything else is dropped. This parameter and/or [exclude] parameter must be NULL.</param>
        /// <param name="exclude">Optional parameter, may be null. Specifies fields to be dropped from response. Everything else is delivered. This parameter and/or [include] parameter must be NULL.</param>
        /// <returns></returns>
        public async Task<Player[]> GetPlayerAsync(string[] tags, bool includeChestCycleInRequest, bool includeBattlesDataInRequest, string[] include = null, string[] exclude = null)
        {
            if ((includeChestCycleInRequest || includeBattlesDataInRequest) && include != null && include.Length != 0 && !include.Contains("tag"))
            {
                Array.Resize(ref include, include.Length + 1);
                include[include.Length - 1] = "tag";
            }

            string query = String.Empty;
            if (include != null && exclude != null)
                throw new ArgumentException("At least one of parameters (include, exclude) must be NULL", "include, exclude");
            if (include != null)
                query += "?keys=" + String.Join(",", include);
            if (exclude != null)
                query += "?exclude=" + String.Join(",", exclude);

            // Check for cache
            Player[] cachedResult = cache.GetFromCache<Player[]>(String.Join("", tags) + String.Join("", include ?? new string[0]) + String.Join("", exclude ?? new string[0]) + includeChestCycleInRequest + includeBattlesDataInRequest);
            if (cachedResult != null)
                return cachedResult;

            Task<string> output = GetAsync(Endpoints.Player, String.Join(",", tags) + query);
            Player[] result = Parse<Player[]>(await output);
            for (int i = 0; i < result.Length; i++)
            {
                Player p = result[i];
                if (includeChestCycleInRequest)
                    p.chestCycle = await GetPlayerChestCycle(p.tag);
                if (includeBattlesDataInRequest)
                    p.battles = await GetPlayerBattles(p.tag);
            }

            cache.Update(result, String.Join("", tags) + String.Join("", include ?? new string[0]) + String.Join("", exclude ?? new string[0]) + includeChestCycleInRequest + includeBattlesDataInRequest);

            return result;
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
            string query = String.Empty;
            if (include != null && exclude != null)
                throw new ArgumentException("At least one of parameters (include, exclude) must be NULL", "include, exclude");
            if (include != null)
                query += "?keys=" + String.Join(",", include);
            if (exclude != null)
                query += "?exclude=" + String.Join(",", exclude);

            Clan cachedResult = cache.GetFromCache<Clan>(tag + String.Join("", include ?? new string[0]) + String.Join("", exclude ?? new string[0]));
            if (cachedResult != null)
                return cachedResult;

            Task<string> output = GetAsync(Endpoints.Clan, tag + query);
            Clan result = Parse<Clan>(await output);

            cache.Update(result, tag + String.Join("", include ?? new string[0]) + String.Join("", exclude ?? new string[0]));

            return result;
        }

        /// <summary>
        /// Get few instatnces of Clan from API
        /// </summary>
        /// <param name="tag">Clan tags. Must be upper-case. Do not pass hundreds of tags, as timeout is set to 20 seconds</param>
        /// <param name="include">Optional parameter, may be null. Specifies fields to be included in response. Everything else is dropped. This parameter and/or [exclude] parameter must be NULL.</param>
        /// <param name="exclude">Optional parameter, may be null. Specifies fields to be dropped from response. Everything else is delivered. This parameter and/or [include] parameter must be NULL.</param>
        /// <returns></returns>
        public async Task<Clan[]> GetClanAsync(string[] tags, string[] include = null, string[] exclude = null)
        {
            string query = String.Empty;
            if (include != null && exclude != null)
                throw new ArgumentException("At least one of parameters (include, exclude) must be NULL", "include, exclude");
            if (include != null)
                query += "?keys=" + String.Join(",", include);
            if (exclude != null)
                query += "?exclude=" + String.Join(",", exclude);

            // Check for cache
            Clan[] cachedResult = cache.GetFromCache<Clan[]>(String.Join("", tags) + String.Join("", include ?? new string[0]) + String.Join("", exclude ?? new string[0]));
            if (cachedResult != null)
                return cachedResult;

            Task<string> output = GetAsync(Endpoints.Clan, String.Join(",", tags) + query);
            Clan[] result = Parse<Clan[]>(await output);

            cache.Update(result, String.Join("", tags) + String.Join("", include ?? new string[0]) + String.Join("", exclude ?? new string[0]));

            return result;
        }

        /// <summary>
        /// Get currently running clan war
        /// </summary>
        /// <param name="tag">Tag of clan</param>
        /// <param name="include">Optional parameter, may be null. Specifies fields to be included in response. Everything else is dropped. This parameter and/or [exclude] parameter must be NULL.</param>
        /// <param name="exclude">Optional parameter, may be null. Specifies fields to be dropped from response. Everything else is delivered. This parameter and/or [include] parameter must be NULL.</param>
        /// <returns></returns>
        public async Task<ClanWarInfo> GetClanWarAsync(string tag, string[] include = null, string[] exclude = null)
        {
            string query = String.Empty;
            if (include != null && exclude != null)
                throw new ArgumentException("At least one of parameters (include, exclude) must be NULL", "include, exclude");
            if (include != null)
                query += "?keys=" + String.Join(",", include);
            if (exclude != null)
                query += "?exclude=" + String.Join(",", exclude);

            ClanWarInfo cachedResult = cache.GetFromCache<ClanWarInfo>(tag + "war" + String.Join("", include ?? new string[0]) + String.Join("", exclude ?? new string[0]));
            if (cachedResult != null)
                return cachedResult;

            Task<string> output = GetAsync(Endpoints.Clan, tag + "/war" + query);
            ClanWarInfo result = Parse<ClanWarInfo>(await output);

            cache.Update(result, tag + "war" + String.Join("", include ?? new string[0]) + String.Join("", exclude ?? new string[0]));

            return result;
        }

        /// <summary>
        /// Get past clan wars of clan
        /// </summary>
        /// <param name="tag">Tag of clan</param>
        /// <param name="include">Optional parameter, may be null. Specifies fields to be included in response. Everything else is dropped. This parameter and/or [exclude] parameter must be NULL.</param>
        /// <param name="exclude">Optional parameter, may be null. Specifies fields to be dropped from response. Everything else is delivered. This parameter and/or [include] parameter must be NULL.</param>
        /// <returns></returns>
        public async Task<ClanWarsRecord[]> GetPastClanWarsAsync(string tag, string[] include = null, string[] exclude = null)
        {
            string query = String.Empty;
            if (include != null && exclude != null)
                throw new ArgumentException("At least one of parameters (include, exclude) must be NULL", "include, exclude");
            if (include != null)
                query += "?keys=" + String.Join(",", include);
            if (exclude != null)
                query += "?exclude=" + String.Join(",", exclude);

            ClanWarsRecord[] cachedResult = cache.GetFromCache<ClanWarsRecord[]>(tag + "pastwars" + String.Join("", include ?? new string[0]) + String.Join("", exclude ?? new string[0]));
            if (cachedResult != null)
                return cachedResult;

            Task<string> output = GetAsync(Endpoints.Clan, tag + "/warlog" + query);
            ClanWarsRecord[] result = Parse<ClanWarsRecord[]>(await output);

            cache.Update(result, tag + "pastwars" + String.Join("", include ?? new string[0]) + String.Join("", exclude ?? new string[0]));

            return result;
        }

        /// <summary>
        /// Get top players in clash royale async. Returned players are instances of SimplifiedPlayer, thus contain only basic information
        /// </summary>
        /// <param name="region">Optional parameter, feault value is Locations.None. Specifies from what country should be top players returned. This CANNOT be location starting with "_" (like _EU, _NA, etc). In that case, Location.None will be used.</param>
        /// <param name="include">Optional parameter, may be null. Specifies fields to be included in response. Everything else is dropped. This parameter and/or [exclude] parameter must be NULL.</param>
        /// <param name="exclude">Optional parameter, may be null. Specifies fields to be dropped from response. Everything else is delivered. This parameter and/or [include] parameter must be NULL.</param>
        /// <returns></returns>
        public async Task<SimplifiedPlayer[]> GetTopPlayersAsync(Locations region = Locations.None, string[] include = null, string[] exclude = null)
        {
            if (region.ToString().StartsWith("_"))
                region = Locations.None;

            string query = String.Empty;
            if (include != null && exclude != null)
                throw new ArgumentException("At least one of parameters (include, exclude) must be NULL", "include, exclude");
            if (include != null)
                query += "?keys=" + String.Join(",", include);
            if (exclude != null)
                query += "?exclude=" + String.Join(",", exclude);

            SimplifiedPlayer[] cachedResult = cache.GetFromCache<SimplifiedPlayer[]>("topPlayersRegion" + region.ToString() + String.Join("", include ?? new string[0]) + String.Join("", exclude ?? new string[0]));
            if (cachedResult != null)
                return cachedResult;

            Task<string> output = GetAsync(Endpoints.Top, region != Locations.None ? "players/" + region.ToString() + query : "players" + query);

            SimplifiedPlayer[] result = Parse<SimplifiedPlayer[]>(await output);

            cache.Update(result, "topPlayersRegion" + region.ToString() + String.Join("", include ?? new string[0]) + String.Join("", exclude ?? new string[0]));

            return result;
        }

        /// <summary>
        /// Get top clans in clash royale async. Returned clans are instances of SimplifiedClan, thus contain only basic information
        /// </summary>
        /// <param name="region">Optional parameter, default value is Locations.None. Specifies region, only clans from selected region will be returned. Select Locations.None to return clans from whole world.</param>
        /// <param name="include">Optional parameter, may be null. Specifies fields to be included in response. Everything else is dropped. This parameter and/or [exclude] parameter must be NULL.</param>
        /// <param name="exclude">Optional parameter, may be null. Specifies fields to be dropped from response. Everything else is delivered. This parameter and/or [include] parameter must be NULL.</param>
        /// <returns></returns>
        public async Task<SimplifiedClan[]> GetTopClansAsync(Locations region = Locations.None, string[] include = null, string[] exclude = null)
        {
            string query = String.Empty;
            if (include != null && exclude != null)
                throw new ArgumentException("At least one of parameters (include, exclude) must be NULL", "include, exclude");
            if (include != null)
                query += "?keys=" + String.Join(",", include);
            if (exclude != null)
                query += "?exclude=" + String.Join(",", exclude);

            SimplifiedClan[] cachedResult = cache.GetFromCache<SimplifiedClan[]>("topClansRegion" + region.ToString() + String.Join("", include ?? new string[0]) + String.Join("", exclude ?? new string[0]));
            if (cachedResult != null)
                return cachedResult;

            Task<string> output = GetAsync(Endpoints.Top, region == Locations.None ? "clans" + query : "clans/" + region.ToString() + query);
            SimplifiedClan[] result = Parse<SimplifiedClan[]>(await output);

            cache.Update(result, "topClansRegion" + region.ToString() + String.Join("", include ?? new string[0]) + String.Join("", exclude ?? new string[0]));

            return result;
        }

        /// <summary>
        /// Search for clans and get array of clan info async. You need to pass AT LEAST one
        /// parameter. If you do not want to pass some parameter, enter NULL instead
        /// </summary>
        /// <param name="name">Name to search. If you do not want to input this one, enter NULL</param>
        /// <param name="score">Minimum clan score. If you do not want to input this one, enter NULL</param>
        /// <param name="minMembers">Minimum members in clan. 0-50. If you do not want to input this one, enter NULL</param>
        /// <param name="maxMembers">Maximum members in clan. 0-60. If you do not want to input this one, enter NULL</param>
        /// <param name="region">Optional parameter, default value is Locations.None. Enter another one to search only for clans in region.</param>
        /// <param name="include">Optional parameter, may be null. Specifies fields to be included in response. Everything else is dropped. This parameter and/or [exclude] parameter must be NULL.</param>
        /// <param name="exclude">Optional parameter, may be null. Specifies fields to be dropped from response. Everything else is delivered. This parameter and/or [include] parameter must be NULL.</param>
        /// <returns></returns>
        public async Task<SimplifiedClan[]> SearchForClansAsync(string name, int? score, int? minMembers, int? maxMembers, Locations region = Locations.None, int? max = null, string[] include = null, string[] exclude = null)
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
            if (max != null)
                queries.Add("max=" + max);

            if (queries.Count == 0)
                throw new ArgumentException("At least one parameter must be not-null!");

            if (include != null && exclude != null)
                throw new ArgumentException("At least one of parameters (include, exclude) must be NULL", "include, exclude");

            if (include != null)
                queries.Add("keys=" + String.Join(",", include));
            if (exclude != null)
                queries.Add("exclude=" + String.Join(",", exclude));

            if (region != Locations.None)
                queries.Add("locationId=" + (int)region);

            string q = String.Empty;
            q += "?" + queries[0];
            for (int i = 1; i < queries.Count; i++)
                q += "&" + queries[i];

            SimplifiedClan[] cachedResult = cache.GetFromCache<SimplifiedClan[]>("clanSearch" + region.ToString() + String.Join("", queries));
            if (cachedResult != null)
                return cachedResult;

            Task<string> output = GetAsync(Endpoints.Clan, "search" + q);

            SimplifiedClan[] result = null;

            try
            {
                result = Parse<SimplifiedClan[]>(await output);
            }
            catch
            {
                result = new SimplifiedClan[] { Parse<SimplifiedClan>(await output) };
            }

            cache.Update(result, "clanSearch" + region.ToString() + String.Join("", queries));

            return result;
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

            Tournament cachedResult = cache.GetFromCache<Tournament>(tag + String.Join("", include ?? new string[0]) + String.Join("", exclude ?? new string[0]));
            if (cachedResult != null)
                return cachedResult;

            Task<string> output = GetAsync(Endpoints.Tournaments, tag + query);
            Tournament result = Parse<Tournament>(await output);

            cache.Update(result, tag + String.Join("", include ?? new string[0]) + String.Join("", exclude ?? new string[0]));

            return result;
        }

        /// <summary>
        /// Get currently open tournaments async
        /// </summary>
        /// <param name="include">Optional parameter, may be null. Specifies fields to be included in response. Everything else is dropped. This parameter and/or [exclude] parameter must be NULL.</param>
        /// <param name="exclude">Optional parameter, may be null. Specifies fields to be dropped from response. Everything else is delivered. This parameter and/or [include] parameter must be NULL.</param>

        public async Task<SimplifiedTournament[]> GetOpenTournamentsAsync(string[] include = null, string[] exclude = null)
        {
            string query = String.Empty;
            if (include != null && exclude != null)
                throw new ArgumentException("At least one of parameters (include, exclude) must be NULL", "include, exclude");
            if (include != null)
                query += "?keys=" + String.Join(",", include);
            if (exclude != null)
                query += "?exclude=" + String.Join(",", exclude);

            SimplifiedTournament[] cachedResult = cache.GetFromCache<SimplifiedTournament[]>("openTournaments");
            if (cachedResult != null)
                return cachedResult;

            Task<string> output = GetAsync(Endpoints.Tournaments, "open" + query);
            SimplifiedTournament[] result = Parse<SimplifiedTournament[]>(await output);

            cache.Update(result, "openTournaments");

            return result;
        }

        /// <summary>
        /// Search for tournaments based on their name async
        /// </summary>
        /// <param name="include">Optional parameter, may be null. Specifies fields to be included in response. Everything else is dropped. This parameter and/or [exclude] parameter must be NULL.</param>
        /// <param name="exclude">Optional parameter, may be null. Specifies fields to be dropped from response. Everything else is delivered. This parameter and/or [include] parameter must be NULL.</param>
        public async Task<Tournament[]> SearchForTournamentsAsync(string name, string[] include = null, string[] exclude = null)
        {
            List<string> queries = new List<string>(4);

            if (name != null)
                if (name.Length < 1)
                    throw new ArgumentException("Parameter must contain at least 1 character.", "name");

            if (name != null)
                queries.Add("name=" + name);

            if (queries.Count == 0)
                throw new ArgumentException("'Name' parameter must be specified.");

            if (include != null && exclude != null)
                throw new ArgumentException("At least one of parameters (include, exclude) must be NULL", "include, exclude");

            if (include != null)
                queries.Add("keys=" + String.Join(",", include));
            if (exclude != null)
                queries.Add("exclude=" + String.Join(",", exclude));

            string q = String.Empty;
            q += "?" + queries[0];
            for (int i = 1; i < queries.Count; i++)
                q += "&" + queries[i];

            Tournament[] cachedResult = cache.GetFromCache<Tournament[]>("tournamentSearch" + String.Join("", queries));
            if (cachedResult != null)
                return cachedResult;

            Task<string> output = GetAsync(Endpoints.Tournaments, "search" + q);
            Tournament[] result = null;

            try
            {
                result = Parse<Tournament[]>(await output);
            }
            catch
            {
                result = new Tournament[] { Parse<Tournament>(await output) };
            }

            cache.Update(result, "tournamentSearch" + String.Join("", queries));

            return result;
        }

        /// <summary>
        /// Get clan history. You have to enable tracking on clan to be able to do this. See docs.royaleapi.com/#/endpoints/clan_history?id=how-to-be-included .
        /// This method returns dictionary, where key (string) is time and value (ClanHistoryRecord) is clan snapshot from the date
        /// </summary>
        /// <param name="tag">Clan tag</param>
        /// <param name="days">How many days should the returned history have</param>
        /// <param name="include">Optional parameter, may be null. Specifies fields to be included in response. Everything else is dropped. This parameter and/or [exclude] parameter must be NULL.</param>
        /// <param name="exclude">Optional parameter, may be null. Specifies fields to be dropped from response. Everything else is delivered. This parameter and/or [include] parameter must be NULL.</param>
        /// <returns></returns>
        public async Task<Dictionary<string, ClanHistoryRecord>> GetClanHistoryAsync(string tag, int days = 7, string[] include = null, string[] exclude = null)
        {
            List<string> queries = new List<string>(3);
            queries.Add(days.ToString());

            if (include != null && exclude != null)
                throw new ArgumentException("At least one of parameters (include, exclude) must be NULL", "include, exclude");

            if (include != null)
                queries.Add("keys=" + String.Join(",", include));
            if (exclude != null)
                queries.Add("exclude=" + String.Join(",", exclude));

            string q = String.Empty;
            q += "?" + queries[0];
            for (int i = 1; i < queries.Count; i++)
                q += "&" + queries[i];

            Dictionary<string, ClanHistoryRecord> cachedResult = cache.GetFromCache<Dictionary<string, ClanHistoryRecord>>("clanHistory" + String.Join("", queries));
            if (cachedResult != null)
                return cachedResult;

            Task<string> output = GetAsync(Endpoints.Clan, tag + "/history" + q);
            Dictionary<string, ClanHistoryRecord> result = null;

            result = Parse<Dictionary<string, ClanHistoryRecord>>(await output);

            cache.Update(result, "clanHistory" + String.Join("", queries));

            return result;
        }

        private async Task<PlayerChestCycle> GetPlayerChestCycle(string tag)
        {
            PlayerChestCycle cachedResult = cache.GetFromCache<PlayerChestCycle>(tag + "chestcycle");
            if (cachedResult != null)
                return cachedResult;

            Task<string> output = GetAsync(Endpoints.Player, tag + "/chests");
            PlayerChestCycle result = Parse<PlayerChestCycle>(await output);

            cache.Update(result, tag + "chestcycle");

            return result;
        }

        private async Task<PlayerChestCycle> GetPlayerChestCycle(string[] tags)
        {
            PlayerChestCycle cachedResult = cache.GetFromCache<PlayerChestCycle>(String.Join(",", tags) + "chestcycle");
            if (cachedResult != null)
                return cachedResult;

            Task<string> output = GetAsync(Endpoints.Player, String.Join(",", tags) + "/chests");
            PlayerChestCycle result = Parse<PlayerChestCycle>(await output);

            cache.Update(result, String.Join(",", tags) + "chestcycle");

            return result;
        }

        private async Task<PlayerBattle[]> GetPlayerBattles(string tag)
        {
            PlayerBattle[] cachedResult = cache.GetFromCache<PlayerBattle[]>(tag + "battles");
            if (cachedResult != null)
                return cachedResult;

            Task<string> output = GetAsync(Endpoints.Player, tag + "/battles");
            PlayerBattle[] result = Parse<PlayerBattle[]>(await output);

            cache.Update(result, tag + "battles");

            return result;
        }

        private async Task<PlayerBattle[]> GetPlayerBattles(string[] tags)
        {
            PlayerBattle[] cachedResult = cache.GetFromCache<PlayerBattle[]>(String.Join(",", tags) + "battles");
            if (cachedResult != null)
                return cachedResult;

            Task<string> output = GetAsync(Endpoints.Player, String.Join(",", tags) + "/battles");
            PlayerBattle[] result = Parse<PlayerBattle[]>(await output);

            cache.Update(result, String.Join(",", tags) + "battles");

            return result;
        }


        public IEnumerable<IEnumerable<O>> Mine<T, O>(IEnumerable<T> input, Func<string, T> getObjectFromTag, IEnumerable<Func<T, IEnumerable<string>>> selectTagsFunction, Func<string, O> resultFunction)
        {
            List<string> tags = new List<string>();

            foreach (T inp in input)
            {
                foreach (Func<T, IEnumerable<string>> function in selectTagsFunction)
                {
                    tags.AddRange(function(inp));
                }
            }

            List<string> ts = new List<string>();
            ts.AddRange(tags);
            for (int i = 0; i < tags.Count; i++)
            {
                T result = getObjectFromTag(tags[i]);
                foreach (Func<T, IEnumerable<string>> function in selectTagsFunction)
                {
                    foreach (string tag in function(result))
                    {
                        if (!tags.Contains(tag))
                        {
                            tags.Add(tag);
                            ts.Add(tag);
                        }
                    }
                }
                yield return ts.Select(tag => resultFunction(tag));
                ts.Clear();
            }
        }

        #endregion

        private async Task<string> GetAsync(Endpoints endpoint, string parameter)
        {
            // If there are no requests remaining
            if (Throttle && RequestsRemaining == 0 && (DateTime.Now - LastRequestTimestamp).TotalSeconds <= 1)
            {
                // Wait a bit
                System.Threading.Thread.Sleep(1000 - (int)(DateTime.Now - LastRequestTimestamp).TotalMilliseconds);
            }

            Task<string> tReq = GetAsync(domain + endpoint.ToString() + "/" + parameter);
            LastRequestTimestamp = DateTime.Now;
            return await tReq;
        }

        private async Task<string> GetAsync(string url)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.MediaType = "GET";
                req.Headers.Add("auth", key);
                req.Timeout = 20000;

                string result;

                using (WebResponse serverResponse = await req.GetResponseAsync())
                {
                    int reqs = -1;
                    int.TryParse(serverResponse.Headers["X-RateLimit-Remaining"], out reqs);
                    if (reqs != -1)
                        RequestsRemaining = reqs;
                    using (StreamReader sr = new StreamReader(serverResponse.GetResponseStream(), System.Text.Encoding.UTF8))
                    {
                        result = sr.ReadToEnd();
                    }
                }

                ServerResponse = result;
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

        private const string tempFileSuffix = ".crapiwrapperTMP";

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
                string fileString = File.ReadAllText(cacheResult.Value.path + tempFileSuffix);
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

                File.WriteAllText(pathToFile + tempFileSuffix, CacheSerializer.SerializeObject(objectToCache));

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
                    File.Delete(oCache.Value.path + tempFileSuffix);
                    cache.Remove(oCache.Key);
                }
                catch { }
            }

            lastCacheCheck = DateTime.Now;
        }

        /// <summary>
        /// Clears cache and removes all temporary files
        /// </summary>
        public void Clear()
        {
            //check to make sure cache is not already null before clearing it
            if (cache != null)
            {
                cache.Clear();
            }

            string[] files = Directory.GetFiles(Path.Combine(Path.GetTempPath()), "*" + tempFileSuffix);

            for (int i = 0; i < files.Length; i++)
                try
                {
                    File.Delete(files[i]);
                }
                catch { }
        }

        ~Cacher()
        {
            if (cache != null)
                Clear();
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
