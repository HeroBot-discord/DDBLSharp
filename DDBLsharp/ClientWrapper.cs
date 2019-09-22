using System;
using System.Collections.Generic;
using System.Text;

namespace DDBLSharp
{
    public interface DiscordClientWrapper
    {
        int GuildCount { get; }
        int ShardCount { get; }
        ulong Id { get; }
    }
}
