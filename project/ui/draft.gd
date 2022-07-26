extends Control


# Called when the node enters the scene tree for the first time.
func _ready():
	
	setupCreatures();	
	setupSpells();
	
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func setupCreatures():
	
	for i in range(100):
		print("");
		var c = MarginContainer.new();
		var t = TextureRect.new();
		var l = Label.new();
		t.texture = load("res://assets/splash/lilia.png");
		t.ignore_texture_size = true;
		t.stretch_mode = TextureRect.STRETCH_KEEP_ASPECT_COVERED;
		t.custom_minimum_size.x = 80;
		t.custom_minimum_size.y = 80;
		c.add_child(t);
		#l.text = "Lilia"
		c.add_child(l);
		$CreatureSelector/CreaturesGrid.add_child(c);
	
	
	for i in range(3):
		print("");
		var c = MarginContainer.new();
		var t = TextureRect.new();
		var l = Label.new();
		t.texture = load("res://assets/splash/lilia.png");
		t.ignore_texture_size = true;
		t.stretch_mode = TextureRect.STRETCH_KEEP_ASPECT_COVERED;
		t.custom_minimum_size.x = 240;
		t.custom_minimum_size.y = 100;
		c.add_child(t);
		l.text = "Lilia"
		c.add_child(l);
		$ContainerOpponent/Creatures.add_child(c);
		
	for i in range(3):
		print("");
		var c = MarginContainer.new();
		var t = TextureRect.new();
		var l = Label.new();
		t.texture = load("res://assets/splash/lilia.png");
		t.ignore_texture_size = true;
		t.stretch_mode = TextureRect.STRETCH_KEEP_ASPECT_COVERED;
		t.custom_minimum_size.x = 240;
		t.custom_minimum_size.y = 100;
		c.add_child(t);
		l.text = "Lilia"
		c.add_child(l);
		$ContainerPlayer/Creatures.add_child(c);
	pass

func setupSpells():
	for i in range(100):
		print("");
		var c = MarginContainer.new();
		var t = TextureRect.new();
		var l = Label.new();
		t.texture = load("res://assets/fire.png");
		t.ignore_texture_size = true;
		t.stretch_mode = TextureRect.STRETCH_KEEP_ASPECT_COVERED;
		t.custom_minimum_size.x = 80;
		t.custom_minimum_size.y = 80;
		c.add_child(t);
		l.text = "Lilia"
		c.add_child(l);
		$SpellSelector/VBoxContainer/HBoxContainer/ScrollContainer/Spells.add_child(c);
	pass


func _on_switch_button_toggled(button_pressed: bool):
	if(button_pressed):
		$SpellSelector.visible = false;
		$CreatureSelector.visible = true;
	else:
		$SpellSelector.visible = true;
		$CreatureSelector.visible = false;
	pass # Replace with function body.
