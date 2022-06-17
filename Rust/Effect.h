#pragma once

#include <string>
using namespace std;

#include "Aoe.h"
#include "Board.h"
#include "Target.h"

class Effect {
    public:
        string name;
        string description;
        string type;
        Aoe aoe;
        TargetTypeFilter filter;
        void stackEffects(Target source, Target target);
};
