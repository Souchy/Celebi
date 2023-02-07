using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.shared.effects;
using souchy.celebi.eevee.impl.util;

namespace Umbreon.vaporeon.components
{
    public interface IEffectNodesContainer
    {
        public IEntityList<IID> parentList { get; }
        public IEntityList<IID> GetEffectIds();
        public IEnumerable<IEffect> GetEffectsEnum();
        public Control GetContainer();

        #region Gui Handlers
        #endregion

        #region Diamond Handlers
        /// <summary>
        /// Add a child to this list
        /// </summary>
        /// <param name="ei"></param>
        [Subscribe(EntityList<IID>.EventAdd)]
        public void onAddEffectChild(IID ei)
        {
            var e = Eevee.models.effects.Get(ei);
            var mini = Vaporeon.instanceScene<EffectMini>();
            this.GetContainer().AddChild(mini);
            mini.init(e, parentList);
        }
        /// <summary>
        /// Remove a child from this list
        /// </summary>
        /// <param name="ei"></param>
        [Subscribe(EntityList<IID>.EventRemove)]
        public void onRemoveEffectChild(IID ei)
        {
            var node = this.GetContainer().GetChildren<EffectMini>().FirstOrDefault(m => m.effect.entityUid == ei);
            if (node != null)
                this.GetContainer().RemoveChild(node);
        }
        /// <summary>
        /// Move in the parent list.
        /// This procs on parentList.Move(e, -1);
        /// So the parent receives this event
        /// </summary>
        [Subscribe(EntityList<IID>.EventMove)]
        public void onMoveEffectInParent(IID ei, int indexPrevious, int indexNow)
        {
            var node = this.GetContainer().GetChild(indexPrevious);
            this.GetContainer().MoveChild(node, indexNow);
            // dis/enable buttons
            for (int i = 0; i < GetContainer().GetChildCount(); i++)
            {
                var c = (EffectMini) GetContainer().GetChild(i);
                c.BtnMoveUp.Disabled = i == 0;
            }
        }
        #endregion
    }

    public static class IEffectNodesContainerExtensions
    {
        public static void onClickAddChild(this IEffectNodesContainer container)
        {
            var newEffect = EffectBase.Create();
            Eevee.models.effects.Add(newEffect.entityUid, newEffect);
            container.GetEffectIds().Add(newEffect.entityUid);
        }
        public static void fillEffects(this IEffectNodesContainer container)
        {
            foreach (var c in container.GetEffectsEnum())
            {
                var mini = Vaporeon.instanceScene<EffectMini>(); // new EffectMini();
                container.GetContainer().AddChild(mini);
                mini.init(c, container.parentList);
            }
        }
    }


}
