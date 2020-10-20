using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Oubru_Bot
{
       class Program
       {
           static void Main(string[] args)
           {
              var bot = new Bot();
              bot.RunAsync().GetAwaiter().GetResult();

           }
       
     
       }      
}
