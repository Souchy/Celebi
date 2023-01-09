#pragma once

#include <vector>
#include "models/Spell.h"
#include "models/Effect.h"
#include "models/Stats.h"
#include "models/Creature.h"

class EffectAction {
public:
    EffectAction();
    ~EffectAction();

    Target target;
    Stats statsTarget;

    Target source; // Creature source; //
    Stats statsSource;

    Effect effect;
};

class ActionPipeline {
public:
    Target source; //Creature source;
    Stats statsSource;
    vector<EffectAction> actions;
};


/*
Stats getTotalStats(Creature c) {
    Stats total = c.stats; // copy base stats
    // compileStats(s, c.stats);
    for (auto status : c.status) {
        addStats(total, status.stats);
    }
    return total;
};
void addStats(Stats dest, Stats from) {
    for (int m = Mod::BOUND_START; m < Mod::BOUND_END; m++) {
        dest.add(m, from.get(m));
    }
}




void castSpell(Spell s, Target source, Target target) {
    Board b;
    vector<EffectAction> actions;
    for (auto statement : s.statements) {
        EffectAction a;
        Creature s, t;
        a.source = b.getCreature(source); // source;
        a.target = target;
        //a.statsSource = getTotalStats(s);
        a.statsTarget = getTotalStats(t);
    }
}


void stackEffect(vector<EffectAction> actions) {

}
*/
