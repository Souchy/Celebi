using Godot;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.shared;
using souchy.celebi.eevee.impl.util;
using Umbreon.vaporeon;

public partial class resource_list_status : ResourceList
{

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        Eevee.models.statusModels.GetEntityBus().subscribe(this);
    }

    #region Functions
    public IStatusModel getSelectedItem() => Eevee.models.statusModels.Get(getSelectedItemID());
    public override void fillList()
    {
        foreach (IID id in Eevee.models.statusModels.Keys)
            createChildNode(id);
    }
    public override void createChildNode(IID modelId)
    {
        IStatusModel model = Eevee.models.statusModels.Get(modelId);
        var name = model.entityUid.ToString();
        //var name = model.GetName()?.ToString() ?? "uid " + model.entityUid.ToString();
        //var desc = model.GetDescription();
        base.addChild(name, new Color().Random(), model.entityUid);
    }
    public override void publishSelect(IID id)
    {
        var model = Eevee.models.statusModels.Get(id);
        this.GetVaporeon().bus.publish(VaporeonSignals.select, base.selectorForControl, model);
        base.selectorForControl = null;
    }
    #endregion


    #region GUI Handlers
    public override void onClickEdit() => this.GetVaporeon().openEditor(getSelectedItem());
    public override void onClickCopy() => this.GetVaporeon().CurrentObjectCopied = getSelectedItem();
    public override void onClickCreateBtn()
    {
        // Model
        var statusModel = StatusModel.CreatePermanent();
        // Name
        //var name = StringEntity.Create("Name for: " + creatureSkin.entityUid);
        //creatureSkin.nameId = name.entityUid;
        //Eevee.models.i18n.Add(creatureSkin.nameId, name);
        // Desc
        //var desc = StringEntity.Create("Desc for: " + creatureSkin.entityUid);
        //creatureSkin.descriptionId = desc.entityUid;
        //Eevee.models.i18n.Add(creatureSkin.descriptionId, desc);
        // add Model 
        Eevee.models.statusModels.Add(statusModel.entityUid, statusModel); // creatureSkin.entityUid, creatureSkin);
    }
    public override void onClickRemoveBtn()
    {
        if (base.selectedNode == null) return;
        IID id = (IID) base.selectedNode.GetMeta(VaporeonUtil.metaIID).AsString();
        var model = Eevee.models.statusModels.Get(id);
        Eevee.models.statusModels.Remove(model.entityUid);
        // dont delete the skins, keep them for later use and
        // TODO add new tabs to vaporeon for skins
        base.selectedNode = null;
        //base.GetVaporeon().CurrentCreatureModel = null;
    }
    #endregion
}
