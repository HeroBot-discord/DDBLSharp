using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace DDBLSharp
{
    /// <summary>  
    /// Divine C# API Wrapper
    /// </summary>  
    public class DDBL
    {
        /// <summary>  
        /// Divine API Token
        /// </summary>  
        public string Token { get; set; }
        /// <summary>  
        /// Discord Bot Id
        /// </summary>  
        public long BotId { get; set; }
        /// <summary>  
        /// Post Server Count
        /// </summary>  
        public void PostStats(int ServerCount)
        {
            WebClient client = new WebClient();
            client.Headers.Add("Authorization", Token);
            client.Headers.Add("content-type", "application/json");
            
            var data = JsonConvert.SerializeObject(new { server_count = ServerCount.ToString() });

            try
            {
                client.UploadString(new Uri("https://divinediscordbots.com/bot/" + BotId + "/stats"), "POST", data);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[SUCCESS] Divine Discord Bot API: Server Count Posted");
                Console.ResetColor();
            }
            catch (WebException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(String.Format("[ERROR: {0}] Divine Discord Bot API: {1}", e.Status, e.Message));
                Console.ResetColor();
            }
            finally
            {
                client?.Dispose();
            }
        }
        /// <summary>  
        /// Retrieve bot information
        /// </summary> 
        public BotInfo RetrieveStats()
        {
            WebClient client = new WebClient();
            client.Headers.Add("Authorization", Token);
            client.Headers.Add("content-type", "application/json");
            BotInfo json = new BotInfo();

            try
            {
                var data = client.DownloadString(new Uri("https://divinediscordbots.com/bot/" + BotId + "/stats"));
                json = JsonConvert.DeserializeObject<BotInfo>(data);
            }
            catch (WebException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(String.Format("[ERROR: {0}] Divine Discord Bot API: {1}", e.Status, e.Message));
                Console.ResetColor();
            }
            finally
            {
                client?.Dispose();
            }
            
            return json;
        }
        /// <summary>  
        /// Check if specific user has voted within specific amount of hours
        /// </summary> 
        public bool UserHasVoted(ulong UserId, int Hours)
        {
            WebClient client = new WebClient();
            client.Headers.Add("content-type", "application/json");

            try
            {
                var data = client.DownloadString(new Uri("https://divinediscordbots.com/bot/" + BotId + "/votes?filter=" + Hours));
                if (data.Contains(UserId.ToString())) return true;
            }
            catch (WebException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(String.Format("[ERROR: {0}] Divine Discord Bot API: {1}", e.Status, e.Message));
                Console.ResetColor();
            }
            finally
            {
                client?.Dispose();
            }

            return false;
        }
        /// <summary>  
        /// Class for RetrieveStats method
        /// </summary> 
        public class BotInfo
        {
            public class Stats
            {
                public int server_count { get; set; }
                public int shards { get; set; }
            }

            public string name { get; set; }
            public string id { get; set; }
            public string owner { get; set; }
            public string shortdesc { get; set; }
            public string description { get; set; }
            public string invite { get; set; }
            public string prefix { get; set; }
            public List<Stats> stats { get; set; }
            public string[] owners { get; set; }
            public string nsfw { get; set; }

        }
    }
}
