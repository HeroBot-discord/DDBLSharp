using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DDBLSharp
{
    public class DDBLBotClient : DDBLClient
    {
        /// <summary>
        /// The bot's Id
        /// </summary>
        public ulong? BotId { get; set; }
        /// <summary>
        /// The api key used to access the account.
        /// </summary>
        public string ApiKey { get; set; }
        /// <summary>
        /// The main constructor. You need to define <see cref="BotId"/> & <see cref="ApiKey"/>
        /// </summary>
        public DDBLBotClient() {
            //if (!BotId.HasValue || ApiKey == null) throw new InvalidOperationException("Can't initialize the BotClient without BotId & ApiKey");
            Http.DefaultRequestHeaders.TryAddWithoutValidation("Authorization",ApiKey);
        }

        public Task<User> GetSelfUserAsync() {
            return GetBotAsync(BotId.Value);
        }
        public async Task<bool> UserHasVotedAsync(ulong id,int hours) {
            return (await (await Http.GetAsync($"{Endpoint}/{BotId}/votes?filter={hours}")).Content.ReadAsStringAsync()).Contains(id.ToString());
        }

        public Task<HttpResponseMessage> UpdateStatsAsync(User.Stats stats) {
            return Http.PostAsync($"{Endpoint}/{BotId}/stats", new StringContent(JsonConvert.SerializeObject(stats)));
        }
    }
    /// <summary>
    /// Base class used to connect to the rest api.
    /// </summary>
    public class DDBLClient {
        /// <summary>
        /// The api endpoint
        /// </summary>
        public string Endpoint { get; set; } = "https://divinediscordbots.com/bot";
        /// <summary>
        /// The http client used to communicae with the api.
        /// </summary>
        protected HttpClient Http { get; set; } = new HttpClient();
        /// <summary>
        /// Constructs tje client. Create thu user-agent variable.
        /// </summary>
        public DDBLClient() {
            Http.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent",$"{typeof(DDBLClient).Assembly.GetName().Name}/{typeof(DDBLClient).Assembly.GetName().Version} .NET");
        }
        /// <summary>
        /// Get a bot from the api by id.
        /// </summary>
        /// <param name="id">The bot's id.</param>
        /// <returns>A task returning the user</returns>
        public async Task<User> GetBotAsync(ulong id)
        {
            return await (await Http.GetAsync($"{Endpoint}/{id}/stats")
                .ContinueWith(async promise =>
                {
                    return JsonConvert.DeserializeObject<User>((await (await promise).Content.ReadAsStringAsync()));
                }));
        }
        /// <summary>
        /// The struct representing the user in the api.
        /// </summary>
        public struct User
        {
            /// <summary>
            /// The statistics gived by the api.
            /// </summary>
            public struct Stats
            {
                /// <summary>
                /// The server count of the bot.
                /// </summary>
                [JsonProperty("server_count")]
                public int ServerCount { set; get; }
                /// <summary>
                /// The shards count of the bot.
                /// </summary>
                [JsonProperty("shards")]
                public int ShardCount { set;  get; }
            }
            /// <summary>
            /// The first case of the Stats array.
            /// </summary>
            [JsonIgnore]
            public Stats LastStats { get {
                    return BotStats[0];
                }
            }
            /// <summary>
            /// The name of the bot i guess.
            /// </summary>
            [JsonProperty("name")]
            public string Name { get; }
            /// <summary>
            /// the id of the bot.
            /// </summary>
            [JsonProperty("id")]
            public string Id { get; }
            /// <summary>
            /// The main owner.
            /// </summary>
            [JsonProperty("owner")]
            public string MainOwner { get; }
            /// <summary>
            /// A short description of the bot.
            /// </summary>
            [JsonProperty("shortdesc")]
            public string ShortDescription { get; }
            /// <summary>
            /// The detailed description of the bot.
            /// </summary>
            [JsonProperty("description")]
            public string CompleteDescription { get; }
            /// <summary>
            /// A link to invite the bot.
            /// </summary>
            [JsonProperty("invite")]
            public string Invite { get; }
            /// <summary>
            /// The prefix of the bot.
            /// </summary>
            [JsonProperty("prefix")]
            public string Prefix { get; }
            /// <summary>
            /// The stats of the bot.
            /// </summary>
            [JsonProperty("stats")]
            public Stats[] BotStats { get; }
            /// <summary>
            /// The owners of the bot.
            /// </summary>
            [JsonProperty("owners")]
            public string[] Owners { get; }
            /// <summary>
            /// Id the bot an nsfw bot ?
            /// </summary>
            [JsonProperty("nsfw")]
            public bool Nsfw { get; }
        }
    }
}
