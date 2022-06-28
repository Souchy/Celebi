#pragma once

#include "../../ActionPipeline.h"
#include "../Fight.h"

enum ConditionLink {
    AND,
    OR
};
enum Actor {
    source,
    target,
    accumulator_last,
    accumulator_total
};
enum ComparisonOperator {
    eq,
    gt,
    ge,
    lt,
    le
};

class Condition {
public:
    Condition() {}
    ~Condition() {}
    vector<Condition> children;
    ConditionLink childLink;
    Actor actor;
    bool verify(ActionPipeline p);
    
    static bool compare(ComparisonOperator e, int a, int b) {
        switch (e) {
        case eq:
            return a == b;
        case gt:
            return a > b;
        case ge:
            return a >= b;
        case lt:
            return a < b;
        case le:
            return a <= b;
        }
    }
};


class TargetTypeCondition : Condition {
public:
    TargetTypeFilter filter;
    bool verify(ActionPipeline p) {
        auto a = p.actions.back();
        switch(actor) {
            // case source:
            //     return p.source.filter.includes(filter);
            // break;
            case target:
                // return a.target.filter.includes(filter);
            break;
            // case accumulator:
            //     return a.target.filter.includes(filter);
            // break;
        }
    }
};

class StatCondition : Condition {
public:
    Mod mod;
    int value;
    ComparisonOperator op;
    bool verify(ActionPipeline p) {
        auto a = p.actions.back();
        Fight f;
        Board b;
        int cid = b.getCreatureId(p.source);
        Creature* cs = f.getCreature(cid);
        switch(actor) {
            case Actor::source:
                return Condition::compare(op, cs->stats.get(mod), value);
            break;
            case Actor::target:
            break;
        }
    }
};
