using System;
using System.IO;
using System.Net;

namespace CRAPI
{
    /// <summary>
    /// Base CR API class, contains basic information
    /// </summary>
    public static class ClashRoyale
    {
        /// <summary>
        /// Get current API version in format like "4.0.3"
        /// </summary>
        public static string Version
        {
            get
            {
                return Inside.APIworker.Get(Inside.APIworker.Endpoints.Version, "");
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
