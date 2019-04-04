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
//Initialize the DDBL object
var ddbl = new DDBL()
{
  BotId = BOT ID HERE,
  Token = "TOKEN HERE"
};

//Post guild/server count to DDBL
ddbl.PostStats(SERVER COUNT HERE);

//Retrieve your bot information
var info = ddbl.RetrieveStats();
Console.Writeline(info.name);

//Check if specific user has voted within x amount of hours
ddbl.UserHasVoted(USER ID, HOURS);

```

