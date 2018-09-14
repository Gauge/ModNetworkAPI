﻿using Sandbox.ModAPI;
using System;
using VRage.Utils;
using VRageMath;

namespace ModNetworkAPI
{
    public class Client : NetworkAPI
    {

        /// <summary>
        /// Handles communication with the server
        /// </summary>
        /// <param name="comId">Identifies the channel to pass information to and from this mod</param>
        /// <param name="keyword">identifies what chat entries should be captured and sent to the server</param>
        public Client(ushort comId, string keyword = null) : base(comId, keyword)
        {
        }

        public override void SendCommand(string commandString, string message = null, object data = null, ulong steamId = ulong.MinValue, bool isReliable = true)
        {
            if (MyAPIGateway.Session?.Player != null)
            {
                Command cmd = new Command() { CommandString = commandString, Message = message, Data = data, SteamId = MyAPIGateway.Session.Player.SteamUserId };
                byte[] packet = ((object)cmd) as byte[];

                MyAPIGateway.Multiplayer.SendMessageToServer(ComId, packet, isReliable);
            }
            else
            {
                MyLog.Default.Warning($"[NetworkAPI] ComID:{ComId} | Failed to send command. Session does not exist.");
            }
        }
    }
}
