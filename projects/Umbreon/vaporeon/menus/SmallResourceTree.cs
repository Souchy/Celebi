using Godot;
using Godot.Sharp.Extras;
using System;
using System.Reflection;

public enum PopupAction
{
    Add,
    Remove,
    Copy,
    Paste
}

public partial class SmallResourceTree : VBoxContainer
{

    private object selected { get; set; }

    [Export]
    public string Title { get; set; }

    #region Toolbar
    [NodePath("Buttons/AddBtn")]
    public Button AddBtn { get; set; }
    [NodePath("Buttons/DeleteBtn")]
    public Button DeleteBtn { get; set; }
    [NodePath("Buttons/CopyBtn")]
    public Button CopyBtn { get; set; }
    [NodePath("Buttons/PasteBtn")]
    public Button PasteBtn { get; set; }
    #endregion

    #region Nodes
    [NodePath]
    public Label TitleLbl { get; set; }
    [NodePath]
    public Tree Tree { get; set; }
    [NodePath]
    public PopupMenu PopupMenu { get; set; }
    #endregion


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        this.OnReady();
        this.TitleLbl.Text = Title;
        //for(int i = 0; i < 30; i++)
        //{
        //    TreeItem item = this.Tree.CreateItem();
        //    item.SetText(0, "some text");
        //}
        //item.AddButton(1, tooltipText: "some button");

        PopupMenu.IndexPressed += PopupMenu_IndexPressed;
        foreach(var action in Enum.GetNames(typeof(PopupAction)))
        {
            PopupMenu.AddItem(action);
        }


    }

    public event Action onAddBtnClick { 
        add => this.AddBtn.Pressed += value;
        remove => this.AddBtn.Pressed -= value;
    }
    public event Action onRemoveBtnClick
    {
        add => this.AddBtn.Pressed += value;
        remove => this.AddBtn.Pressed -= value;
    }
    public event Action onCopyBtnClick
    {
        add => this.CopyBtn.Pressed += value;
        remove => this.CopyBtn.Pressed -= value;
    }
    public event Action onPasteBtnClick
    {
        add => this.PasteBtn.Pressed += value;
        remove => this.PasteBtn.Pressed -= value;
    }

    /*
    public void onTreeItemDoubleClick()
    {
        GD.Print("Click Double");
    }
    public void onTreeItemSelected()
    {
        //GD.Print("Selected: " + Tree.GetSelected().GetText(0));
    }
    */

    public void onTreeItemMouseSelected(Vector2 pos, int mouseButton)
    {
        //GD.Print("Click TreeItemSelected " + mouseButton);
        if (mouseButton == (int) MouseButton.Right)
        {
            GD.Print("Click TreeItemSelected Right");
            PopupMenu.Position = (Vector2I) pos; // new Vector2I((int) pos.X, (int) pos.Y);
            PopupMenu.Show();
        }
    }

    private void PopupMenu_IndexPressed(long index)
    {
        GD.Print("Popup index press: " + index);
        switch((PopupAction) index)
        {
            case PopupAction.Add:

                break;
            case PopupAction.Remove:
                Tree.GetSelected().Free(); // ?
                break;
            case PopupAction.Copy:
                Vaporeon vap = GetNode<Vaporeon>("Vaporeon");
                vap.CurrentObjectCopied = Tree.GetSelected();
                break;
            case PopupAction.Paste: 
                // what do we have, an iid?, an object? what type of object?
                // we should assume the type based on the content of the tree
                // 
                break;
        }
    }

}
