#pragma once

// #include <godot_cpp/classes/node3d.hpp>
// #include <godot_cpp/classes/global_constants.hpp>
// #include <godot_cpp/classes/viewport.hpp>
// #include <godot_cpp/core/binder_common.hpp>
// #include <godot_cpp/classes/mesh_instance3d.hpp>
// #include <godot_cpp/classes/mesh.hpp>
// using namespace godot;

#include <string>
#include <vector>
using namespace std;

class Stats { //} : public Node3D {
	// GDCLASS(Unit, Node3D);

protected:
	// static void _bind_methods();
    
private:

public:
	Stats();
	~Stats();

    int hp;
    int attack;
    int defense;
    int sp_attack;
    int sp_defense;
    int speed;

};

