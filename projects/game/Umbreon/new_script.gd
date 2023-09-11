extends Node


# Called when the node enters the scene tree for the first time.
func _ready():
	
	const path = "res://assets/creatures/xbot/xbot.glb"
	# async load request
	ResourceLoader.load_threaded_request(path);
	
	
	
	# later thread-blocking get
	var scene = ResourceLoader.load_threaded_get(path);
	
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
