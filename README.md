# DDBLSharp
Divine Discord Bot List API in C#

```
Authorization: "yes"
Ratelimit: 1 minute
```

Methods:
```
.PostStats(int ServerCount)
.RetrieveStats()
.UserHasVoted(ulong UserId, int Hours)
```

Example Usage:

```
var ddbl = new DDBL()
{
  BotId = BOT ID HERE,
  Token = "TOKEN HERE"
};

ddbl.PostStats(SERVER COUNT HERE);

ddbl.RetrieveStats();

var info = ddbl.RetrieveStats();
Console.WriteLine(info.shortdesc);
```

