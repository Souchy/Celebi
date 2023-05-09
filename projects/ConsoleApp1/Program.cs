using MongoDB.Bson;
using souchy.celebi.eevee;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.impl.stats;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.impl.util.math;

namespace PlayfabClientTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            //var asd = Eevee.uIdGenerator;
            //var breed = new Breed();
            //breed.throwEvent();
            //string txt = nameof(breed.Id);
            //Console.WriteLine($"text: {txt} or {nameof(Breed.asdf)}.");

            ////new Celebi();
            ////Celebi.common.Celebi.main(null);
            //var c = new Umbreon.common.PlayfabClientTest();
            //c.thing();

            //[
            //[-3, 0, 0],
            //[-2, 0, 0], [-2, 1, 0],
            //[-1, 0, 0], [-1, 1, 0], [-1, 2, 0],
            //[0, 0, 0], [0, 1, 0], [0, 2, 0], [0, 3, 0],
            //[1, 0, 0], [1, 1, 0], [1, 2, 0],
            //[2, 0, 0], [2, 1, 0],
            //[3, 0, 0]
            //]
            //rotate(new Vector3(-3, 0, 0));
            //rotate(new Vector3(-2, 1, 0));
            //rotate(new Vector3(-2, 2, 0));
            //rotate(new Vector3(-1, 0, 0));
            rotate(new Vector3(-1, 1, 0));
            //rotate(new Vector3(-1, 2, 0));
            //rotate(new Vector3(0, 0, 0));
            //rotate(new Vector3(0, 1, 0));
            //rotate(new Vector3(0, 2, 0));
            //rotate(new Vector3(0, 3, 0));


            Console.ReadLine();
        }
        public static void rotate(IVector3 p)
        {
            var original = p.copy();
            var unit = -90d;
            var angle = unit * Math.PI / 180;

            var xb = Math.Round(p.x * Math.Cos(angle) - p.z * Math.Sin(angle));
            var zb = Math.Round(p.z * Math.Cos(angle) + p.x * Math.Sin(angle));
            p.set((int) xb, (int) zb);
            Console.WriteLine($"original: {original}, output p: {p}, or float [{xb}, {zb}]");
        }
    }



    public class Breed : IEntity
    {
        public int Id { get; set; }
        public static string asdf = "";
        public ObjectId entityUid { get; set; } = Eevee.RegisterIIDTemporary(); //Eevee.RegisterIID<string>();
        public IStats stats = Stats.Create(); //new Stats();
        public Breed()
        {
            //stats.GetEventBus().subscribe(this); // register All
            stats.GetEntityBus().subscribe(this, "onLifeChanged", "onLifeChanged"); // register specific methods
            stats.GetEntityBus().subscribe(this, "onLifeChanged", "onLifeChanged"); // registers only once per subscribe(), but can be registered twice if subscribe() twice
            stats.GetEntityBus().unsubscribe(this); // unsubscribes all subscriptions even if registered multiple times
        }


        [Subscribe(nameof(Resource.Life))]
        public void onLifeChanged(IStats stats, IStat stat)
        {
            var simple = (StatSimple) stat;
            Console.WriteLine($"Received event stat: {simple.value}");
        }

        public void throwEvent()
        {
            stats.Add(Resource.Life.Create(5));
        }

        public void Dispose()
        {
            Eevee.DisposeEventBus(this);
        }
    }

}