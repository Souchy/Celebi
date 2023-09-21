class_name Draggable
extends Control

var mouseIn: bool = false;
var draggingDistance
var dir
var dragging: bool = false;
var newPosition: Vector2 = Vector2()
var lastDraggedTime;
var mousePos;

func _physics_process(delta):
	if dragging:
#		position = newPosition;
		position = lerp(position, newPosition, 25 * delta);
	pass
	
func _on_panel_container_gui_input(event):
	mousePos = get_viewport().get_mouse_position();
	if event is InputEventMouseButton: 
		if event.is_pressed() && mouseIn:
			lastDraggedTime = Time.get_ticks_msec();
			draggingDistance = position.distance_to(mousePos)
			dir = (mousePos - position).normalized();
			dragging = true;
			newPosition = mousePos - draggingDistance * dir
		else:
			dragging = false;
	if event is InputEventMouseMotion:
		if dragging:
			newPosition = mousePos - draggingDistance * dir
	pass # Replace with function body.

func _on_panel_container_mouse_entered():
	mouseIn = true;
	pass # Replace with function body.

func _on_panel_container_mouse_exited():
	mouseIn = false;
	pass # Replace with function body.
