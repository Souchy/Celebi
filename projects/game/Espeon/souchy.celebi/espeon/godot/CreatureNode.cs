using Godot;
using souchy.celebi.eevee;
using souchy.celebi.eevee.impl.objects;

namespace Espeon.souchy.celebi.espeon.godot
{
    public partial class CreatureNode : Node
    {
        [Export]
        public Creature Creature { get; set; }
    }
}
