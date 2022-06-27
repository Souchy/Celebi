#pragma once

#include <vector>
#include "../util/Table.h"

class Position {
public:
    int x, y, z;
};

class Cell {
public:
    Cell();
    ~Cell();
    Position pos;
};


class Board {
public:
    //vector<Cell> cells;
    Table<Cell> cells;
};