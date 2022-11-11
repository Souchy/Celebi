extends Control

var social;
var team;
var pfx;

# Called when the node enters the scene tree for the first time.
func _ready():
	social = $VBoxContainer/ui_social
	team = $VBoxContainer/TeamSelector
	pfx = $VBoxContainer/MarginContainer/Play/PlayParticles
	pass 

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func _on_ui_sidebar_focus_exited():
	team.visible = false;
	social.visible = true;
	pass 

func _on_play_button_up():
	if(team.visible):
		# play -> change scene etc
		print("play")
		social.visible = true;
		team.visible = false;
	else:
		social.visible = false;
		team.visible = true;
	pass

func _on_play_mouse_entered():
	pfx.emitting = true;
	pass 

func _on_play_mouse_exited():
	pfx.emitting = false;
	pass 

