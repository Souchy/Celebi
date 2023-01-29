using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.stats;
using souchy.celebi.eevee.impl.util;
using Umbreon.common;

namespace PlayfabClientTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var asd = Eevee.uIdGenerator;
            var breed = new Breed();
            breed.throwEvent();
            //string txt = nameof(breed.Id);
            //Console.WriteLine($"text: {txt} or {nameof(Breed.asdf)}.");

            ////new Celebi();
            ////Celebi.common.Celebi.main(null);
            //var c = new Umbreon.common.PlayfabClientTest();
            //c.thing();


            Console.ReadLine();
        }
    }



    public class Breed : IEntity
    {
        public int Id { get; set; }
        public static string asdf = "";
        public IID entityUid { get; init; } = Eevee.RegisterIID();
        public IStats stats = new Stats();
        public Breed()
        {
            //stats.GetEventBus().subscribe(this); // register All
            stats.GetEventBus().subscribe(this, "onLifeChanged", "onLifeChanged"); // register specific methods
            stats.GetEventBus().subscribe(this, "onLifeChanged", "onLifeChanged"); // registers only once per subscribe(), but can be registered twice if subscribe() twice
            stats.GetEventBus().unsubscribe(this); // unsubscribes all subscriptions even if registered multiple times
        }


        [Subscribe(nameof(StatType.Life))]
        public void onLifeChanged(IStats stats, IStat stat)
        {
            var simple = (StatSimple) stat;
            Console.WriteLine($"Received event stat: {simple.value}");
        }

        public void throwEvent()
        {
            stats.set(StatType.Life, new StatSimple()
            {
                value = 5
            });
            //this.GetEventBus().publish(nameof(StatType.Life), stats, );
        }

        public void Dispose() => Eevee.DisposeIID(this);
    }

}