#pragma once

#include <vector>

class Aoe {
private:
	vector<int> table;
public:
	int width;
	int height;

	Aoe(int width, int height) {
		this->width = width;
		this->height = height;
	}

	int get(int row, int col) {
		return table[row * width + col];
	}

	void set(int row, int col, int val) {
		table[row * width + col] = val;
	}

	void addColumn(int colIndex) {

	}

	void addRow(int rowIndex) {

	}

	void resize(int width, int height) {
		table.resize(width * height);
	}

};


class Circle {
	bool canDiagonal = true;
};
class Rectangle {
	bool canDiagonal = false;
};
class Square {
	bool canDiagonal = true;
};
class Cross {
	bool canDiagonal = true;
};
class X {
	bool canDiagonal = true;
};
class Line {
	bool canDiagonal = false;
};
class Point {
	bool canDiagonal = true;
};
class Cone {
	bool canDiagonal = false;
};
class Hemisphere {
	bool canDiagonal = false;
};

