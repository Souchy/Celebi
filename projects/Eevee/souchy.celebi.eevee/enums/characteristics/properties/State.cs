using souchy.celebi.eevee.face.objects.stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.enums.characteristics.creature
{
    public sealed record State(int localId) : CharacteristicType<IStatBool>(CharacteristicCategory.State, localId)
    {
        public static readonly State BaseVisible = new(01);
        public static readonly State BaseGhosted = new(02); // phasing // is 30% opacity, unlocks line of sight, but blocks movement
        public static readonly State BaseFlying = new(03); // slash Hovering
        public static readonly State BaseUnderground = new(04);
        public static readonly State BaseDrenched = new(05); // wet
        public static readonly State BaseShocked = new(06); // 
        public static readonly State BaseHot = new(07); // burning
        public static readonly State BaseGrounded = new(08); // Muddy
        public static readonly State BaseUnmoveable = new(09); // everything 
        public static readonly State BaseRooted = new(10); // can't translate (dash/push/pull)
        public static readonly State BaseGravity = new(11); // can't teleport
        public static readonly State BaseHeavy = new(12); // carry/throw
        public static readonly State BaseCarrying = new(13);
        public static readonly State BaseCarried = new(14);
        public static readonly State BasePacifist = new(15);


        public static readonly State Visible = new(16);
        public static readonly State Ghosted = new(17); // phasing // is 30% opacity, unlocks line of sight, but blocks movement
        public static readonly State Flying = new(18); // slash Hovering
        public static readonly State Underground = new(19);
        public static readonly State Drenched = new(20); // wet
        public static readonly State Shocked = new(21); // 
        public static readonly State Hot = new(22); // burning
        public static readonly State Grounded = new(23); // Muddy
        public static readonly State Unmoveable = new(24); // everything 
        public static readonly State Rooted = new(25); // can't translate (dash/push/pull)
        public static readonly State Gravity = new(26); // can't teleport
        public static readonly State Heavy = new(27); // carry/throw
        public static readonly State Carrying = new(28);
        public static readonly State Carried = new(29);
        public static readonly State Pacifist = new(30);


        public static readonly Dictionary<int, State> values = new();
        static State()
        {
            var fields = typeof(State).GetFields();
            foreach (var field in fields)
            {
                var value = (State) field.GetValue(null);
                values[value.id] = value;
            }
        }
    }
}
