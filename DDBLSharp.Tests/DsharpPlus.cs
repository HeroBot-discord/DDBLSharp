using DDBLSharp;
using DSharpPlus;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTests
{
    class DsharpPlus
    {
        private ClientConfiguration _config;
        private DiscordShardedClient _discordSharded;
        private DiscordClient _discord;


        [SetUp]
        public void Setup()
        {


            _config = new DDBLSharp.ClientConfiguration()
            {
                ApiKey = Environment.GetEnvironmentVariable("TEST_API_KEY")
            };
        }

        [Test]
        public async Task DiscordNetShardedClient()
        {
            _discordSharded = new DiscordShardedClient(new DiscordConfiguration()
            {
                Token = Environment.GetEnvironmentVariable("TEST_TOKEN"),
                TokenType = TokenType.Bot
            });
            var service = new DDBLSharp.DSharpPlus.DDBLService(_discordSharded, _config);
            _discordSharded.StartAsync().Wait();
            await _discordSharded.StartAsync();
            await Task.Delay(10000);
            await service.Client.GetSelfUserAsync();
            Assert.Pass();
        }
        [Test]
        public async Task DiscordNetSocketClient()
        {
            _discord = new DiscordClient(new DiscordConfiguration() {
                Token = Environment.GetEnvironmentVariable("TEST_TOKEN"),
                TokenType = TokenType.Bot
            });
            var service = new DDBLSharp.DSharpPlus.DDBLService(_discord, _config);
            _discord.InitializeAsync().Wait();
            _discord.ConnectAsync().Wait();
            await Task.Delay(10000);
            await service.Client.GetSelfUserAsync();
            Assert.Pass();
        }

    }
}
