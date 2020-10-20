
using Discord;
using Discord.WebSocket;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Oubru_Bot.Commands;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Victoria;

namespace Oubru_Bot
{ 
   public class Bot
   {
      public DiscordClient Client { get; private set; }
      public InteractivityExtension InterActivity { get; private set; }
      public CommandsNextExtension Commands { get; private set; }

      private DiscordSocketClient _client;
        public async Task RunAsync()
        {

            var json = string.Empty;

             using (var fs = File.OpenRead("config.json"))
                 using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                     json = await sr.ReadToEndAsync().ConfigureAwait(false);

            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

            var config = new DiscordConfiguration()
            {
                Token = configJson.Token,
                TokenType = DSharpPlus.TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = LogLevel.Debug
            };
         
           Client = new DiscordClient(config);
            Client.Ready += Client_Ready;
            

            _client = new DiscordSocketClient();
            _client.MessageReceived += CommandHandler;
            _client.UserJoined += client_UserJoined;

            Client.UseInteractivity(new InteractivityConfiguration
            {
                PaginationBehaviour = PaginationBehaviour.Ignore,
                Timeout = TimeSpan.FromMinutes(1)
            }) ;

           var commandsConfig = new CommandsNextConfiguration
           {
                StringPrefixes = new string[] { configJson.Prefix },
                EnableDms = false,
                EnableMentionPrefix = true,
                CaseSensitive = false,
                DmHelp = true,
           };

           Commands = Client.UseCommandsNext(commandsConfig);

           Commands.RegisterCommands<FunCommands>();
           Commands.RegisterCommands<InfoCommand>();
           Commands.RegisterCommands<RoleCommands>();
            Commands.RegisterCommands<WelcomeCommand>();


            await Client.ConnectAsync();

           await Task.Delay(-1);
        }

        private Task Client_Ready(DiscordClient sender, ReadyEventArgs e)
        {
            
           return Task.CompletedTask;
        }
        private Task CommandHandler(SocketMessage socketMessage)
        {
            //Filters your commands
            if(!socketMessage.Content.StartsWith('?'))
            {
                return Task.CompletedTask;
            }

            if (socketMessage.Author.IsBot)
            {
                return Task.CompletedTask;
            }

                return Task.CompletedTask;
        }

        private async Task client_UserJoined(SocketGuildUser arg)
        {
            // whatever you put here will execute when a user joins. 
            await arg.Guild.DefaultChannel.SendMessageAsync("Welcome to the Anthamation Server! I'm Oubru Bot, the Colorful Bot! My server prefex is ' ? ' Yo. Let's get started!");
        }

    }


}