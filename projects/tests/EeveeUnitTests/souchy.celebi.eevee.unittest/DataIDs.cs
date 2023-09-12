using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EeveeUnitTests.souchy.celebi.eevee.unittest
{
    public static class CreatureIDs
    {
        public static readonly ObjectId amelia = new ObjectId("646a8b69ea5ee922a0d0f18e");
        public static readonly ObjectId berzerker = new ObjectId("646adace502ddf02ec47b948");
        public static readonly ObjectId aurelia = new ObjectId("646ae245502ddf02ec47c009");
        public static readonly ObjectId lancelot = new ObjectId("646fdefd5f4d6650d27ecb7f");
        public static readonly ObjectId flora = new ObjectId("646ffb8fc062e368390d24bb");
    }
    public static class SpellIDs
    {
        public static readonly ObjectId fireball = new ObjectId("646a933fea5ee922a0d0f1eb");
        public static readonly ObjectId jumpkick = new ObjectId("646ac998fd2b5121f0d0d0ce");
        public static readonly ObjectId chainhook = new ObjectId("646adae7502ddf02ec47b985");
        public static readonly ObjectId chainpull = new ObjectId("646addf9502ddf02ec47bb68");
        public static readonly ObjectId seedthrow = new ObjectId("647000ef2e3d7772e906afc9");
        public static readonly ObjectId vinewhipe = new ObjectId("6470e603f81562b342d5d972");
        public static readonly ObjectId entangle = new ObjectId("6470ea2cf81562b342d5db27");
        public static readonly ObjectId bloom = new ObjectId("6470eafef81562b342d5ddec");
    }
}
