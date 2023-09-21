extends Draggable

@onready
var highlightTheme: StyleBoxFlat = $PanelContainer.get_theme_stylebox("panel").duplicate();

# Called when the node enters the scene tree for the first time.
func _ready():
	highlightTheme.bg_color.a = 100.0 / 255.0;
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func _physics_process(delta):
	super(delta);
	pass

func _on_panel_container_mouse_entered():
	super();
	$PanelContainer.add_theme_stylebox_override("panel", highlightTheme);
	pass # Replace with function body.

func _on_panel_container_mouse_exited():
	super();
	$PanelContainer.remove_theme_stylebox_override("panel");
	pass # Replace with function body.

func _on_panel_container_gui_input(event):
	super(event);
	pass # Replace with function body.
