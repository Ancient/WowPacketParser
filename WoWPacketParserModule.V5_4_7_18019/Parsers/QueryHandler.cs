using System;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParserModule.V5_4_2_18019;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParserModule.V5_4_2_18019.Parsers
{
    public static class QueryHandler
    {
        [HasSniffData]
        [Parser(Opcode.SMSG_CREATURE_QUERY_RESPONSE)]
        public static void HandleCreatureQueryResponse(Packet packet)
        {
            var entry = packet.ReadEntry("Entry");
            var hasData = packet.ReadBit();
            if (!hasData)
                return; // nothing to do

            var creature = new UnitTemplate();

            creature.RacialLeader = packet.ReadBit("Racial Leader");
            var iconLenS = (int)packet.ReadBits(6);
            var unkLens = (int)packet.ReadBits(11);
            var nameLens = (int)packet.ReadBits(11);
            for (int i = 0; i < 6; i++)
                unkLens = (int)packet.ReadBits(11);

            var qItemCount = packet.ReadBits(22);
            var subLenS = (int)packet.ReadBits(11);
            unkLens = (int)packet.ReadBits(11);

            packet.ResetBitReader();

            creature.Modifier2 = packet.ReadSingle("Modifier 2");

            creature.Name = packet.ReadCString("Name");
            creature.Modifier1 = packet.ReadSingle("Modifier 1");
            creature.KillCredits = new uint[2];
            creature.KillCredits[1] = packet.ReadUInt32("KillCredit 2");
            creature.DisplayIds = new uint[4];
            creature.DisplayIds[1] = packet.ReadUInt32("Display ID 1");
            creature.QuestItems = new uint[qItemCount];

            for (var i = 0; i < qItemCount; ++i)
                creature.QuestItems[i] = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Item, "Quest Item", i);

            creature.Type = packet.ReadEnum<CreatureType>("Type", TypeCode.Int32);

            if (iconLenS > 1)
                creature.IconName = packet.ReadCString("Icon Name");

            creature.TypeFlags2 = packet.ReadUInt32("Creature Type Flags 2"); // Missing enum
            creature.TypeFlags = packet.ReadEnum<CreatureTypeFlag>("Type Flags", TypeCode.UInt32);
            creature.KillCredits[0] = packet.ReadUInt32("KillCredit 1");
            creature.Family = packet.ReadEnum<CreatureFamily>("Family", TypeCode.Int32);
            creature.MovementId = packet.ReadUInt32("Movement ID");
            creature.Expansion = packet.ReadEnum<ClientType>("Expansion", TypeCode.UInt32);
            creature.DisplayIds[0] = packet.ReadUInt32("Display ID 0");
            creature.DisplayIds[2] = packet.ReadUInt32("Display ID 2");
            creature.Rank = packet.ReadEnum<CreatureRank>("Rank", TypeCode.Int32);
            if (subLenS > 1)
                creature.SubName = packet.ReadCString("Sub Name");
            creature.DisplayIds[3] = packet.ReadUInt32("Display ID 3");

            packet.AddSniffData(StoreNameType.Unit, entry.Key, "QUERY_RESPONSE");

            Storage.UnitTemplates.Add((uint)entry.Key, creature, packet.TimeSpan);

            var objectName = new ObjectName
            {
                ObjectType = ObjectType.Unit,
                Name = creature.Name,
            };
            Storage.ObjectNames.Add((uint)entry.Key, objectName, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_NPC_TEXT_QUERY)]
        public static void HandleNpcTextQuery(Packet packet)
        {
            var entry = packet.ReadInt32("Entry");

            var GUID = new byte[8];
            GUID = packet.StartBitStream(5, 6, 7, 4, 3, 0, 2, 1);
            packet.ParseBitStream(GUID, 0, 7, 1, 4, 3, 5, 2, 6);
            packet.WriteGuid("GUID", GUID);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_NPC_TEXT_UPDATE)]
        public static void HandleNpcTextUpdate(Packet packet)
        {
            var npcText = new NpcText();

            var hasData = packet.ReadBit("hasData");
            var entry = packet.ReadEntry("TextID");
            if (entry.Value) // Can be masked
                return;

            if (!hasData)
                return; // nothing to do

            var size = packet.ReadInt32("Size");

            npcText.Probabilities = new float[8];
            for (var i = 0; i < 8; ++i)
                npcText.Probabilities[i] = packet.ReadSingle("Probability", i);
            for (var i = 0; i < 8; ++i)
                packet.ReadInt32("Unknown Id", i);
        }
    }
}
