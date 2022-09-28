using Discord.WebSocket;

namespace PingPongBot.Contracts.Interfaces;

public interface IBaseCommand
{
    public Task RegisterCommand();
    public Task ExecCommand(SocketSlashCommand command);
}