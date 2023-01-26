using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.face.objects.controllers
{
    public interface ITimeline
    {
        public int currentRound { get; set; }
        public int currentTurn { get; set; }
    }
}
