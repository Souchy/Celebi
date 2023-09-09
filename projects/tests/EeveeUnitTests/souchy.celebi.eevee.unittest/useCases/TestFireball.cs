using EeveeUnitTests.souchy.celebi.eevee.unittest.setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EeveeUnitTests.souchy.celebi.eevee.unittest.useCases
{

    [Collection("BaseCase")]
    public class TestFireball //: IClassFixture<UseCaseBase> //: UseCaseBase
    {
        private UseCaseBase baseCase;
        public TestFireball(UseCaseBase baseCase)
        {
            this.baseCase = baseCase;
        }
        [Fact]
        public void test()
        {

        }
    }

}
