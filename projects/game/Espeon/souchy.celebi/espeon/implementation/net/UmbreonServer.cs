using Godot;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.util;

namespace Espeon.souchy.celebi.espeon.implementation.net
{
    public class UmbreonServer
    {

        [RPC(MultiplayerAPI.RPCMode.AnyPeer)]
        public void sendDirectMessage(IID destinationUserId, string msg)
        {

        }
        [RPC(MultiplayerAPI.RPCMode.AnyPeer)]
        public void sendMessage(MessageChannelType channel, string msg)
        {

        }


        [RPC(MultiplayerAPI.RPCMode.AnyPeer)]
        public void requestJoinFight(IID fightID)
        {

        }
        [RPC(MultiplayerAPI.RPCMode.Authority, CallLocal = true)]
        public void sendJoinFight(IID fightID) // int peerId ?
        {

        }

        public void getFightData(IID fightID)
        {

        }
        public void sendFightData(IID fightID)
        {

        }


        [RPC(MultiplayerAPI.RPCMode.AnyPeer)]
        public void requestUserInfo(IID userID)
        {

        }
        [RPC(MultiplayerAPI.RPCMode.Authority, CallLocal = true)]
        public void responseUserInfo(IID userID) // int peerId ?
        {

        }

    }
}
