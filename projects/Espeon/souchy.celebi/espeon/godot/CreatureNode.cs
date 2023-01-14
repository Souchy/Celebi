using Espeon.souchy.celebi.espeon.eevee.impl.objects;
using Godot;
using souchy.celebi.eevee;

namespace Espeon.souchy.celebi.espeon.godot
{
    public partial class CreatureNode : Node
    {
        [Export]
        public Creature Creature { get; set; }
    }
}
