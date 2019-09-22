using DDBLSharp;
using Discord.WebSocket;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BasicTests
{
    public class Basic
    {

        private ClientConfiguration _config;

        [SetUp]
        public void Setup()
        {

            _config = new DDBLSharp.ClientConfiguration()
            {
                ApiKey = Environment.GetEnvironmentVariable("TEST_API_KEY")
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
        public async Task GetSelfBot() {
            var client = new DDBLBotClient()
            {
                BotId = 491673480006205461,
                ApiKey = _config.ApiKey,
            };
            await client.GetSelfUserAsync();
            Assert.Pass();
        }


    }
}