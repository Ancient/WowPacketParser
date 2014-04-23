using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V5_4_7_18019
{
    public static class Opcodes_5_4_7
    {
        public static BiDictionary<Opcode, int> Opcodes()
        {
            return Opcs;
        }

        private static readonly BiDictionary<Opcode, int> Opcs = new BiDictionary<Opcode, int>
        {
            {Opcode.CMSG_ADD_FRIEND, 0x064F}, // 18019
            {Opcode.CMSG_ADD_IGNORE, 0x126D}, // 18019
            {Opcode.CMSG_AREATRIGGER, 0x155A}, // 18019
            {Opcode.CMSG_ATTACKSTOP, 0x1E13}, // 18019
            {Opcode.CMSG_ATTACKSWING, 0x1513}, // 18019
            {Opcode.CMSG_AUCTION_HELLO, 0x047F}, // 18019
            {Opcode.CMSG_AUTH_SESSION, 0x1A51}, // 18019
            {Opcode.CMSG_AUTOEQUIP_ITEM, 0x166B}, // 18019
            {Opcode.CMSG_AUTOSTORE_LOOT_ITEM, 0x1F7A}, // 18019
            {Opcode.CMSG_BANKER_ACTIVATE, 0x02FD}, // 18019
            {Opcode.CMSG_BINDER_ACTIVATE, 0x0477}, // 18019
            {Opcode.CMSG_BUYBACK_ITEM, 0x07D7}, // 18019
            {Opcode.CMSG_BUY_BANK_SLOT, 0x00FE}, // 18019
            {Opcode.CMSG_BUY_ITEM, 0x1077}, // 18019
            {Opcode.CMSG_CALENDAR_ADD_EVENT, 0x16D0}, // 18019

            {Opcode.CMSG_CANCEL_CAST, 0x12EB}, // 18019
            {Opcode.CMSG_CANCEL_MOUNT_AURA, 0x1552}, // 18019
            {Opcode.CMSG_CANCEL_TRADE, 0x1D32}, // 18019
            {Opcode.CMSG_CAST_SPELL, 0x1E5B}, // 18019

            {Opcode.CMSG_CHAR_CREATE, 0x09B9}, // 18019
            {Opcode.CMSG_CHAR_DELETE, 0x113B}, // 18019
            {Opcode.CMSG_CHAR_ENUM, 0x12C2}, // 18019

           // {Opcode.CMSG_CORPSE_QUERY, 0x129B}, // 18019
            {Opcode.CMSG_CREATURE_QUERY, 0x1E72}, // 18019

            {Opcode.CMSG_DESTROY_ITEM, 0x1F12}, // 18019

            {Opcode.CMSG_GAMEOBJECT_QUERY, 0x14EA}, // 18019
            {Opcode.CMSG_GAMEOBJ_REPORT_USE, 0x06DF}, // 18019
            {Opcode.CMSG_GAMEOBJ_USE, 0x055F}, // 18019

            {Opcode.CMSG_GOSSIP_HELLO, 0x05F6}, // 18019
            {Opcode.CMSG_GOSSIP_SELECT_OPTION, 0x02D7}, // 18019

            {Opcode.SMSG_GAMEOBJECT_QUERY_RESPONSE, 0x066A},// 18019
            {Opcode.SMSG_UPDATE_OBJECT, 0x1725}, // 18019
            {Opcode.SMSG_DESTROY_OBJECT, 0x1D69}, // 18019

            {Opcode.SMSG_WARDEN_DATA, 0x14EB}, // 18019
            {Opcode.SMSG_DB_REPLY,0x1F01}, // 18019
            {Opcode.CMSG_REQUEST_HOTFIX, 0x16C2}, // 18019
            {Opcode.SMSG_MOTD, 0x0E20}, // 18019
            {Opcode.SMSG_CHAR_ENUM, 0x040A},
            {Opcode.CMSG_PLAYER_LOGIN, 0x17D3},
            {Opcode.SMSG_SET_PROFICIENCY, 0x1E3B},
            {Opcode.SMSG_INITIALIZE_FACTIONS, 0x11E1},
            {Opcode.SMSG_CHAR_CREATE, 0x1469},
            {Opcode.CMSG_READY_FOR_ACCOUNT_DATA_TIMES, 0x13CB},
            {Opcode.SMSG_ACCOUNT_DATA_TIMES, 0x0F40},
            {Opcode.SMSG_SPELL_GO, 0x0851},
            {Opcode.SMSG_AURA_UPDATE, 0x1B8D},
            //{Opcode.CMSG_QUESTGIVER_STATUS_QUERY ,0x05FD},
            {Opcode.SMSG_AUTH_RESPONSE, 0x15A0},
            {Opcode.CMSG_REALM_SPLIT, 0x1282},
            {Opcode.SMSG_POWER_UPDATE, 0x1441},
            {Opcode.CMSG_QUEST_QUERY, 0x1F52},
            {Opcode.SMSG_QUEST_QUERY_RESPONSE, 0x0F13},


            {Opcode.SMSG_QUESTUPDATE_COMPLETE, 0x0F77},
            {Opcode.SMSG_CREATURE_QUERY_RESPONSE, 0x00E0}
            //{Opcode.SMSG_SET_TIMEZONE_INFORMATION, 0x0C2B}, // 18019
        };
    }
}
