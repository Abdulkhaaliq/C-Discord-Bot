using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oubru_Bot.Commands
{
   public class InfoCommand : BaseCommandModule
    {
    
        [Command("info")]
        public async Task Info(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Hello, I am called Oubru Bot. Your #1 stop and chill bot located in the beautiful mother city!" + "If you want a list of my commands, type '?help' and ill be there in your dms :)");
        }
    
    }
}