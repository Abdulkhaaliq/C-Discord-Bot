using Discord;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;

namespace Oubru_Bot.Commands
{
    public class WelcomeCommand : BaseCommandModule
    {
        [Command("postwelcome")]
        public async Task welcome(CommandContext ctx)
        {
            var b = new DiscordEmbedBuilder();
            b.WithTitle($"Welcome to the {ctx.Guild.Name} Server! I'm Oubru Bot, the Colorful Bot! My server prefex is '?' Yo. Let's get started!");
            b.WithDescription("Before you can do ANYTHING, you must go to #rules-and-access channel and read through the rules first! You will also find instructions on how to access the channels! PLEASE NOTE: If you are not a verified member within 3 days or type in something OTHER than the desired answer, you will be kicked automatically. See you on the other side!");
            await ctx.Channel.SendMessageAsync("", false, b);
        }
    }
    //https://discord.gg/xSAbxg
}
