using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.espeon.eevee.impl.controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EeveeUnitTests.souchy.celebi.eevee.unittest.useCases
{
    public class UseCaseBase
    {
        public void beforeAllTests()
        {
            // load db into Eevee.models
        }

        public void asdf()
        {
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
    }
}
