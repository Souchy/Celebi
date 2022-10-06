extends Node3D


########################
# EXPORT PARAMS
########################
# movement
@export_range (0, 100, 0.5) var movement_speed: float = 20
# zoom
@export_range (0, 100, 0.5) var min_zoom: float = 3
@export_range (0, 100, 0.5) var max_zoom: float = 20
@export_range (0, 100, 0.5) var zoom_speed: float = 20
@export_range (0, 1, 0.05) var zoom_speed_damp: float = 0.8

@export_range (0, 100, 0.5) var maxDistanceFromOrigin: float = 20


########################
# PARAMS
########################
# movement
#onready var tween = $Tween
var _lock_movement: bool = false
# zoom
@onready var camera: Camera3D = $Elevation/Camera3D
var zoom_direction = 0
# rotation
@onready var elevation: Node3D = $Elevation
var is_rotating = false
# pan
var is_panning = false
# click position
const RAY_LENGTH = 1000
const GROUND_PLANE = Plane(Vector3.UP, 0)
var _last_mouse_position = Vector2()

var tween: Tween;

########################
# 
########################

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass


func _input(event: InputEvent) -> void:
	# zoom
	if event.is_action_pressed("camera_zoom_in"):
		zoom_direction = -1
	if event.is_action_pressed("camera_zoom_out"):
		zoom_direction = 1
	# rotation
	if event.is_action_pressed("camera_rotate"):
		is_rotating = true
		_last_mouse_position = get_viewport().get_mouse_position()
	if event.is_action_released("camera_rotate"):
		is_rotating = false
	# pan
	if event.is_action_pressed("camera_pan"):
		is_panning = true
		_last_mouse_position = get_viewport().get_mouse_position()
	if event.is_action_released("camera_pan"):
		is_panning = false
