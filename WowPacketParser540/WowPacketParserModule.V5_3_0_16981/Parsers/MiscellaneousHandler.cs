﻿using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParserModule.V5_3_0_16981.Enums;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_3_0_16981.Parsers
{
    public static class MiscellaneousHandler
    {
        [Parser(Opcode.SMSG_BATTLEFIELD_STATUS)]
        public static void HandleBattlefieldStatus(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadUInt32("Instance ID");
            packet.ReadUInt32("Queue slot");
            packet.ReadUInt32("Join Time");

            packet.StartBitStream(guid, 1, 2, 4, 6, 5, 7, 0, 3);
            packet.ParseBitStream(guid, 7, 6, 4, 3, 0, 1, 2, 5);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_STATUS_QUEUED)]
        public static void HandleBattlefieldStatusQueued(Packet packet)
        {
            var battlegroundGuid = new byte[8];
            var playerGuid = new byte[8];

            battlegroundGuid[7] = packet.ReadBit();
            playerGuid[6] = packet.ReadBit();
            packet.ReadBit("Eligible in Queue");
            battlegroundGuid[0] = packet.ReadBit();
            packet.ReadBit("isRated");
            packet.ReadBit("Unk Bit");
            playerGuid[5] = packet.ReadBit();
            playerGuid[1] = packet.ReadBit();
            playerGuid[3] = packet.ReadBit();
            battlegroundGuid[6] = packet.ReadBit();
            playerGuid[4] = packet.ReadBit();
            playerGuid[4] = packet.ReadBit();
            battlegroundGuid[4] = packet.ReadBit();
            packet.ReadBit("Unk bit");
            battlegroundGuid[5] = packet.ReadBit();
            playerGuid[7] = packet.ReadBit();
            battlegroundGuid[1] = packet.ReadBit();
            battlegroundGuid[2] = packet.ReadBit();
            playerGuid[2] = packet.ReadBit();
            battlegroundGuid[3] = packet.ReadBit();

            packet.ResetBitReader();

            packet.ReadXORByte(battlegroundGuid, 1);
            packet.ReadUInt32("Estimated Wait Time");
            packet.ReadByte("Unk Byte");
            packet.ReadUInt32("Time Now");
            packet.ReadUInt32("Time of Join");
            packet.ReadXORByte(playerGuid, 2);
            packet.ReadXORByte(playerGuid, 3);
            packet.ReadXORByte(battlegroundGuid, 5);
            packet.ReadXORByte(battlegroundGuid, 3);
            packet.ReadXORByte(playerGuid, 5);
            packet.ReadByte("Min Level");
            packet.ReadByte("Unk Byte");
            packet.ReadXORByte(battlegroundGuid, 4);
            packet.ReadUInt32("Queue slot");
            packet.ReadUInt32("Instance ID");
            packet.ReadXORByte(battlegroundGuid, 2);
            packet.ReadUInt32("Max Players per Team");
            packet.ReadXORByte(playerGuid, 1);
            packet.ReadXORByte(playerGuid, 0);
            packet.ReadXORByte(playerGuid, 4);
            packet.ReadXORByte(battlegroundGuid, 0);
            packet.ReadXORByte(battlegroundGuid, 6);
            packet.ReadXORByte(battlegroundGuid, 7);
            packet.ReadXORByte(playerGuid, 6);
            packet.ReadXORByte(playerGuid, 7);

            packet.WriteGuid("Battleground GUID", battlegroundGuid);
            packet.WriteGuid("Player GUID", playerGuid);
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_STATUS_NEED_CONFIRMATION)]
        public static void HandleBattlefieldStatusNeedConfirmation(Packet packet)
        {
            var battlegroundGuid = new byte[8];
            var playerGuid = new byte[8];

            battlegroundGuid[4] = packet.ReadBit();
            battlegroundGuid[6] = packet.ReadBit();
            battlegroundGuid[1] = packet.ReadBit();
            battlegroundGuid[3] = packet.ReadBit();
            playerGuid[2] = packet.ReadBit();
            playerGuid[3] = packet.ReadBit();
            battlegroundGuid[2] = packet.ReadBit();
            playerGuid[5] = packet.ReadBit();
            var hasRole = 3 - packet.ReadBit("hasRole");
            battlegroundGuid[0] = packet.ReadBit();
            battlegroundGuid[7] = packet.ReadBit();
            battlegroundGuid[5] = packet.ReadBit();
            playerGuid[7] = packet.ReadBit();
            playerGuid[0] = packet.ReadBit();
            playerGuid[6] = packet.ReadBit();
            packet.ReadBit("isRated");
            playerGuid[1] = packet.ReadBit();
            playerGuid[4] = packet.ReadBit();

            packet.ResetBitReader();

            if (hasRole != 2)
                packet.ReadByte("Role ?");

            packet.ReadUInt32("Max Players per Team");
            packet.ReadXORByte(battlegroundGuid, 5);
            packet.ReadByte("Unk Byte");
            packet.ReadUInt32("Instance ID");
            packet.ReadXORByte(playerGuid, 0);
            packet.ReadXORByte(playerGuid, 6);
            packet.ReadUInt32("Now");
            packet.ReadByte("Min Level");
            packet.ReadXORByte(playerGuid, 1);
            packet.ReadXORByte(playerGuid, 5);
            packet.ReadXORByte(playerGuid, 2);
            packet.ReadXORByte(battlegroundGuid, 4);
            packet.ReadXORByte(playerGuid, 7);
            packet.ReadXORByte(battlegroundGuid, 1);
            packet.ReadXORByte(battlegroundGuid, 7);
            packet.ReadXORByte(battlegroundGuid, 0);
            packet.ReadUInt32("Time in join request");
            packet.ReadXORByte(battlegroundGuid, 6);
            packet.ReadXORByte(battlegroundGuid, 2);
            packet.ReadByte("Unk Byte");
            packet.ReadXORByte(playerGuid, 3);
            packet.ReadXORByte(playerGuid, 4);
            packet.ReadUInt32("Time Left");
            packet.ReadUInt32("Queue slot");
            packet.ReadXORByte(battlegroundGuid, 3);

            packet.WriteGuid("Battleground GUID", battlegroundGuid);
            packet.WriteGuid("Player GUID", playerGuid);
        }

        [Parser(Opcode.CMSG_SET_ACTION_BUTTON)]
        public static void HandleSetActionButton(Packet packet)
        {
            packet.ReadByte("Slot Id");
            var actionId = packet.StartBitStream(0, 4, 7, 2, 5, 3, 1, 6);
            packet.ParseBitStream(actionId, 7, 3, 0, 2, 1, 4, 5, 6);
            packet.WriteLine("Action Id: {0}", BitConverter.ToUInt32(actionId, 0));
        }

        [Parser(Opcode.CMSG_SET_SELECTION)]
        public static void HandleSetSelection(Packet packet)
        {
            var guid = packet.StartBitStream(1, 3, 4, 6, 0, 5, 7, 2);
            packet.ParseBitStream(guid, 7, 6, 0, 2, 3, 1, 4, 5);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_REALM_SPLIT)]
        public static void HandleServerRealmSplit(Packet packet)
        {
            var len = packet.ReadBits(7);
            packet.ReadWoWString("Split Date", len);
            packet.ReadUInt32("Client State");
            packet.ReadUInt32("Split State");
        }

        [Parser(Opcode.CMSG_INSPECT)]
        public static void HandleClientInspect(Packet packet)
        {
            var guid = new byte[8];

            guid[7] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[1] = packet.ReadBit();


            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 3);

            packet.WriteGuid("Player GUID: ", guid);
        }

        [Parser(Opcode.CMSG_INSPECT_HONOR_STATS)]
        public static void HandleClientInspectHonorStats(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 2, 3, 6, 0, 1, 5, 4, 7);
            packet.ParseBitStream(guid, 1, 2, 6, 4, 7, 0, 3, 5);
            packet.WriteGuid("Player GUID: ", guid);
        }

        [Parser(Opcode.SMSG_WEATHER)]
        public static void HandleWeatherStatus(Packet packet)
        {
            packet.ReadEnum<WeatherState>("State", TypeCode.Int32);
            packet.ReadSingle("Grade");
            packet.ReadBit("Unk bit");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_NEW_WORLD)]
        public static void HandleNewWorld434(Packet packet)
        {
            var pos = new Vector4();
            pos.O = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            CoreParsers.MovementHandler.CurrentMapId = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map");
            packet.WriteLine("Position: {0}", pos);

            packet.AddSniffData(StoreNameType.Map, (int)CoreParsers.MovementHandler.CurrentMapId, "NEW_WORLD");
        }

        [Parser(Opcode.CMSG_AREATRIGGER)]
        public static void HandleClientAreaTrigger(Packet packet)
        {
            packet.ReadInt32("Area Trigger Id");
            packet.ReadBit("Unk bit1");
            packet.ReadBit("Unk bit2");
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS)]
        public static void HandleFeatureSystemStatus(Packet packet)
        {
            packet.ReadInt32("Scroll of Resurrections Remaining");
            packet.ReadInt32("Realm Id?");
            packet.ReadByte("Complain System Status");
            packet.ReadInt32("Unused Int32");
            packet.ReadInt32("Scroll of Resurrections Per Day");
            var sessionTimeAlert = packet.ReadBit("Session Time Alert");
            packet.ReadBit("IsVoiceChatAllowedByServer");
            packet.ReadBit("Scroll of Resurrection Enabled");
            packet.ReadBit("GMItemRestorationButtonEnabled");
            var quickTicket = packet.ReadBit("EuropaTicketSystemEnabled");
            packet.ReadBit("HasTravelPass");
            packet.ReadBit("Something with web ticket");

            if (quickTicket)
            {
                packet.ReadInt32("Unk5");
                packet.ReadInt32("Unk6");
                packet.ReadInt32("Unk7");
                packet.ReadInt32("Unk8");
            }

            if (sessionTimeAlert)
            {
                packet.ReadInt32("Session Alert Period");
                packet.ReadInt32("Session Alert DisplayTime");
                packet.ReadInt32("Session Alert Delay");
            }
        }

        [Parser(Opcode.SMSG_HOTFIX_INFO)]
        public static void HandleHotfixInfo(Packet packet)
        {
            var count = packet.ReadBits("Count", 20);

            for (var i = 0; i < count; ++i)
            {
                packet.ReadInt32("Hotfixed entry", i);
                packet.ReadEnum<DB2Hash>("Hotfix DB2 File", TypeCode.UInt32, i);
                packet.ReadTime("Hotfix date", i);
            }
        }

        [Parser(Opcode.SMSG_SEND_SERVER_LOCATION)]
        public static void HandleSendServerLocation(Packet packet)
        {
            packet.ReadBits("Unk Bits", 9);
            packet.ReadCString("Server Location");
        }

        [Parser(Opcode.CMSG_UNKNOWN_2979)]
        public static void HandleUnknow2979(Packet packet)
        {
            packet.ReadBits("String length", 9);
            packet.ReadCString("File");
        }
    }
}
