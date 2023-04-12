using souchy.celebi.eevee.face.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.face.objects
{
    public interface IAction
    {
    }

    public interface IActionSpell : IAction
    {
        public IID caster { get; set; }
        public IID targetCell { get; set; }
        public IID spell { get; set; }
    }
    public interface IActionMove : IAction
    {
        public IID caster { get; set; }
        public IID targetCell { get; set; }
    }
    public interface IActionPass : IAction
    {
        public IID caster { get; set; }
        public IID target { get; set; }
    }
    public interface IActionSwapOut : IAction
    {
        public IID caster { get; set; }
        public IID target { get; set; }
    }
}
