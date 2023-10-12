using souchy.celebi.eevee.impl.values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee
{

    /// <summary>
    /// DataTypes used in Celebi for any value. Stats, Conditions, Triggers, etc.
    /// DataTypes can contain other DataTypes in lists or dictionaries.
    /// <para></para>
    /// In reality idk if I'll be able to integrate it in everything (statsDictionary, etc), 
    ///     but at least for now it will work for Trigger filters
    /// </summary>
    public interface DataType {
        public DataType copy();
    }
    public class DataTypeObjectId : DataType
    {
        public ObjectId value { get; set; }

        public DataType copy() 
            => new DataTypeObjectId()
            {
                value = value,
            };
    }
    public class DataTypeInt : DataType
    {
        public int value { get; set; }
        public DataType copy()
            => new DataTypeInt()
            {
                value = value,
            };
    }
    public class DataTypeMinMax : DataType
    {
        public int min { get; set; }
        public int max { get; set; }
        public DataType copy()
            => new DataTypeMinMax()
            {
                min = min,
                max = max,
            };
    }
    public class DataTypeBool : DataType
    {
        public bool value { get; set; }

        public DataType copy()
            => new DataTypeBool()
            {
                value = value,
            };
    }
    public class DataTypString : DataType
    {
        public string value { get; set; }

        public DataType copy()
            => new DataTypString()
            {
                value = value,
            };
    }
    public class DataTypeDictionary : DataType
    {
        public Dictionary<DataType, DataType> values { get; set; } = new();

        public DataType copy()
        {
            var copy = new DataTypeDictionary();
            foreach(var v in values)
            {
                copy.values.Add(v.Key.copy(), v.Value.copy());
            }
            return copy;
        }
    }
    public class DataTypeList : DataType
    {
        public List<DataType> values { get; set; } = new();

        public DataType copy()
        {
            var copy = new DataTypeList();
            foreach (var v in values)
            {
                copy.values.Add(v.copy());
            }
            return copy;
        }
    }
}
