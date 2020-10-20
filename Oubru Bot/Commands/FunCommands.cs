using Discord.WebSocket;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oubru_Bot.Commands
{
    public class FunCommands : BaseCommandModule
    {
        //Add commands here
        [Command("Ping")]
        public async Task Ping(CommandContext ctx)
        {

            await ctx.Channel.SendMessageAsync($"Oubru, don't be lastag {ctx.User.Mention}!!").ConfigureAwait(false);

        }

        [Command("Hi")]
        public async Task Greet(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync($"Hey {ctx.User.Mention}, hope you are doing good!!").ConfigureAwait(false);
        }

        [Command("respondmessage")]
        public async Task RespondMessage(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();

            var message = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);

            await ctx.Channel.SendMessageAsync(message.Result.Content);
        }

        [Command("respondreaction")]
        public async Task RespondReaction(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();

            var message = await interactivity.WaitForReactionAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);

            await ctx.Channel.SendMessageAsync(message.Result.Emoji);
        }

        [Command("add")]
        [Description("Adds two numbers together")]
        [RequireRoles(RoleCheckMode.Any, "Moderator", "Owner")]
        public async Task Add(CommandContext ctx,
           [Description("First Number")] int numberOne,
           [Description("Second Number")] int numberTwo)
        {
            await ctx.Channel
                .SendMessageAsync((numberOne + numberTwo).ToString())
                .ConfigureAwait(false);
        }

        [Command("poll")]
        public async Task Poll(CommandContext ctx, TimeSpan duration, params DiscordEmoji[] emojiOptions)
        {
            var interactivity = ctx.Client.GetInteractivity();
            var options = emojiOptions.Select(x => x.ToString());

            var pollEmbed = new DiscordEmbedBuilder
            {
                Title = "Poll",
                Description = string.Join(" ", options)
            };

            var pollMessage = await ctx.Channel.SendMessageAsync(embed: pollEmbed).ConfigureAwait(false);

            foreach (var option in emojiOptions)
            {
                await pollMessage.CreateReactionAsync(option).ConfigureAwait(false);
            }

            var result = await interactivity.CollectReactionsAsync(pollMessage, duration).ConfigureAwait(false);
            var distinctResult = result.Distinct();

            var results = distinctResult.Select(x => $"{x.Emoji}: {x.Total}");

            await ctx.Channel.SendMessageAsync(string.Join("\n", results)).ConfigureAwait(false);
        }

        [Command("Random")]
        public async Task Random(CommandContext ctx)
        {
            var rnd = new Random();
            await ctx.RespondAsync($"🎲 {ctx.User.Mention} random number is: {rnd.Next(1, 1000)}").ConfigureAwait(false);
        }

        [Command("Flip")]
        public async Task Flip(CommandContext ctx)
        {
            var rnd = new Random();
            var answer = rnd.Next(1, 2);
            if(answer == 1)
            {
                await ctx.Channel.SendMessageAsync($"{ctx.User.Mention} got Heads").ConfigureAwait(false);
            }
            else
            {
                await ctx.Channel.SendMessageAsync($"{ctx.User.Mention} got Tails").ConfigureAwait(false);
            }

        }
    }
}
