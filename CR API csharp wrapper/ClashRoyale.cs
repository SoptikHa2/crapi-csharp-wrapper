using System;

namespace CRAPI
{
    /// <summary>
    /// Base CR API class, contains basic information, like API version, or wrapper version. Can be used to get top players and top clans.
    /// </summary>
    public static class ClashRoyale
    {
        private const string wVersion = "0.1.0";

        /// <summary>
        /// Get current API version in format like "4.0.3"
        /// </summary>
        public static string ApiVersion
        {
            get
            {
                return Inside.APIworker.Get(Inside.APIworker.Endpoints.Version, "");
            }
            set { }
        }

        public static string WrapperVersion
        {
            get
            {
                return wVersion;
            }
            set { }
        }

        public static TopPlayers GetTopPlayers()
        {
            return Inside.APIworker.Parse<TopPlayers>(Inside.APIworker.Get(Inside.APIworker.Endpoints.Top, "players"));
        }

        public static TopClans GetTopClans()
        {
            return Inside.APIworker.Parse<TopClans>(Inside.APIworker.Get(Inside.APIworker.Endpoints.Top, "clans"));
        }
    }
}
