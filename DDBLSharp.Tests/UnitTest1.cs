using DDBLSharp;
using Discord.WebSocket;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Tests
{
    public class Tests
    {
        private DiscordShardedClient _discordNetSharded;
        private DiscordSocketClient _discordNetSocket;
        private ClientConfiguration _config;

        [SetUp]
        public void Setup()
        {
            _discordNetSharded = new DiscordShardedClient();
            _discordNetSharded.LoginAsync(Discord.TokenType.Bot, Environment.GetEnvironmentVariable("DISCORD_TOKEN")).Wait();
            _discordNetSocket = new DiscordSocketClient();
            _discordNetSocket.LoginAsync(Discord.TokenType.Bot, Environment.GetEnvironmentVariable("DISCORD_TOKEN")).Wait();
            _config = new DDBLSharp.ClientConfiguration()
            {
                ApiKey = Environment.GetEnvironmentVariable("API_KEY")
            };
        }

        [Test]
        public async Task GetBotNoAuth()
        {
            var client = new DDBLClient();
            await client.GetBotAsync(491673480006205461);
            Assert.Pass();
        }
        [Test]
        public async Task GetBotWithAuth() {
            var client = new DDBLBotClient()
            {
                BotId = 491673480006205461,
                ApiKey = _config.ApiKey,
            };
            await client.GetBotAsync(491673480006205461);
            Assert.Pass();
        }
        [Test]
        public async Task GetSelfbot() {
            var client = new DDBLBotClient()
            {
                BotId = 491673480006205461,
                ApiKey = _config.ApiKey,
            };
            await client.GetSelfUserAsync();
            Assert.Pass();
        }

        [Test]
        public async Task DiscordNetShardedClient() {

            var service = new DDBLSharp.Discord.NET.DDBLService(_discordNetSharded, _config);
            await _discordNetSharded.StartAsync();
            if (service.Client == null) {
                Assert.Fail("The client is null.");
            }
        }

    }
}