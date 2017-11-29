using System;
using System.IO;
using System.Net;

namespace CRAPI
{
    /// <summary>
    /// Base CR API class, contains basic information and net methods
    /// </summary>
    public static class ClashRoyale
    {
        private const string domain = "http://api.cr-api.com/";
        

        /// <summary>
        /// All current API endpoints
        /// </summary>
        public enum Endpoints
        {
            Profile,
            Top,
            Clan,
            Constants,
            Version
        }

        /// <summary>
        /// Get current API version in format like "4.0.3"
        /// </summary>
        public static string Version
        {
            get
            {
                return Get(Endpoints.Version, "");
            }
            set { }
        }

        /// <summary>
        /// Get direct output from CR API
        /// </summary>
        /// <param name="endpoint">Endpoint to use</param>
        /// <param name="parameter">Parameter to endpoint (like player ID)</param>
        /// <returns></returns>
        public static string Get(Endpoints endpoint, string parameter)
        {
            return Get(domain + endpoint.ToString() + "/" + parameter);
        }

        private static string Get(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            WebResponse myResponse = req.GetResponse();
            StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
            string result = sr.ReadToEnd();
            sr.Close();
            myResponse.Close();

            return result;
        }
    }
}
