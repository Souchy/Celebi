using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.impl.objects.zones;
using souchy.celebi.eevee.neweffects.face;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.neweffects.impl.effects.move
{
    public abstract class IMoveSchema : IEffectSchema
    {
        /// <summary>
        ///  Reference zone, usually a point on the caster or on the spell's target cell
        ///  This is where we pull to or push from, etc
        ///  ex: 
        ///     - pulsar push from target (middle)
        ///     - piège magnétique pulls to target (middle)
        ///     - aimantation/croisement pulls to target (middle)
        ///     
        ///     - condensation pulls to caster
        ///     - attirance pulls to caster
        ///     - ravage dashs to target (dont need ref for that)
        ///     
        ///     - odyssé/octave dash away from target ("recule de x cases")
        ///     
        ///     - flèche dispersion push from target (middle)
        ///     - fleche concentration/miroir aux alouettes pulls to target
        ///     - flèche recul/répulsion push from caster
        ///     
        ///     - tribulation/commotion pulls to target
        ///     - thérapie/mépris pulls to caster
        ///     - convulsion/brimade pushes from target
        ///     - point fulgu/sermon push from caster
        ///     - insolence dash away
        ///     - fouet pulls summon to target (summon filter is in the target acquisition zone, not the ref zone)
        ///     
        ///     - guet-apens pulls towards the sram's double (64po aoe, filter double's creatureModel)
        ///         - dofus fait:
        ///             - état GA sur la cible
        ///             - cast GA sur la map 64po, filter double du caster
        ///             - double cast GA sur la map 64po, filter état GA
        ///             - enlève état GA
        /// </summary>
        public IZone ReferenceZone { get; set; } = new Zone();

        //public ActorType Reference { get; set; }
    }
}
