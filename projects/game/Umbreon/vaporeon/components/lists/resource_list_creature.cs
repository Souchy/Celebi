using Godot;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.factories;
using souchy.celebi.eevee.impl.shared;
using souchy.celebi.eevee.impl.stats;
using souchy.celebi.eevee.impl.util;
using Umbreon.vaporeon;

public partial class resource_list_creature : ResourceList
{

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        base._Ready();
        Eevee.models.creatureModels.GetEntityBus().subscribe(this);
    }

    #region Functions
    public ICreatureModel getSelectedItem() => Eevee.models.creatureModels.Get(getSelectedItemID());
    public override void fillList()
    {
        foreach (IID creatureId in Eevee.models.creatureModels.Keys)
            createChildNode(creatureId);
    }
    public override void createChildNode(IID modelId)
    {
        ICreatureModel model = Eevee.models.creatureModels.Get(modelId);
        var name = model.GetName()?.ToString() ?? "uid " + model.entityUid.ToString();
        var desc = model.GetDescription();
        base.addChild(name, new Color().Random(), model.entityUid);
    }
    public override void publishSelect(IID id)
    {
        var model = Eevee.models.creatureModels.Get(id);
        this.GetVaporeon().bus.publish(VaporeonSignals.select, base.selectorForControl, model);
        base.selectorForControl = null;
    }
    #endregion


    #region GUI Handlers
    public override void onClickEdit() => this.GetVaporeon().openEditor(getSelectedItem()); 
    public override void onClickCopy() => this.GetVaporeon().CurrentObjectCopied = getSelectedItem();
    public override void onClickCreateBtn()
    {
        Factories.newCreatureModel();
    }
    public override void onClickRemoveBtn()
    {
        if (base.selectedNode == null) return;
        IID id = (IID) base.selectedNode.GetMeta(VaporeonUtil.metaIID).AsString();
        var creature = Eevee.models.creatureModels.Get(id);
        Eevee.models.creatureModels.Remove(creature.entityUid);
        // dont delete the skins, keep them for later use and
        // TODO add new tabs to vaporeon for skins
        base.selectedNode = null;
        //base.GetVaporeon().CurrentCreatureModel = null;
    }
    #endregion

}
