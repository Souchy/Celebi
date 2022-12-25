using Espeon.souchy.celebi.eeveeimpl.objects;
using Espeon.souchy.celebi.espeon.impl.eevee.controllers;
using EspeonTest.souchy.celebi.espeon.test.situations;
using Moq;
using souchy.celebi.eevee;
using souchy.celebi.eevee.face.controllers;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.impl.util.math;
using System.Reflection.Emit;

namespace EeveeTest.souchy.celebi.eeveetest.situations
{
    public class TestSituation1 : ITestSituation
    {

        public readonly IUIdGenerator uIdGenerator;
        public IFight fight { get; init; }

        public Mock<IPlayer> playerMoq1 = new Mock<IPlayer>();
        public Mock<IPlayer> playerMoq2 = new Mock<IPlayer>();

        public TestSituation1()
        {
            uIdGenerator = new UIdGenerator();
            fight = new Fight(uIdGenerator);
            playerMoq1.Object.creatures = new List<ICreature>();
            playerMoq2.Object.creatures = new List<ICreature>();

            prepCreatures();

            // cells 20x20
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    var cell = new Cell(uIdGenerator);
                    cell.position.x = i;
                    cell.position.y = j;
                    fight.board.cells.Add(cell);
                }
            }
        }

        private void prepCreatures()
        {
            var newCreature = (int x, int y) => {
                var c = new Creature(uIdGenerator);
                c.position.x = x;
                c.position.y = y;
                c.player = playerMoq1.Object;
                return c;
            };

            // 0,0
            this.fight.board.creatures.Add(newCreature(0, 0));
            // lines and diagonal of 3 from 0,0
            for (int i = 0; i < 3; i++)
            {
                this.fight.board.creatures.Add(newCreature(0, i));
                this.fight.board.creatures.Add(newCreature(i, 0));
                this.fight.board.creatures.Add(newCreature(i, i));
            }
            //       5,6
            // 4,5 - 5,5 - 6,5
            //       4,5
            this.fight.board.creatures.Add(newCreature(5, 5));
            this.fight.board.creatures.Add(newCreature(5, 4));
            this.fight.board.creatures.Add(newCreature(5, 6));
            this.fight.board.creatures.Add(newCreature(4, 5));
            this.fight.board.creatures.Add(newCreature(6, 5));
        }


    }
}
