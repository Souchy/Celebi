extends MarginContainer


# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass


func _on_gui_input(event:InputEvent):
	# print("gui input event");
	if event is InputEventMouse:
		var e:InputEventMouse = event as InputEventMouse
		print("gui input mouse " + str(e.is_pressed()) + "");
		if e.is_pressed():
			print("gui input mouse click");
		pass
	pass # Replace with function body.
