#pragma once

#include <vector>

class Board {

    vector<Cell> cells;

};

class Cell {
    public:
        Position pos;
};

class Position {
    public:
        int x, y, z;
};
