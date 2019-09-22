using Discord.WebSocket;
using System.Threading.Tasks;

namespace DDBLSharp.Discord.NET
{
    public class DDBLService
    {
        /// <summary>
        /// The client ussed to connect to the api.
        /// </summary>
        public DDBLBotClient Client { get; private set; }
        /// <summary>
        /// The configuration of the client.
        /// </summary>
        private readonly ClientConfiguration _config;
        /// <summary>
        /// The Discord.NEt Wrapper
        /// </summary>
        private readonly DiscordClientWrapper _discord;

        /// <summary>
        /// Used to create the service.
        /// </summary>
        public DDBLService(DiscordShardedClient client, ClientConfiguration configuration)
        {
            _config = configuration;
            _discord = new DiscordShardedClientWrapper(ref client);
            int shardsConnected = 0;
            client.JoinedGuild += async (guild) => await UpdateStats();
            client.LeftGuild += async (guild) => await UpdateStats();
            client.ShardReady += async (shard) =>
            {
                if (shardsConnected != -1)
                {
                    shardsConnected++;
                    if (shardsConnected == client.Shards.Count)
                    {
                        shardsConnected = -1;
                        await Init();
                    }
                }
            };
        }
        public Task UpdateStats()
        {
            return Client.UpdateStatsAsync(new DDBLClient.User.Stats()
            {
                ServerCount = _discord.GuildCount,
                ShardCount = _discord.ShardCount
            });
        }
        public DDBLService(DiscordSocketClient client,ClientConfiguration configuration) {
            _discord = new DiscordSocketClientWrapper(ref client);
            _config = configuration;
            client.JoinedGuild += async (guild) => await UpdateStats();
            client.LeftGuild += async (guild) => await UpdateStats();
            client.Ready += async () => await Init();
        }
        private async Task Init()
        {
            // Now the client is lunched.
            Client = new DDBLBotClient() {
                ApiKey = _config.ApiKey,
                BotId = _discord.Id
            };
            await UpdateStats();
        }
    }
}