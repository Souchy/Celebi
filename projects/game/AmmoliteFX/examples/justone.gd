extends MeshInstance3D

@export_range(0, 1, 0.01)
var dissolved: float = 0;

# Called when the node enters the scene tree for the first time.
func _ready():
	var mat: Material = self.mesh.surface_get_material(0);
	mat.set("shader_parameter/alpha", dissolved);
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	var mat: Material = self.mesh.surface_get_material(0);
	mat.set("shader_parameter/alpha", dissolved);
	pass
