namespace souchy.celebi.eevee.face.util
{
    public struct IID
    {
        public uint value { get; init; }
        public IID(uint value)
        {
            this.value = value;
        }

        public static implicit operator uint(IID i) => i.value;
        public static explicit operator IID(uint i) => new IID(i);
        public static explicit operator IID(int i) => new IID((uint)i);
    }
}
