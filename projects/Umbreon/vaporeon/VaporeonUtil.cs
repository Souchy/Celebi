using Godot;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbreon.vaporeon
{
    public static class VaporeonSignals
    {
        public static readonly string save = "vaporeon:save";
        /// <summary>
        /// Params: [Control selector, object selectedItem]
        /// </summary>
        public const string select = "vaporeon:select";

    }

    public static class VaporeonUtil
    {
        #region Const
        public const string metaIID = "iid";
        public static List<Type> effectTypes { get; } = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => typeof(IEffect).IsAssignableFrom(p))
            .Where(t => !t.IsInterface && t != typeof(Effect))
            .ToList();
        #endregion

        #region Extensions
        public static Color Random(this Color color)
        {
            var rnd = new Random();
            color.R = rnd.NextSingle();
            color.G = rnd.NextSingle();
            color.B = rnd.NextSingle();
            color.A = 1;
            return color;
        }
        public static void Remove<TKey, TValue>(this IDictionary<TKey, TValue> dict,
            Func<TValue, bool> predicate)
        {
            var keys = dict.Keys.Where(k => predicate(dict[k])).ToList();
            foreach (var key in keys)
            {
                dict.Remove(key);
            }
        }
        public static IID GetMetaIID(this Control control)
            => (IID) control.GetMeta(metaIID).AsString();
        #endregion
    }
}
