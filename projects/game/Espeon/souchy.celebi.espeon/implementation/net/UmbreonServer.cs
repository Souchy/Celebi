using Godot;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.espeon.implementation.net
{
    public class UmbreonServer
    {

        [Rpc(MultiplayerApi.RpcMode.AnyPeer)]
        public void sendDirectMessage(IID destinationUserId, string msg)
        {

        }
        [Rpc(MultiplayerApi.RpcMode.AnyPeer)]
        public void sendMessage(MessageChannelType channel, string msg)
        {

        }


        [Rpc(MultiplayerApi.RpcMode.AnyPeer)]
        public void requestJoinFight(IID fightID)
        {

        }
        [Rpc(MultiplayerApi.RpcMode.Authority, CallLocal = true)]
        public void sendJoinFight(IID fightID) // int peerId ?
        {

        }

        public void getFightData(IID fightID)
        {

        }
        public void sendFightData(IID fightID)
        {

        }


        [Rpc(MultiplayerApi.RpcMode.AnyPeer)]
        public void requestUserInfo(IID userID)
        {

        }
        [Rpc(MultiplayerApi.RpcMode.Authority, CallLocal = true)]
        public void responseUserInfo(IID userID) // int peerId ?
        {

        }

    }
}
