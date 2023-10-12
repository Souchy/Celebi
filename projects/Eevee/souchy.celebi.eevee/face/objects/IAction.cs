using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.neweffects.face;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace souchy.celebi.eevee.face.objects
{
    public interface IAction
    {
        /// <summary>
        /// Player who sent that action message. 
        /// </summary>
        public IPlayer player { get; set; }
        /// <summary>
        /// Null by default.
        /// This action's parent action. Null for root actions (spell cast, move). <br></br>
        /// A root effect action will refer to the spell action that casts it.
        /// </summary>
        public IAction parent { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public HashSet<IAction> children { get; set; }
        /// <summary>
        /// 0 by default.
        /// Level starts at 0 and goes deeper. <br></br>
        /// Ex spell cast is at 0, root effects are at 1, etc
        /// </summary>
        public int depthLevel { get; set; }
        /// <summary>
        /// Holds effect results for the current action
        /// </summary>
        public ActionContext context { get; set; }  
        /// <summary>
        /// 
        /// </summary>
        public IFight fight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ObjectId caster { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ObjectId targetCell { get; set; }
        /// <summary>
        /// If we ever use IActionContext, then we can use the root action's context to compile actions results into
        /// </summary>
        public IAction getRootAction() {
            return parent == null ? this : parent.getRootAction();
        }
        /// <summary>
        /// Copy action with same depthLevel, same context reference, same parent reference. <br></br>
        /// Useful to reverse caster/target for example?
        /// </summary>
        public IAction copy();
        public ICreature getCaster() => fight.creatures.Get(caster);
        public ICell getCell() => fight.cells.Get(targetCell);
    }
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public abstract class BaseAction : IAction
    {
        public IPlayer player { get; set; }
        public IAction parent { get; set; }
        public HashSet<IAction> children { get; set; } = new();
        public int depthLevel { get; set; }
        public ActionContext context { get; set; } = new ActionContext();
        public IFight fight { get; set; }
        public ObjectId caster { get; set; }
        public ObjectId targetCell { get; set; }
        public BaseAction() { }
        public BaseAction(IAction parentAction)
        {
            fight = parentAction.fight;
            parent = parentAction;
            caster = parentAction.caster;
            targetCell = parentAction.targetCell;
            depthLevel = parentAction.depthLevel + 1;
            parent.children.Add(this);
        }

        public IAction copy()
        {
            IAction copy = copyImplementation();
            copy.fight = fight;
            copy.player = player;
            foreach(var child in children)
                copy.children.Add(child);
            copy.parent = parent;
            copy.depthLevel = depthLevel;
            copy.context = context;
            copy.caster = caster;
            copy.targetCell = targetCell;
            return copy;
        }
        protected abstract IAction copyImplementation();
    }

    public class ActionContext
    {
        public List<ActionEffectTargetReturnValue> effectReturnValues = new List<ActionEffectTargetReturnValue>();
        /// <summary>
        /// Get all target actions for the saame effect
        /// </summary>
        /// <param name="effectEntityId"></param>
        /// <returns></returns>
        public IEnumerable<ActionEffectTargetReturnValue> getReturnsForEffect(ObjectId effectEntityId)
        {
            return effectReturnValues.Where(e => e.action.effect.entityUid == effectEntityId);
        }
    }
    public class ActionEffectTargetReturnValue
    {
        public SubActionEffectTarget action { get; set; }
        public DataType data { get; set; }
    }

    /// <summary>
    /// for retrieving stats whenever, for the UI
    /// </summary>
    //public record EmptyAction(IFight fight) : IAction
    //{
    //    public IAction parent { get; set; }
    //    public int depthLevel { get; init; }
    //    public IFight fight { get; set; } = fight;
    //    public ObjectId caster { get; set; }
    //    public ObjectId targetCell { get; set; }
    //}
    public interface IActionSpell : IAction
    {
        public ObjectId spell { get; set; }
    }
    public class ActionSpell : BaseAction, IActionSpell
    {
        public ObjectId spell { get; set; }

        protected override IAction copyImplementation()
        {
            var copy = new ActionSpell();
            copy.spell = spell;
            return copy;
        }
    }
    public interface IActionMove : IAction
    {
        //public IID caster { get; set; }
        //public IID targetCell { get; set; }
    }
    public interface IActionPass : IAction
    {
        //public IID caster { get; set; }
        //public IID target { get; set; }
    }

    public class ActionPass : BaseAction, IActionPass
    {
        protected override IAction copyImplementation()
        {
            var copy = new ActionPass();
            return copy;
        }
    }

    public interface IActionSwapOut : IAction
    {
        //public IID caster { get; set; }
        //public IID target { get; set; }
    }

    /// <summary>
    /// Apply 1 effect to 1 target only
    /// </summary>
    public interface ISubActionEffectTarget : IAction
    {
        /// <summary>
        /// Effect to apply, contains the schema, conditions, etc
        /// </summary>
        public IEffect effect { get; set; }
    }
    /// <summary>
    /// Apply 1 effect to its whole aoe
    /// </summary>
    public class SubActionEffect : BaseAction, IAction
    {
        /// <summary>
        /// Effect to apply, contains the schema, conditions, etc
        /// </summary>
        public IEffect effect { get; set; }
        /// <summary>
        /// Possible targets in the effect aoe
        /// </summary>
        public IEnumerable<IBoardEntity> boardTargets { get; set; }

        public SubActionEffect(IAction parent) : base(parent) { }

        protected override IAction copyImplementation()
        {
            var copy = new SubActionEffect(parent);
            copy.effect = effect;
            copy.boardTargets = boardTargets.ToList();
            return copy;
        }
    }
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public class SubActionEffectTarget : BaseAction,  ISubActionEffectTarget
    {
        public IEffect effect { get; set; }
        public SubActionEffectTarget(IAction parent) : base(parent) { }

        protected override IAction copyImplementation()
        {
            var copy = new SubActionEffect(parent);
            copy.effect = effect;
            return copy;
        }
    }

    public class SubActionStatus : BaseAction
    {
        public IStatusInstance statusInstance { get; set; }
        public SubActionStatus(IAction parent) : base(parent) { }

        protected override IAction copyImplementation()
        {
            var copy = new SubActionStatus(parent);
            copy.statusInstance = statusInstance;
            return copy;
        }
    }

}
