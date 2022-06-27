#pragma once

#include <string>
using namespace std;

#include "Aoe.h"
#include "Board.h"
#include "Target.h"


class EffectAssets {
public:
    string name;
    string description;
    string vfx; // vfx command to execute, with parameters 
};

class Effect {
public:
    Effect() {}
    ~Effect() {}

    int type;
    EffectAssets assets;
    
    Aoe aoe = Aoe(1, 1);
    TargetTypeFilter filter;
};

