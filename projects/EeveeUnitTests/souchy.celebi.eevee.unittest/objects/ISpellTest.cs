using Moq;
using souchy.celebi.eevee;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;

namespace EeveeUnitTests.souchy.celebi.eevee.unittest.objects
{
    public class ISpellTest
    {


        public void asdf()
        {
            Mock<ICreature> crea = new Mock<ICreature>();
            Mock<IContext> context = new Mock<IContext>(); //crea.Object.contexts[ContextType.Turn];
            Mock<ISpell> spell = new Mock<ISpell>();


            foreach(var spellCastHistory in context.Object.spellsCast)
            {

            }

        }



    }
}
