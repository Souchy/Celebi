using souchy.celebi.eevee.face.objects.stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.enums.characteristics.creature
{
    /*
    public sealed record Resource1(int localId) : CharacteristicType(CharacteristicCategory.Resource, localId)
    {
        public static readonly Resource1 Life       = new((int)ResourceEnum.Life);
        public static readonly Resource1 Mana       = new((int)ResourceEnum.Mana);
        public static readonly Resource1 Movement   = new((int)ResourceEnum.Movement);
        public static readonly Resource1 Summon     = new((int)ResourceEnum.Summon);
        public static readonly Resource1 Rage       = new((int)ResourceEnum.Rage);
        public static void generateKeys()
        {
            var props = Enum.GetValues<ResourceProperty>();
            var ress = Enum.GetValues<ResourceEnum>();
            var ids = ress.SelectMany(r => props.Select(p => (int)r * props.Length + (int)p));
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
    */

    public sealed record Resource : CharacteristicType
    {
        public ResourceEnum resType { get; init; }
        public ResourceProperty resProp { get; init; }
        public Resource(int localId, string name, ResourceEnum resType, ResourceProperty resProp) : base(CharacteristicCategory.Resource, localId, name, SimpleFactory)
        {
            this.resProp = resProp;
            this.resType = resType;
            this.StatValueType = StatValueType.Simple;
        }

        // peut-être comme ça à la place (IStatSimple pour chaque valeur)
        //public static readonly Resource BaseLifeMax           = new(0);
        public static readonly Resource LifeInitialMax          = new(0, nameof(LifeInitialMax    ), ResourceEnum.Life, ResourceProperty.InitialMax); //maybe need this for erosion?
        public static readonly Resource Life                    = new(1, nameof(Life              ), ResourceEnum.Life, ResourceProperty.Current);
        public static readonly Resource LifeMax                 = new(2, nameof(LifeMax           ), ResourceEnum.Life, ResourceProperty.Max);
        public static readonly Resource LifeRegen               = new(3, nameof(LifeRegen         ), ResourceEnum.Life, ResourceProperty.Regen);
        public static readonly Resource LifeMissing             = new(4, nameof(LifeMissing       ), ResourceEnum.Life, ResourceProperty.Missing);
        public static readonly Resource LifePercent             = new(5, nameof(LifePercent       ), ResourceEnum.Life, ResourceProperty.Percent);
        public static readonly Resource LifeMissingPercent      = new(6, nameof(LifeMissingPercent), ResourceEnum.Life, ResourceProperty.MissingPercent);

        //public static readonly Resource BaseManaMax             = new(7);
        public static readonly Resource ManaInitialMax          = new(7,  nameof(ManaInitialMax    ), ResourceEnum.Mana, ResourceProperty.InitialMax);
        public static readonly Resource Mana                    = new(8,  nameof(Mana              ), ResourceEnum.Mana, ResourceProperty.Current);
        public static readonly Resource ManaMax                 = new(9,  nameof(ManaMax           ), ResourceEnum.Mana, ResourceProperty.Max);
        public static readonly Resource ManaRegen               = new(10, nameof(ManaRegen         ), ResourceEnum.Mana, ResourceProperty.Regen);
        public static readonly Resource ManaMissing             = new(11, nameof(ManaMissing       ), ResourceEnum.Mana, ResourceProperty.Missing);
        public static readonly Resource ManaPercent             = new(12, nameof(ManaPercent       ), ResourceEnum.Mana, ResourceProperty.Percent);
        public static readonly Resource ManaMissingPercent      = new(13, nameof(ManaMissingPercent), ResourceEnum.Mana, ResourceProperty.MissingPercent);

        //public static readonly Resource BaseMovementMax       = new(14);
        public static readonly Resource MovementInitialMax      = new(14, nameof(MovementInitialMax    ), ResourceEnum.Movement, ResourceProperty.InitialMax);
        public static readonly Resource Movement                = new(15, nameof(Movement              ), ResourceEnum.Movement, ResourceProperty.Current);
        public static readonly Resource MovementMax             = new(16, nameof(MovementMax           ), ResourceEnum.Movement, ResourceProperty.Max);
        public static readonly Resource MovementRegen           = new(17, nameof(MovementRegen         ), ResourceEnum.Movement, ResourceProperty.Regen);
        public static readonly Resource MovementMissing         = new(18, nameof(MovementMissing       ), ResourceEnum.Movement, ResourceProperty.Missing);
        public static readonly Resource MovementPercent         = new(19, nameof(MovementPercent       ), ResourceEnum.Movement, ResourceProperty.Percent);
        public static readonly Resource MovementMissingPercent  = new(20, nameof(MovementMissingPercent), ResourceEnum.Movement, ResourceProperty.MissingPercent);

        //public static readonly Resource BaseSummonMax       = new(21);
        public static readonly Resource SummonInitialMax        = new(21, nameof(SummonInitialMax    ), ResourceEnum.Summon, ResourceProperty.InitialMax);
        public static readonly Resource Summon                  = new(22, nameof(Summon              ), ResourceEnum.Summon, ResourceProperty.Current);
        public static readonly Resource SummonMax               = new(23, nameof(SummonMax           ), ResourceEnum.Summon, ResourceProperty.Max);
        public static readonly Resource SummonRegen             = new(24, nameof(SummonRegen         ), ResourceEnum.Summon, ResourceProperty.Regen);
        public static readonly Resource SummonMissing           = new(25, nameof(SummonMissing       ), ResourceEnum.Summon, ResourceProperty.Missing);
        public static readonly Resource SummonPercent           = new(26, nameof(SummonPercent       ), ResourceEnum.Summon, ResourceProperty.Percent);
        public static readonly Resource SummonMissingPercent    = new(27, nameof(SummonMissingPercent), ResourceEnum.Summon, ResourceProperty.MissingPercent);

        //public static readonly Resource BaseRageMax           = new(28);
        public static readonly Resource RageInitialMax          = new(28, nameof(RageInitialMax    ), ResourceEnum.Rage, ResourceProperty.InitialMax);
        public static readonly Resource Rage                    = new(29, nameof(Rage              ), ResourceEnum.Rage, ResourceProperty.Current);
        public static readonly Resource RageMax                 = new(30, nameof(RageMax           ), ResourceEnum.Rage, ResourceProperty.Max);
        public static readonly Resource RageRegen               = new(31, nameof(RageRegen         ), ResourceEnum.Rage, ResourceProperty.Regen);
        public static readonly Resource RageMissing             = new(34, nameof(RageMissing       ), ResourceEnum.Rage, ResourceProperty.Missing);
        public static readonly Resource RagePercent             = new(35, nameof(RagePercent       ), ResourceEnum.Rage, ResourceProperty.Percent);
        public static readonly Resource RageMissingPercent      = new(36, nameof(RageMissingPercent), ResourceEnum.Rage, ResourceProperty.MissingPercent);

        //public static readonly Resource BaseShield              = new(37);
        //public static readonly Resource ShieldIntial            = new(0, ResourceEnum.Life, ResourceProperty.InitialMax); // since theres no max, i think Shield is just the initial no?
        public static readonly Resource Shield                  = new(38, nameof(Shield     ), ResourceEnum.Shield, ResourceProperty.Current);
        public static readonly Resource ShieldRegen             = new(39, nameof(ShieldRegen), ResourceEnum.Shield, ResourceProperty.Regen);

        public static readonly Dictionary<CharacteristicId, Resource> values = new();
        static Resource()
        {
            var fields = typeof(Resource).GetFields();
            foreach (var field in fields)
            {
                var value = (Resource)field.GetValue(null);
                values[value.ID] = value;
            }
        }
        public static Resource getById(CharacteristicId id)
        {
            return values[id];
        }
        public static Resource getKey(ResourceEnum res, ResourceProperty prop)
        {
            return values.Values.FirstOrDefault(v => v.resType == res && v.resProp == prop);
        }
        public static void forPropertyEachResource(ResourceProperty prop, Action<ResourceEnum, Resource> actionForKey)
        {
            var resources = Enum.GetValues<ResourceEnum>();
            foreach (var res in resources)
            {
                actionForKey(res, getKey(res, prop));
            }
        }
    }
}
