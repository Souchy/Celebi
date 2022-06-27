#pragma once

#include "../ActionPipeline.h"

class Condition {
public:
    Condition() {}
    ~Condition() {}
    Actor actor;
    bool verify(ActionPipeline p);
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
        switch(actor) {
            case source:
                return Comparator::compare(op, p.source.stats.get(mod), value);
            break;
            case target:
            break;
        }
    }
};

enum Actor {
    source,
    target,
    accumulator_last,
    accumulator_total
};


struct ConditionIsEqual {
    
};
struct ConditionGT {

};
enum ComparisonOperator {
    eq,
    gt,
    ge,
    lt,
    le
};

class Comparator {
public:
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
}
