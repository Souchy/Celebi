extends Node3D


# Fireball is a projectile with an aoe explosion at the end
# Could add [additional projectiles] to launch multiple fireballs in poe style I guess
# - Does it target the ground, then explode and then shotgun? 
# - Or do each projectile need a creature target? (similar result to chain)

# Called when the node enters the scene tree for the first time.
func _ready():
	cast(null, Vector3(10,0,0), null)
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass


func cast(source, targetcell, targets):
	var anim: Animation = $AnimationPlayer.get_animation("launch")
	var positionTrack: int = anim.find_track("Projectile:position", Animation.TYPE_POSITION_3D);
	anim.track_set_key_value(positionTrack, 1, (targetcell - Vector3(0,0,0)) / 2 + Vector3(0,0.5,0));
	anim.track_set_key_value(positionTrack, 2, targetcell);
	$AnimationPlayer.play("launch");
	# send event to show damage numbers when you reach animation 0.5s key
	pass



