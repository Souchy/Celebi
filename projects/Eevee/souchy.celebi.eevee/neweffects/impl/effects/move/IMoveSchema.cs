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
    public abstract record IMoveSchema() : IEffectSchema
    {

        // Dont need an actorType to know who to apply the effect to
        // because we use the TargetAcquisitionZone to get targets (could be the caster)
        // then use the MoveTargetZone to say where we want to move to


        /// <summary>
        ///  Reference / Move Target zone, usually a point on the caster or on the spell's target cell  <br></br>
        ///  <b> ** This is where we pull to or push from, etc ** </b>
        ///  
        ///  <para></para>
        ///  
        ///  ex: <br></br>
        ///     - pulsar push from target (middle) <br></br>
        ///     - piège magnétique pulls to target (middle) <br></br>
        ///     - aimantation/croisement pulls to target (middle) <br></br>
        ///     
        ///     - condensation pulls to caster <br></br>
        ///     - attirance pulls to caster <br></br>
        ///     - ravage dashs to target (dont need ref for that) <br></br>
        ///     
        ///     - odyssé/octave dash away from target ("recule de x cases") <br></br>
        ///     
        ///     - flèche dispersion push from target (middle) <br></br>
        ///     - fleche concentration/miroir aux alouettes pulls to target <br></br>
        ///     - flèche recul/répulsion push from caster <br></br>
        ///     
        ///     - tribulation/commotion pulls to target <br></br>
        ///     - thérapie/mépris pulls to caster <br></br>
        ///     - convulsion/brimade pushes from target <br></br>
        ///     - point fulgu/sermon push from caster <br></br>
        ///     - insolence dash away <br></br>
        ///     - fouet pulls summon to target (summon filter is in the target acquisition zone, not the ref zone) <br></br>
        ///     
        ///     - guet-apens pulls towards the sram's double (64po aoe, filter double's creatureModel) <br></br>
        ///         - dofus fait: <br></br>
        ///             - état GA sur la cible <br></br>
        ///             - cast GA sur la map 64po, filter double du caster <br></br>
        ///             - double cast GA sur la map 64po, filter état GA <br></br>
        ///             - enlève état GA <br></br>
        /// </summary>
        public IZone MoveTargetZone { get; set; } = new Zone();

    }
}
