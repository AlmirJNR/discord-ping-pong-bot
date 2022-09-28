using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using PingPongBot.Bot.Utils;
using PingPongBot.Commands.Ping;
using PingPongBot.Contracts.Interfaces;

namespace PingPongBot.Bot;

public static class Program
{
    private static readonly IServiceProvider ServiceProvider = CreateProvider();
    private static readonly DiscordSocketClient? Client = ServiceProvider.GetService<DiscordSocketClient>();
    private static readonly Logger? Logger = ServiceProvider.GetService<Logger>();
    private static readonly IBaseCommand? PingCommand = ServiceProvider.GetService<PingCommand>();

    private static readonly SlashCommandHandler?
        SlashCommandHandler = ServiceProvider.GetService<SlashCommandHandler>();

    public static void Main(string[] args)
        => MainAsync()
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult();

    private static IServiceProvider CreateProvider()
    {
        var config = new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.GuildMessages
        };

        return new ServiceCollection()
            .AddSingleton(x => new DiscordSocketClient(config))
            .AddSingleton<Logger>()
            .AddSingleton<PingCommand>()
            .AddSingleton<SlashCommandHandler>()
            .BuildServiceProvider();
    }

    private static async Task MainAsync()
    {
        if (Client is null)
            throw new ArgumentException("Dependency injection error");

        if (Logger is not null)
            Client.Log += Logger.Log;

        var token = Environment.GetEnvironmentVariable("BOT_TOKEN")
                    ?? throw new ArgumentException("Missing environment variable BOT_TOKEN");

        await Client.LoginAsync(TokenType.Bot, token);
        await Client.StartAsync();

        if (PingCommand is not null)
            Client.Ready += PingCommand.RegisterCommand;

        if (SlashCommandHandler is not null)
            Client.SlashCommandExecuted += SlashCommandHandler.HandleCommand;

        await Task.Delay(Timeout.Infinite);
    }
}