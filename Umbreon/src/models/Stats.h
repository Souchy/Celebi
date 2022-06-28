#pragma once

#include <string>
#include <vector>
#include <map>
using namespace std;

enum Mod {
    BOUND_START,

    ap,
    ap_max,
    mp,
    mp_max,
    hp,
    hp_max,

    attack,
    defense,
    sp_attack,
    sp_defense,
    speed,

    BOUND_END
};


class Stats {
private:
    map<int, int> stats;
public:
    Stats() {
        for(int m = Mod::BOUND_START; m < Mod::BOUND_END; m++) {
            stats[m] = 0;
        }
    }
    ~Stats() {
        stats.clear();
    }
    int get(int mod) {
        return stats[mod];
    }
    void add(int mod, int val) {
        int old = get(mod);
        stats[mod] = old + val;
    }
};
