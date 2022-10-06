extends Node3D

var maxx = 0;
var maxy = 0;
var maxz = 0;
var center: Vector3;

# Called when the node enters the scene tree for the first time.
func _ready():
	var sumx = 0;
	var sumy = 0;
	var sumz = 0;
	for cell in $GridMap.get_used_cells():
		if(cell.x > maxx): maxx = cell.x;
		if(cell.y > maxy): maxy = cell.y;
		if(cell.z > maxz): maxz = cell.z;
		sumx += cell.x;
		sumy += cell.y;
		sumz += cell.z;
	
	center = Vector3();
	center.x = sumx / $GridMap.get_used_cells().size();
	center.y = sumy / $GridMap.get_used_cells().size();
	center.z = sumz / $GridMap.get_used_cells().size();
	var local = $GridMap.map_to_local(center);
	var global = $GridMap.to_global(local);
	var worldcenter = global;
	#var worldcenter = $GridMap.map_to_world(center);
	if(worldcenter == center):
		center = worldcenter;
	else:
		center = worldcenter;
		center.x += $GridMap.cell_size.x / 2;
		center.y += $GridMap.cell_size.y / 2;
		center.z += $GridMap.cell_size.z / 2;
	
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
