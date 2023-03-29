using souchy.celebi.eevee.face.objects.stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.enums.characteristics.creature
{
    public sealed record Resource1(int localId) : CharacteristicType<IStatSimple>(CharacteristicCategory.Resource, localId)
    {
        public static readonly Resource1 Life = new((int)ResourceEnum.Life);
        public static readonly Resource1 Mana = new((int)ResourceEnum.Mana);
        public static readonly Resource1 Movement = new((int)ResourceEnum.Movement);
        public static readonly Resource1 Summon = new((int)ResourceEnum.Summon);
        public static readonly Resource1 Rage = new((int)ResourceEnum.Rage);
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

    public sealed record Resource(int localId) : CharacteristicType<IStatSimple>(CharacteristicCategory.Resource, localId)
    {
        // peut-être comme ça à la place (IStatSimple pour chaque valeur)
        public static readonly Resource BaseLifeMax         = new(0);
        public static readonly Resource Life                = new(1);
        public static readonly Resource LifeMax             = new(2);
        public static readonly Resource LifeRegen           = new(3);
        public static readonly Resource LifeMissing         = new(4);
        public static readonly Resource LifePercent         = new(5);
        public static readonly Resource LifeMissingPercent  = new(6);

        public static readonly Resource BaseManaMax         = new(7);
        public static readonly Resource Mana                = new(8);
        public static readonly Resource ManaMax             = new(9);
        public static readonly Resource ManaRegen           = new(10);
        public static readonly Resource ManaMissing         = new(11);
        public static readonly Resource ManaPercent         = new(12);
        public static readonly Resource ManaMissingPercent  = new(13);

        public static readonly Resource BaseMovementMax     = new(14);
        public static readonly Resource Movement            = new(15);
        public static readonly Resource MovementMax         = new(16);
        public static readonly Resource MovementRegen       = new(17);
        public static readonly Resource MovementMissing     = new(18);
        public static readonly Resource MovementPercent     = new(19);
        public static readonly Resource MovementMissingPercent = new(20);

        public static readonly Resource BaseSummonMax       = new(21);
        public static readonly Resource Summon              = new(22);
        public static readonly Resource SummonMax           = new(23);
        public static readonly Resource SummonRegen         = new(24);
        public static readonly Resource SummonMissing       = new(25);
        public static readonly Resource SummonPercent       = new(26);
        public static readonly Resource SummonMissingPercent = new(27);

        public static readonly Resource BaseRageMax         = new(28);
        public static readonly Resource Rage                = new(29);
        public static readonly Resource RageMax             = new(30);
        public static readonly Resource RageRegen           = new(31);
        public static readonly Resource RageMissing         = new(34);
        public static readonly Resource RagePercent         = new(35);
        public static readonly Resource RageMissingPercent  = new(36);

        public static readonly Resource BaseShield          = new(37);
        public static readonly Resource Shield              = new(38);
        public static readonly Resource ShieldRegen         = new(39);

        public static readonly Dictionary<int, Resource> values = new();
        static Resource()
        {
            var fields = typeof(Resource).GetFields();
            foreach (var field in fields)
            {
                var value = (Resource)field.GetValue(null);
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
            int i = (int)res * properties.Length + (int)prop;
            return getById(i);
        }
        public static void forPropertyEachResource(ResourceProperty prop, Action<ResourceEnum, Resource> actionForKey)
        {
            var resources = Enum.GetValues<ResourceEnum>();
            var properties = Enum.GetValues<ResourceProperty>();
            foreach (var res in resources)
            {
                int i = (int)res * properties.Length + (int)prop;
                actionForKey(res, getById(i));
            }
        }
    }
}
