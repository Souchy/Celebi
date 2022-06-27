#pragma once
#include <string>
#include <vector>
#include "Aoe.h"
#include "Effect.h"
#include "Trigger.h"
#include "Stats.h"
#include <map>
#include "conditions/Condition.h"

struct SpellConditions {
    Aoe cellConditions = Aoe(1,1);
    int castPerTarget = 0;
    int castPerTurn = 0;
    int cooldown = 0;
    int range = 1;
};

struct Cost {
    Mod resource;
    int amount;
};

struct Statement {
    Condition condition;
    vector<Statement> children;
    vector<Effect> effects;
};

struct SpellStatus {
    int castsThisTurn;
    std::map<int, int> castsPerTarget;
    int lastTurnCast;
    Trigger onEndTurn;
};

struct SpellAssets {
    string name; // only client needs this
    string description; // only client needs this
    string icon;
    string animation; // caster animation
    // vfx command to execute, with parameters
    string vfx; // main vfx on the caster, each effect has their own vfx
};

class Spell {
public:
    Spell() {}
    ~Spell() {}

    SpellAssets assets;
    SpellStatus status; // for an instance of the spell

    SpellConditions conditions;
    vector<Cost> costs;
    vector<Statement> statements;
};
