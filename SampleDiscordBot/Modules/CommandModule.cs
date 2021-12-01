using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace SampleDiscordBot.Modules
{
    [Group("test")]
    [Name("Test")]
    [Summary("Test commands")]
    public class CommandModule : ModuleBase<SocketCommandContext>
    {
        
        private readonly CommandService _commandService;
        public CommandModule(CommandService commandService)
        {
            _commandService = commandService;
        }
        
        [Command("")]
        [Name("-")]
        [Summary("-")]
        public async Task TestAsync()
        {
            await ReplyAsync("Testing");
        }
        
        [Command("help")]
        [Name("help")]
        [Summary("Display commands")]
        public async Task Help()
        {
            List<CommandInfo> commands = _commandService.Commands.ToList();
            EmbedBuilder embedBuilder = new EmbedBuilder();

            foreach (CommandInfo command in commands)
            {
                // Get the command Summary attribute information
                string embedFieldText = command.Summary ?? "No description available\n";

                embedBuilder.AddField(command.Name, embedFieldText);

                foreach(Attribute attr in command.Attributes)
                {
                    Console.WriteLine(attr.ToString());
                }
                Console.WriteLine(command.Module.Group);
            }

            await ReplyAsync("Here's a list of commands and their description: ", false, embedBuilder.Build());
        }
        
        [Command("ping")]
        [Name("ping")]
        [Summary("Get a Pong back")]
        public async Task PingAsync()
        {
            Console.WriteLine("Ping Command");
            await ReplyAsync("pong");
        }

        [Command("say")]
        [Name("say")]
        [Summary("Echoes a message")]
        public async Task SayAsync([Remainder] [Summary("The text to echo")] string echo)
        {
            await ReplyAsync(echo);
        }

        [Command("square")]
        [Name("square")]
        [Summary("Returns the value^2")]
        public async Task SquareAsync(int value)
        {
            await ReplyAsync("Square of "+value+" is "+value* value);
        }

        [Command("list")]
        [Name("list")]
        [Summary("Turn the inputs into a list")]
        public async Task ListAsync(params string[] values)
        {
            foreach (var value in values)
            {
                await ReplyAsync("-" + value);
            }
        }

        [Command("userinfo")]
        [Name("userinfo")]
        [Summary ("Returns info about the current user, or the user parameter, if one passed.")]
        [Alias("user", "whois")]
        public async Task UserInfoAsync([Summary("The (optional) user to get info from")] SocketUser user = null)
        {
            var userInfo = user ?? Context.Client.CurrentUser;
            await ReplyAsync($"{userInfo.Username}#{userInfo.Discriminator}");
        }

        [Command("givexp")]
        [Name("givexp")]
        [Summary("Attempt to give xp to a user")]
        public async Task UserInfoAsync([Summary("The (optional) user to get info from")] SocketUser user = null, int xp = 0)
        {
            var userInfo = user ?? Context.Client.CurrentUser;
            await ReplyAsync("/give-xp "+userInfo.Username+" "+ xp);
        }
    }
}