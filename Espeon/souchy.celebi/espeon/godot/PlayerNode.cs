using Godot;
using souchy.celebi.eevee.face.util;

namespace Espeon.souchy.celebi.espeon.godot
{
    public partial class PlayerNode : Node
    {

        public PlayerNode(int id)
        {
            this.SetMultiplayerAuthority(id, true);
        }


        [RPC(MultiplayerAPI.RPCMode.AnyPeer)]
        public void getFightData()
        {

        }

        [RPC(MultiplayerAPI.RPCMode.Authority)]
        public void actionCastSpell(IID spellId, IID cellid)
        {

        }

        [RPC(MultiplayerAPI.RPCMode.Authority)]
        public void actionWalkTo(IID cellid)
        {

        }

        [RPC(MultiplayerAPI.RPCMode.Authority)]
        public void actionPassTurn(IID cellid)
        {

        }

        [RPC(MultiplayerAPI.RPCMode.Authority)]
        public void actionForfeit(IID cellid)
        {

        }

    }
}
