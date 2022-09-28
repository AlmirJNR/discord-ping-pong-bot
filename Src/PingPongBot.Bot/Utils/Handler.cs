using Discord.WebSocket;
using PingPongBot.Commands.Ping;

namespace PingPongBot.Bot.Utils;

public class SlashCommandHandler
{
    private readonly PingCommand? _pingCommand;

    public SlashCommandHandler(PingCommand? pingCommand)
    {
        _pingCommand = pingCommand;
    }

    public async Task HandleCommand(SocketSlashCommand command)
    {
        switch (command.Data.Name)
        {
            case "ping":
                if (_pingCommand is not null)
                    await _pingCommand.ExecCommand(command);
                break;
        }
    }
}