using Discord.WebSocket;
namespace DDBLSharp.Discord.NET
{

    public class DiscordShardedClientWrapper : DiscordClientWrapper
    {
        private readonly DiscordShardedClient _discord;

        public DiscordShardedClientWrapper(ref DiscordShardedClient discord)
        {
            _discord = discord;
        }
        public ulong Id => _discord.CurrentUser.Id;
        public int GuildCount => _discord.Guilds.Count;
        public int ShardCount => _discord.Shards.Count;
    }

    public class DiscordSocketClientWrapper : DiscordClientWrapper
    {
        private readonly DiscordSocketClient _discord;

        public DiscordSocketClientWrapper(ref DiscordSocketClient discord)
        {
            _discord = discord;
        }
        public int GuildCount => _discord.Guilds.Count;

        public int ShardCount => 1;

        public ulong Id => _discord.CurrentUser.Id;
    }
}
