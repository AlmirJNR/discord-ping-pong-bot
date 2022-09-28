using Discord;

namespace PingPongBot.Bot.Utils;

public class Logger
{
    public Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
}