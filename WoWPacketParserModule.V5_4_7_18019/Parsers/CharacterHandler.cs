using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WoWPacketParserModule.V5_4_7_18019.Parsers
{
    public static class CharacterHandler
    {
        [Parser(Opcode.CMSG_CHAR_CREATE)]
        public static void HandleClientCharCreate510(Packet packet)
        {
            packet.ReadByte("Outfit Id");
            packet.ReadByte("Facial Hair");
            packet.ReadByte("Skin");
            packet.ReadEnum<Race>("Race", TypeCode.Byte);
            packet.ReadByte("Hair Style");
            packet.ReadEnum<Class>("Class", TypeCode.Byte);
            packet.ReadByte("Face");
            packet.ReadEnum<Gender>("Gender", TypeCode.Byte);
            packet.ReadByte("Hair Color");
            packet.ReadByte("Unk Id");
            packet.ReadCString("Name");
        }

        [Parser(Opcode.SMSG_CHAR_ENUM)]
        public static void HandleCharEnum510(Packet packet)
        {
            var unkCounter = packet.ReadBits("Unk Counter", 23);
            var count = packet.ReadBits("Char count", 17);
            if (count > 0)
            {
                var charGuids = new byte[count][];
                var guildGuids = new byte[count][];
                var firstLogins = new bool[count];
                var nameLenghts = new uint[count];

                for (int c = 0; c < count; ++c)
                {
                    charGuids[c] = new byte[8];
                    guildGuids[c] = new byte[8];

                    charGuids[c][7] = packet.ReadBit();
                    charGuids[c][0] = packet.ReadBit();
                    charGuids[c][4] = packet.ReadBit();
                    guildGuids[c][2] = packet.ReadBit();
                    charGuids[c][5] = packet.ReadBit();
                    charGuids[c][3] = packet.ReadBit();
                    nameLenghts[c] = packet.ReadBits(7);
                    guildGuids[c][0] = packet.ReadBit();
                    guildGuids[c][5] = packet.ReadBit();
                    guildGuids[c][3] = packet.ReadBit();
                    firstLogins[c] = packet.ReadBit();
                    guildGuids[c][6] = packet.ReadBit();
                    guildGuids[c][7] = packet.ReadBit();
                    charGuids[c][1] = packet.ReadBit();
                    guildGuids[c][4] = packet.ReadBit();
                    guildGuids[c][1] = packet.ReadBit();
                    charGuids[c][2] = packet.ReadBit();
                    charGuids[c][6] = packet.ReadBit();
                }

                packet.ReadBit("Unk bit");
                packet.ResetBitReader();

                for (int c = 0; c < count; ++c)
                {
                    packet.ReadEnum<CharacterFlag>("CharacterFlag", TypeCode.Int32, c);
                    packet.ReadInt32("Pet Family", c);
                    var z = packet.ReadSingle("Position Z", c);
                    packet.ReadXORByte(charGuids[c], 7);
                    packet.ReadXORByte(guildGuids[c], 6);

                    for (var itm = 0; itm < 23; ++itm)
                    {
                        packet.ReadInt32("Item EnchantID", c, itm);
                        packet.ReadEnum<InventoryType>("Item InventoryType", TypeCode.Byte, c, itm);
                        packet.ReadInt32("Item DisplayID", c, itm);
                    }

                    var x = packet.ReadSingle("Position X", c);
                    var clss = packet.ReadEnum<Class>("Class", TypeCode.Byte, c);
                    packet.ReadXORByte(charGuids[c], 5);
                    var y = packet.ReadSingle("Position Y", c);
                    packet.ReadXORByte(guildGuids[c], 3);
                    packet.ReadXORByte(charGuids[c], 6);
                    packet.ReadInt32("Pet Level", c);
                    packet.ReadInt32("Pet Display ID", c);
                    packet.ReadXORByte(charGuids[c], 2);
                    packet.ReadXORByte(charGuids[c], 1);
                    packet.ReadByte("Hair Color", c);
                    packet.ReadByte("Facial Hair", c);
                    packet.ReadXORByte(guildGuids[c], 2);
                    var zone = packet.ReadEntryWithName<UInt32>(StoreNameType.Zone, "Zone Id", c);
                    packet.ReadByte("List Order", c);
                    packet.ReadXORByte(charGuids[c], 0);
                    packet.ReadXORByte(guildGuids[c], 1);
                    packet.ReadByte("Skin", c);
                    packet.ReadXORByte(charGuids[c], 4);
                    packet.ReadXORByte(guildGuids[c], 5);
                    var name = packet.ReadWoWString("Name", (int)nameLenghts[c], c);
                    packet.ReadXORByte(guildGuids[c], 0);
                    var level = packet.ReadByte("Level", c);
                    packet.ReadXORByte(charGuids[c], 3);
                    packet.ReadXORByte(guildGuids[c], 7);
                    packet.ReadByte("Hair Style", c);
                    packet.ReadXORByte(guildGuids[c], 4);
                    packet.ReadEnum<Gender>("Gender", TypeCode.Byte, c);
                    var mapId = packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map Id", c);
                    packet.ReadEnum<CustomizationFlag>("CustomizationFlag", TypeCode.UInt32, c);
                    var race = packet.ReadEnum<Race>("Race", TypeCode.Byte, c);
                    packet.ReadByte("Face", c);

                    var playerGuid = new WowPacketParser.Misc.Guid(BitConverter.ToUInt64(charGuids[c], 0));

                    packet.WriteGuid("Character GUID", charGuids[c], c);
                    packet.WriteGuid("Guild GUID", guildGuids[c], c);

                    if (firstLogins[c])
                    {
                        var startPos = new StartPosition();
                        startPos.Map = mapId;
                        startPos.Position = new Vector3(x, y, z);
                        startPos.Zone = zone;

                        Storage.StartPositions.Add(new Tuple<Race, Class>(race, clss), startPos, packet.TimeSpan);
                    }

                    var playerInfo = new Player { Race = race, Class = clss, Name = name, FirstLogin = firstLogins[c], Level = level };
                    if (Storage.Objects.ContainsKey(playerGuid))
                        Storage.Objects[playerGuid] = new Tuple<WoWObject, TimeSpan?>(playerInfo, packet.TimeSpan);
                    else
                        Storage.Objects.Add(playerGuid, playerInfo, packet.TimeSpan);
                    StoreGetters.AddName(playerGuid, name);
                }

                for (var i = 0; i < unkCounter; ++i)
                {
                    packet.ReadByte("Unk byte", i);
                    packet.ReadUInt32("Unk int", i);
                }
            }
        }
    }
}
