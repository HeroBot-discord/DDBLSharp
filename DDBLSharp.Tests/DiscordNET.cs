using DDBLSharp;
using Discord;
using Discord.WebSocket;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTests
{
    class DiscordNET
    {

        private ClientConfiguration _config;
        private DiscordShardedClient _discordNetSharded;
        private DiscordSocketClient _discordNetSocket;


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
            _discordNetSharded = new DiscordShardedClient();
            _discordNetSharded.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("TEST_TOKEN")).Wait();
            var service = new DDBLSharp.Discord.NET.DDBLService(_discordNetSharded, _config);
            await _discordNetSharded.StartAsync();
            await Task.Delay(10000);
            await service.Client.GetSelfUserAsync();
            Assert.Pass();
        }
        [Test]
        public async Task DiscordNetSocketClient()
        {
            _discordNetSocket = new DiscordSocketClient();
            _discordNetSocket.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("TEST_TOKEN")).Wait();
            var service = new DDBLSharp.Discord.NET.DDBLService(_discordNetSocket, _config);
            await _discordNetSocket.StartAsync();
            await Task.Delay(10000);
            await service.Client.GetSelfUserAsync();
            Assert.Pass();
        }
    }
}
