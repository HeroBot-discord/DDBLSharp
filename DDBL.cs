using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace DDBLSharp
{
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
        /// Retrieve Bot Information
        /// </summary> 
        public GetStats RetrieveStats()
        {
            WebClient client = new WebClient();
            client.Headers.Add("Authorization", Token);
            client.Headers.Add("content-type", "application/json");
            GetStats json = new GetStats();

            try
            {
                var data = client.DownloadString(new Uri("https://divinediscordbots.com/bot/" + BotId + "/stats"));
                json = JsonConvert.DeserializeObject<GetStats>(data);

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
        public class GetStats
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
