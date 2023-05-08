namespace souchy.celebi.umbreon.data
{
    public class CreatureSkinData
    {
        public int id;
        public string model;
        public string meshName;
        public int[] colorMaterials;
        public AnimationsData animations;
        public string icon;
    }

    public class AnimationsData
    {
        public string idle;
        public string run;
        public string walk;
        public string receiveHit;
        public string victory;
        public string defeat;
    }

}
