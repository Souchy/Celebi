using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.espeon.eevee.impl.controllers;
using souchy.celebi.spark.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EeveeUnitTests.souchy.celebi.eevee.unittest.setup
{
    [CollectionDefinition("BaseCase")]
    public class UseCaseBaseCollection : ICollectionFixture<UseCaseBase>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }

    /// <summary>
    /// Defines base use case, a Fight instance.
    /// </summary>
    public class UseCaseBase : IDisposable
    {
        public Fight fight;
        public UseCaseBase(MongoModelsDbService db)
        {
            // load db into Eevee.models

            // Don't put entities automatically in the fight dictionaries because it could be a model or an instance
            //      so do it manually ig, could have a function for it.
            //      Actually, we can use .Create(fightid) for instances and .CreatePermanent() for models
            //      then register them automatically in the Fight or in Eevee
            // 

            IFight fight = new Fight();
            // new Board
            // new Timeline
            // new 2 players
            // new 6 creatures
            //      new spells / stats / passives
            // start fight/timeline

            // ---- in use case implementation:
            // cast spell
            // check results
            // pass turn
            // check results
            //

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
