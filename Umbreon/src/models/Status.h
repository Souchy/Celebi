#pragma once


#include <string>
#include <vector>
using namespace std;

#include "Stats.h"
#include "Effect.h"
#include "Trigger.h"

class Status {
public:
    int id;
    int source;
    // ---
    Stats stats;
    vector<Effect> effects;
    vector<Trigger> triggers;
    // triggers for the list of effects
    
    // --- client only
    //string icon;
    //string name;
    //string description;

    int stacks;
    int duration;


    Status() {

    }

};

class SpellStatus : public Status {
public: 
    SpellStatus(int spellid) : Status() {
        // client only
        //this->icon = "";
        //this->name = "";
        //this->description = "";
    }
};