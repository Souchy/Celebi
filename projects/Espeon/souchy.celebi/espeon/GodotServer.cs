using Espeon.souchy.celebi.espeon.godot;
using Godot;
using souchy.celebi.eevee.face.util;

namespace Espeon.souchy.celebi.espeon
{
    public partial class GodotServer : Node
    {
        /// <summary>
        /// 1 is reserved for Server
        /// </summary>
        private int peerId = 2;

        private Node root { get; set; }

        public GodotServer()
        {
            root = new Node();
            // players nodes
            root.AddChild(new PlayerNode(peerId++));
            root.AddChild(new PlayerNode(peerId++)); 
            root.AddChild(new PlayerNode(peerId++));


            var server = new ENetMultiplayerPeer();
            server.CreateServer(7000);
            this.Multiplayer.MultiplayerPeer = server;

            Multiplayer.GetUniqueId();
            SetMultiplayerAuthority(1);
            var hi = IsMultiplayerAuthority();
            
        }

        [RPC(MultiplayerAPI.RPCMode.AnyPeer)]
        public void getFightData(IID fightID)
        {
            int clientId = this.Multiplayer.GetRemoteSenderId();
        }



    }
}
