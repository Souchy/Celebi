#pragma once

#include <vector>
#include "../util/Table.h"
#include "../util/Position.h"
//#include "Creature.h"

class Cell {
public:
    Cell() {}
    ~Cell() {}
    Position pos;
    int creatureId;
};


class Board {
public:
    Table<Cell> cells = Table<Cell>(24, 24, Cell());

    Board() {
        //for (auto i : cells.size()) {
        //}
    }
    ~Board() {}

    int getCreatureId(Target t) {
        return 0;
    }
};
