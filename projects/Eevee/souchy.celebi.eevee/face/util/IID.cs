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


        public static implicit operator uint(IID i) => uint.Parse(i.value);
        public static explicit operator IID(uint i) => new IID(i.ToString());
        public static explicit operator IID(int i) => new IID(i.ToString());

    }
}
