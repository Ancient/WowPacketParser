using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WoWPacketParserModule.V5_4_7_18019.Parsers
{
    public static class QuestHandler
    {
        [Parser(Opcode.SMSG_QUESTUPDATE_COMPLETE)]
        public static void HandleQuestUpdateComplete547(Packet packet)
        {
            packet.ReadGuid("Guid");

            packet.ReadInt32("Unk Int32");



            packet.ReadInt32("QuestGiver Portrait");
            packet.ReadInt32("QuestTurn Portrait");
            packet.ReadByte("Unk Byte");

            packet.ReadCString("Complete Text");
            packet.ReadCString("QuestGiver Text Window");
            packet.ReadCString("QuestGiver Target Name");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Quest, "Quest ID");
            packet.ReadCString("QuestTurn Text Window");
            packet.ReadCString("QuestTurn Target Name");
            packet.ReadCString("Title");

            packet.ReadEnum<QuestFlags>("Quest Flags", TypeCode.UInt32);
            packet.ReadEnum<QuestFlags2>("Quest Flags 2", TypeCode.UInt32);
            packet.ReadInt32("Unk Int32");

            var emoteCount = packet.ReadUInt32("Quest Emote Count");
            for (var i = 0; i < emoteCount; i++)
            {
                packet.ReadUInt32("Emote Delay (ms)", i);
                packet.ReadUInt32("Emote Id", i);
            }

            //ReadExtraQuestInfo(ref packet);
        }
    }
}
