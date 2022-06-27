#pragma once

#include <vector>
#include "../models/Spell.h"
#include "../models/Effect.h"
#include "../models/Stats.h"

class ActionPipeline {
public:
    Creature source;
    Stats statsSource;
    vector<EffectAction> actions;
};

class EffectAction {
public:
    EffectAction();
    ~EffectAction();
    Target target;
    Stats statsTarget;
    //Creature source; //Target source;
    //Stats statsSource;
    Effect effect;
};

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
    vector<EffectAction> actions;
    for (auto e : s.effects) {
        EffectAction a;
        Creature s, t;
        a.source = source;
        a.target = target;
        a.statsSource = getTotalStats(s);
        a.statsTarget = getTotalStats(t);
    }
}


void stackEffect(vector<EffectAction> actions) {

}
