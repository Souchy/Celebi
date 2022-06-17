#pragma once

#include <string>
using namespace std;

#include "Board.h"

class Target {
    public:
        Position pos;
        TargetTypeFilter filter;
};


class TargetType {
    public:
        static const int emptyCell = 0;
        static const int ally = 2;
        static const int enemy = 4;
        static const int summon = 8;
        static const int corpse = 16;
};

class TargetTypeFilter {
    public:
        int value = TargetType::emptyCell;
};

