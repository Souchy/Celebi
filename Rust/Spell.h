#pragma once
#include <string>
#include <vector>
using namespace std;

#include "Aoe.h"
#include "Effect.h"

class Spell
{
    string name;
    string description;
    SpellConditions conditions;
    vector<Cost> costs;
    vector<Effect> effects;
};

class SpellConditions {
    Aoe cellConditions;
    int castPerTarget;
    int castPerTurn;
    int cooldown;
    int range;
};

class Cost {
    int resource;
    int amount;
};