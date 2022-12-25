#pragma once

#include <string>
#include <vector>
using namespace std;

#include "Spell.h"
#include "Stats.h"
#include "Status.h"

class Creature {
public:
    int id;
    string name;
    Stats stats;
    vector<Spell> spells;
    vector<Status> status;
};
