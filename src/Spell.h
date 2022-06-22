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
#include "Effect.h"
using namespace std;

class Spell { //} : public Node3D {
	// GDCLASS(Unit, Node3D);

protected:
	// static void _bind_methods();
    
private:

public:
	Spell();
	~Spell();

    int id;
    string name; // i18n
    string description; // i18n
    string type;
    int range;
    int cost;
    vector<Effect> effects;

    // aoe
    // cast conditions: cooldown, cast per turn, cast per target, empty/ally/enemy/summon cell
    // range min/max
    // range pattern min/max


};

