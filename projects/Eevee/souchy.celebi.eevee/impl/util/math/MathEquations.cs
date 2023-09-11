using Newtonsoft.Json;

namespace souchy.celebi.eevee.impl.util.math
{
    public class MathEquation
    {
        public static MathEquation CreateConstant(double value)
        {
            return new MathEquation()
            {
                functions = { new MathFunction() {
                    slopes = new double[] { value }
                } }
            };
        }

        [JsonProperty]
        private List<MathFunction> functions = new List<MathFunction>();
        public double get(int x)
        {
            var curve = functions.Find(f => f.xFromIncluded <= x && x < f.xToExcluded);
            return curve.get(x);
        }
        public int getAsInt(int x)
        {
            return (int) get(x);
        }
        public bool getAsBool(int x)
        {
            return get(x) >= 1;
        }
        public bool getAsBool(int x, bool baseBool)
        {
            return ((baseBool ? 1 : 0) + get(x)) >= 1;
        }
        public MathEquation copy()
        {
            var copy = new MathEquation();
            foreach(var func in functions)
            {
                copy.functions.Add(func.copy());   
            }
            return copy;
        }
    }

    public class MathFunction
    {
        [JsonProperty]
        internal int xFromIncluded { get; set; } = int.MinValue;
        [JsonProperty]
        internal int xToExcluded { get; set; } = int.MaxValue;
        /// <summary>
        /// [0] = Y constant (x^0=1)
        /// [1] = s * x (x^1=x)
        /// </summary>
        [JsonProperty]
        internal double[] slopes { get; set; } = new double[1];
        public double get(int x)
        {
            double total = 0;
            for (int i = 0; i < slopes.Length; i++)
            {
                total += slopes[i] * Math.Pow(x, i);
            }
            return total;
        }
        public MathFunction copy()
        {
            var copy = new MathFunction();
            copy.xFromIncluded = xFromIncluded;
            copy.xToExcluded = xToExcluded;
            slopes.CopyTo(copy.slopes, 0);
            return copy;
        }
    }

}
