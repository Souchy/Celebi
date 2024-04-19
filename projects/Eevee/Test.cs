using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee;

public interface ID
{
    public ID fromString(string str);
    public string toString();
}


public struct IDImplementation : ID
{
    public ObjectId value;
    public IDImplementation(ObjectId obj) => value = obj;
    public IDImplementation(string str) => fromString(str);
    public ID fromString(string str)
    {
        value = new ObjectId(str);
        return this;
    }

    public string toString()
    {
        return value.ToString();
    }
}