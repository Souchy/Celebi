namespace souchy.celebi.eevee.face.util
{
    public readonly struct IID
    {
        private string value { get; init; }

        public IID(string value)
        {
            this.value = value;
        }

        public override string ToString() => value;

        public override bool Equals(object obj)
        {
            if(obj == null) return false;
            if(obj.GetType() != typeof(IID)) return false;
            IID id = (IID) obj;
            return id.value == this.value;
        }


        public static implicit operator string(IID iid) => iid.ToString();
        public static explicit operator IID(string str) => new IID(str);

        // int conversion is only for UidGenerator. MongoIDs wouldn't go through this
        public static implicit operator int(IID i) => int.Parse(i.value);
        public static explicit operator IID(int i) => new IID(i.ToString());

        public static bool operator ==(IID leftSide, IID rightSide) =>  object.Equals(leftSide, rightSide);
        public static bool operator !=(IID leftSide, IID rightSide) => !(leftSide == rightSide);

    }
}
