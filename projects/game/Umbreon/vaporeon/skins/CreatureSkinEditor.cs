using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;
using System;
using souchy.celebi.umbreon.vaporeon.common;

public partial class CreatureSkinEditor : PanelContainer, EditorInitiator<ICreatureSkin>
{
    private ICreatureSkin skin { get; set; }


    #region Nodes - Main bar 
    [NodePath] public Button BtnSave { get; set; }
    [NodePath] public Label EntityID { get; set; }
    [NodePath] public LineEdit NameEdit { get; set; }
    [NodePath] public LineEdit DescriptionEdit { get; set; }
    #endregion

    #region Nodes 
    [NodePath] public LineEdit InputModel { get; set; }
    [NodePath] public Button BtnModel { get; set; }
    [NodePath] public FileDialog FileDialogModel { get; set; }
    [NodePath] public LineEdit InputMesh { get; set; }
    [NodePath] public LineEdit InputIcon { get; set; }
    [NodePath] public Button BtnIcon { get; set; }
    [NodePath] public FileDialog FileDialogIcon { get; set; }
    // animations
    [NodePath] public LineEdit InputIdle { get; set; }
    [NodePath] public LineEdit InputRun { get; set; }
    [NodePath] public LineEdit InputWalk { get; set; }
    [NodePath] public LineEdit InputReceiveHit { get; set; }
    [NodePath] public LineEdit InputVictory { get; set; }
    [NodePath] public LineEdit InputDefeat { get; set; }
    #endregion

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        this.OnReady();
        // main bar
        NameEdit.TextChanged += (txt) => skin.GetName().value = txt;
        DescriptionEdit.TextChanged += (txt) => skin.GetDescription().value = txt;
        BtnSave.ButtonUp += () => skin.GetEntityBus().publish(IEventBus.save, skin);
        //
        BtnIcon.ButtonUp += BtnIcon_ButtonUp;
        BtnModel.ButtonUp += BtnModel_ButtonUp;
	}

    private void BtnModel_ButtonUp()
    {
        throw new NotImplementedException();
    }

    private void BtnIcon_ButtonUp()
    {
        throw new NotImplementedException();
    }

    public void init(ICreatureSkin value)
    {
        throw new NotImplementedException();
    }
}
