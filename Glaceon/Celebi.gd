extends Node3D


# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	# var pos = get_viewport().get_mouse_position()
	var pos = _get_ground_position;
	# print("raycast1: %f, %f, %f" % [pos.x, pos.y, pos.z])
	# cursor.set_position()
	pass



@onready var cursor: Node3D = get_node("Cursor")
@onready var camera: Camera3D = get_node("Camera3d")
const RAY_LENGTH = 1000
const GROUND_PLANE = Plane(Vector3.UP, 0)
func _get_ground_position() -> Vector3:
	var mouse_pos = get_viewport().get_mouse_position()
	var ray_from = camera.project_ray_origin(mouse_pos)
	var ray_to = ray_from + camera.project_ray_normal(mouse_pos) * RAY_LENGTH
	return GROUND_PLANE.intersects_ray(ray_from, ray_to)
