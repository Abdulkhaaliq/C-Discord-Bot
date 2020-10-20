using System;
using System.Collections.Generic;
using System.Text;

namespace Oubru_Bot.Enums
{
    public enum PermissionLevel : byte
    {
        NoAccess = 0, //everyone
        GroupUsers,
        GroupMods, //Bot Mods
        GroupAdmin, //Bot Admins
        BotOwner, //Bot Owner (Global)
    }
}
