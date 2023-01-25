namespace souchy.celebi.eevee.face.util
{
    public struct IID
    {
        public string value { get; init; }

        public IID(string value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return this.value;
        }

        public static implicit operator string(IID iid) => iid.value;
        public static explicit operator IID(string str) => new IID(str);

        // int conversion is only for UidGenerator. MongoIDs wouldn't go through this
        public static implicit operator int(IID i) => int.Parse(i.value);
        public static explicit operator IID(int i) => new IID(i.ToString());

    }
}
