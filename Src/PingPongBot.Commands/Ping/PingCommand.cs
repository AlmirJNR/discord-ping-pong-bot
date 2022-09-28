using Discord;
using Discord.WebSocket;
using PingPongBot.Contracts.Interfaces;

namespace PingPongBot.Commands.Ping;

public class PingCommand : IBaseCommand
{
    private readonly DiscordSocketClient? _client;

    public PingCommand(DiscordSocketClient? discordSocketClient)
    {
        _client = discordSocketClient;
    }

    public async Task RegisterCommand()
    {
        var globalCommand = new SlashCommandBuilder()
            .WithName("ping")
            .WithDescription("This is going to return a pong!");

        var socketCommandTask = _client?.CreateGlobalApplicationCommandAsync(globalCommand.Build());
        if (socketCommandTask is null)
            return;

        await socketCommandTask;
    }

    public async Task ExecCommand(SocketSlashCommand command)
    {
        await command.RespondAsync($"pong!");
    }
}