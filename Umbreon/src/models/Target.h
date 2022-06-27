#pragma once
#include <string>
#include "Board.h"

enum TargetType {
	any = 0,
	emptyCell = 1,
	ally = 2,
	enemy = 4,
	corpse = 8,
	summon = 16,
	summon_static = 32,
	//ALL = emptyCell | ally | enemy | summon | summon_static | corpse,
};

class TargetTypeFilter {
public:
    int value = TargetType::any;
	TargetTypeFilter() {

	}
	TargetTypeFilter(int v) {
		value = v;
	}
    bool includes(TargetTypeFilter filter) {
		return value & filter.value;
    }
	bool equals(TargetTypeFilter filter) {
		return value == filter.value;
	}
	bool isAny() {
		return value == TargetType::any;
	}
	TargetTypeFilter operator-(const TargetTypeFilter other) {
		value = value & ~other.value;
		return value;
	}
	TargetTypeFilter operator+(const TargetTypeFilter other) {
		value = value | other.value;
		return value;
	}
};


class Target {
public:
	Position pos;
	TargetTypeFilter filter;
};
