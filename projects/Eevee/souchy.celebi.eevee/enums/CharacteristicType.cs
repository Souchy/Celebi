using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.enums.characs
{
    public record CharacteristicType<T>(CharacteristicCategory category, int localId) where T : IStat
    {
        public readonly IID id = (IID) ((int) category * 100 + localId);
        //public override string ToString()
        //{
        //    return category + " " + id;
        //}
    }

    public class CreatureStats
    {
        private Dictionary<IID, IStat> stats = new();
        // -1 = full every turn, 0 = no regen, 1 = 1/turn, 0.25 = 1 per 4 turns // string to parse for different regens
        //private readonly Dictionary<string, IValue<int>> resourceRegen = new();
        // gain x every turn
        private readonly Dictionary<IID, IValue<int>> growth = new();

        public T get<T>(CharacteristicType<T> ct) where T : IStat
        {
            return (T) stats[ct.id];
        }
        public T get<T>(IID id) where T : IStat
        {
            return (T) stats[id];
        }
        public int getGrowth<T>(CharacteristicType<T> ct) where T : IStat
        {
            return growth[ct.id].value;
        }
        public void applyRegen()
        {
            foreach(var res in Enum.GetValues<ResourceEnum>())
            {
                var life = get(Resource.getKey(res, ResourceProperty.Current));
                var regen = get(Resource.getKey(res, ResourceProperty.Regen));
                life.value += regen.value;
            }
        }
        public void applyGrowth()
        {
            foreach (var key in growth.Keys)
            {
                var grow = growth[key].value;
                var charac = get<IStatSimple>(key);
                charac.value += grow;
                // if StatResource : current += grow & max += grow
            }
        }
    }

    public enum CharacteristicCategory
    {
        Resource    = 1,
        Affinity    = 2,
        Resistance  = 3,
        State       = 4,
        Contextual  = 5
    }

    public enum ResourceEnum
    {
        Life,
        Mana,
        Movement,
        Summon,
        Rage
    }

    public enum ResourceProperty
    {
        Current,
        Max,
        Missing,
        Regen,
        Percent,
        MissingPercent
    }


    public sealed record Resource1(int localId) : CharacteristicType<IStatSimple>(CharacteristicCategory.Resource, localId)
    {
        public static readonly Resource1 Life                   = new((int) ResourceEnum.Life);
        public static readonly Resource1 Mana                   = new((int) ResourceEnum.Mana);
        public static readonly Resource1 Movement               = new((int) ResourceEnum.Movement);
        public static readonly Resource1 Summon                 = new((int) ResourceEnum.Summon);
        public static readonly Resource1 Rage                   = new((int) ResourceEnum.Rage);
        public static void generateKeys()
        {
            var props = Enum.GetValues<ResourceProperty>();
            var ress = Enum.GetValues<ResourceEnum>();
            var ids = ress.SelectMany(r => props.Select(p => (int) r * props.Length + (int) p));
            // + categoryid * 100

            //foreach (var res in ress)
            //{
            //    foreach (var prop in props)
            //    {
            //        int index = (int) res * props.Length + (int) prop;

            //    }
            //}
        }
    }

    public sealed record Resource(int localId) : CharacteristicType<IStatSimple>(CharacteristicCategory.Resource, localId)
    {
        // peut-être comme ça à la place (IStatSimple pour chaque valeur)
        public static readonly Resource Life                    = new(0);
        public static readonly Resource LifeMax                 = new(1);
        public static readonly Resource LifeRegen               = new(2);
        public static readonly Resource LifeMissing             = new(3); 
        public static readonly Resource LifePercent             = new(4); 
        public static readonly Resource LifeMissingPercent      = new(5);

        public static readonly Resource Mana                    = new(6);
        public static readonly Resource ManaMax                 = new(7);
        public static readonly Resource ManaRegen               = new(8);
        public static readonly Resource ManaMissing             = new(9);
        public static readonly Resource ManaPercent             = new(10);
        public static readonly Resource ManaMissingPercent      = new(11);

        public static readonly Resource Movement                = new(12);
        public static readonly Resource MovementMax             = new(13);
        public static readonly Resource MovementRegen           = new(14);
        public static readonly Resource MovementMissing         = new(15);
        public static readonly Resource MovementPercent         = new(16);
        public static readonly Resource MovementMissingPercent  = new(17);

        public static readonly Resource Summon                  = new(18);
        public static readonly Resource SummonMax               = new(19);
        public static readonly Resource SummonRegen             = new(20);
        public static readonly Resource SummonMissing           = new(21);
        public static readonly Resource SummonPercent           = new(22);
        public static readonly Resource SummonMissingPercent    = new(23);

        public static readonly Dictionary<int, Resource> values = new();
        static Resource()
        {
            var fields = typeof(Resource).GetFields();
            foreach (var field in fields)
            {
                var value = (Resource) field.GetValue(null);
                values[value.id] = value;
            }
        }
        public static Resource getById(int id)
        {
            return values[id];
        }
        public static Resource getKey(ResourceEnum res, ResourceProperty prop)
        {
            var properties = Enum.GetValues<ResourceProperty>();
            int i = (int) res * properties.Length + (int) prop;
            return getById(i);
        }
        public static void forPropertyEachResource(ResourceProperty prop, Action<ResourceEnum, Resource> actionForKey)
        {
            var resources = Enum.GetValues<ResourceEnum>();
            var properties = Enum.GetValues<ResourceProperty>();
            foreach (var res in resources) 
            {
                int i = (int) res * properties.Length + (int) prop;
                actionForKey(res, getById(i));
            }
        }
    }

    public sealed record Affinity(int localId) : CharacteristicType<IStatSimple>(CharacteristicCategory.Affinity, localId)
    {
        public static readonly Affinity Fire                = new(1);
        public static readonly Affinity Water               = new(2);
        public static readonly Affinity Earth               = new(3);
        public static readonly Affinity Air                 = new(4);
                                                                
        public static readonly Affinity Damage              = new(5);
        public static readonly Affinity Heal                = new(6);
        public static readonly Affinity Melee               = new(7);
        public static readonly Affinity Distance            = new(8);
        public static readonly Affinity Trap                = new(9);
        public static readonly Affinity Glyph               = new(10);
    }

    public sealed record Resistance(int localId) : CharacteristicType<IStatSimple>(CharacteristicCategory.Resistance, localId)
    {
        public static readonly Resistance Fire              = new(1);
        public static readonly Resistance Water             = new(2);
        public static readonly Resistance Earth             = new(3);
        public static readonly Resistance Air               = new(4);

        public static readonly Resistance Damage            = new(5);
        public static readonly Resistance Heal              = new(6);
        public static readonly Resistance Melee             = new(7);
        public static readonly Resistance Distance          = new(8);
        public static readonly Resistance Trap              = new(9);
        public static readonly Resistance Glyph             = new(10);
    }

    public sealed record Contextual(int localId) : CharacteristicType<IStatSimple>(CharacteristicCategory.Contextual, localId)
    {
        public static readonly Contextual LifeGained        = new(1);
        public static readonly Contextual LifeLost          = new(2);
        public static readonly Contextual ManaGained        = new(3);
        public static readonly Contextual ManaUsed          = new(4);
        public static readonly Contextual ManaLost          = new(5);
        public static readonly Contextual MovementGained    = new(6);
        public static readonly Contextual MovementUsed      = new(7);
        public static readonly Contextual MovementLost      = new(8);

        public static readonly Contextual CountHitsGiven    = new(9);
        public static readonly Contextual CountHitsReceived = new(10);
    }
    public sealed record State(int localId) : CharacteristicType<IStatBool>(CharacteristicCategory.State, localId) 
    {
        public static readonly State Visible                = new(1);
        public static readonly State Ghosted                = new(2); // phasing // is 30% opacity, unlocks line of sight, but blocks movement

        public static readonly State Flying                 = new(3); // slash Hovering
        public static readonly State Underground            = new(4);

        public static readonly State Drenched               = new(5); // wet
        public static readonly State Shocked                = new(6); // 
        public static readonly State Hot                    = new(7); // burning
        public static readonly State Grounded               = new(8); // Muddy
    
        public static readonly State Unmoveable             = new(9);  // everything 
        public static readonly State Rooted                 = new(10); // can't translate (dash/push/pull)
        public static readonly State Gravity                = new(11); // can't teleport
        public static readonly State Heavy                  = new(12); // carry/throw

        public static readonly State Carrying               = new(13);
        public static readonly State Carried                = new(14);

        public static readonly State Pacifist               = new(15);
    }



}
