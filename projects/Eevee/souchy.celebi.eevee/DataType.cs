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
    public interface DataType { }
    public class DataTypeObjectId : DataType
    {
        public ObjectId value { get; set; }
    }
    public class DataTypeInt : DataType
    {
        public int value { get; set; }
    }
    public class DataTypeMinMax : DataType
    {
        public int min { get; set; }
        public int max { get; set; }
    }
    public class DataTypeBool : DataType
    {
        public bool value { get; set; }
    }
    public class DataTypString : DataType
    {
        public string value { get; set; }
    }
    public class DataTypeDictionary : DataType
    {
        public Dictionary<DataType, DataType> values { get; set; } = new Dictionary<DataType, DataType>();
    }
    public class DataTypeList : DataType
    {
        public List<DataType> values { get; set; } = new List<DataType>();
    }
}
