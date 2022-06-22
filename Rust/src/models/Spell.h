#pragma once
#include <string>
#include <vector>
using namespace std;

#include "Aoe.h"
#include "Effect.h"

class Spell
{
public:
    Spell();
    ~Spell();
    string name;
    string description;
    SpellConditions conditions;
    vector<Cost> costs;
    vector<Effect> effects;
    TargetTypeFilter targetType;
};

class SpellConditions {
public:
    Aoe cellConditions;
    int castPerTarget;
    int castPerTurn;
    int cooldown;
    int range;
};

class Cost {
public:
    int resource;
    int amount;
};
