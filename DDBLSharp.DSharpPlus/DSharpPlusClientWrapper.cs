namespace DDBLSharp.DSharpPlus
{
    using global::DSharpPlus;
    using System.Linq;

    class DiscordShardedClientWrapper : DiscordClientWrapper
    {
        private readonly DiscordShardedClient _discord;

        public DiscordShardedClientWrapper(ref DiscordShardedClient discord)
        {
            _discord = discord;
        }
        public ulong Id => _discord.CurrentUser.Id;
        public int GuildCount => _discord.ShardClients.Sum(shard => shard.Value.Guilds.Count);
        public int ShardCount => _discord.ShardClients.Count;
    }

    class DiscordSocketClientWrapper : DiscordClientWrapper
    {
        private readonly DiscordClient _discord;

        public DiscordSocketClientWrapper(ref DiscordClient discord)
        {
            _discord = discord;
        }
        public int GuildCount => _discord.Guilds.Count;

        public int ShardCount => 1;

        public ulong Id => _discord.CurrentUser.Id;
    }
}
