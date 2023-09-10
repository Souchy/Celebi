using EeveeUnitTests.souchy.celebi.eevee.unittest.setup;
using souchy.celebi.eevee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EeveeUnitTests.souchy.celebi.eevee.unittest.useCases
{

    //[Collection("BaseCase")]
    public class TestFireball : IClassFixture<FightTest1> //: IClassFixture<UseCaseBase> //: UseCaseBase
    {
        //private UseCaseBase baseCase;
        private FightTest1 baseCase;
        public TestFireball(FightTest1 baseCase) //UseCaseBase baseCase)
        {
            Console.WriteLine($"TestFireball ctor: {baseCase}, {Eevee.models.creatureModels.Values.Count()}");
            this.baseCase = baseCase;
        }
        [Fact]
        public void test()
        {

        }
    }

}
