using Godot;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.espeon.godot
{
    public partial class PlayerNode : Node
    {
        public PlayerNode(int id)
        {
            SetMultiplayerAuthority(id, true);
        }


        [Rpc(MultiplayerApi.RpcMode.AnyPeer)]
        public void getFightData()
        {

        }

        [Rpc(MultiplayerApi.RpcMode.Authority)]
        public void actionCastSpell(IID spellId, IID cellid)
        {

        }

        [Rpc(MultiplayerApi.RpcMode.Authority)]
        public void actionWalkTo(IID cellid)
        {

        }

        [Rpc(MultiplayerApi.RpcMode.Authority)]
        public void actionPassTurn(IID cellid)
        {

        }

        [Rpc(MultiplayerApi.RpcMode.Authority)]
        public void actionForfeit(IID cellid)
        {

        }

    }
}
