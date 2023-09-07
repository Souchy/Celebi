using Microsoft.Extensions.FileSystemGlobbing;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.values;

namespace souchy.celebi.eevee.concepts
{
    public enum ValueFetchMode
    {
        singleTarget,
        sum,
        avg,
        first,
        last,
        min,
        max,
        any,
        all,
        none,
    }

    public class IValueContextFetcher<T> : IValue<T>
    {
        public T value { get; set; }
        public ValueFetchMode mode { get; set; }
        public ObjectId effectId { get; set; }
        public string resultPropertyName { get; set; }
    }

    public static class ValueFetcher
    {

        public static T get<T>(IAction action, IValue<T> ivalue)
        {
            if(ivalue is IValueContextFetcher<int> fetcherInt)
            {
                T result = (T) (object) fetchInt(action, fetcherInt); 
                return result;
            }
            else
            if (ivalue is IValueContextFetcher<bool> fetcherBool)
            {
                return (T) (object) fetchBool(action, fetcherBool);
            }
            else
            {
                return ivalue.value;
            }
        }
        private static int fetchInt(IAction action, IValueContextFetcher<int> fetcher)
        {
            var results = action.context.getReturnsForEffect(fetcher.effectId).Select(r => ((DataTypeInt) r.data).value);
            var result = fetcher.mode switch
            {
                ValueFetchMode.singleTarget => results.First(),
                ValueFetchMode.sum => results.Sum(),
                ValueFetchMode.avg => (int) Math.Floor(results.Average()),
                ValueFetchMode.first => results.First(),
                ValueFetchMode.last => results.Last(),
                ValueFetchMode.min => results.Min(),
                ValueFetchMode.max => results.Max(),
                ValueFetchMode.any => throw new NotImplementedException(),
                ValueFetchMode.all => throw new NotImplementedException(),
                ValueFetchMode.none => throw new NotImplementedException(),
            };
            return result;
        }
        private static bool fetchBool(IAction action, IValueContextFetcher<bool> fetcher)
        {
            var results = action.context.getReturnsForEffect(fetcher.effectId).Select(r => ((DataTypeBool) r.data).value);
            var result = fetcher.mode switch
            {
                ValueFetchMode.singleTarget => results.First(),
                ValueFetchMode.sum => throw new NotImplementedException(),
                ValueFetchMode.avg => throw new NotImplementedException(),
                ValueFetchMode.first => results.First(),
                ValueFetchMode.last => results.Last(),
                ValueFetchMode.min => results.Min(),
                ValueFetchMode.max => results.Max(),
                ValueFetchMode.any => results.Any(r => r == true),
                ValueFetchMode.all => results.All(r => r == true),
                ValueFetchMode.none => results.All(r => r == false),
            };
            return result;
        }
    }


}
