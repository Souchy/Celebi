#pragma once

#include <vector>
#include <string>
#include <iostream>
#include <functional> // std::function

#include "Target.h"
#include "../util/Table.h"

class Aoe : public Table<TargetTypeFilter> {
public:
	Aoe() : Aoe(1, 1) {}
	Aoe(int width, int height) : Table(width, height, TargetTypeFilter()) {
	}
	Aoe(int width, int height, TargetTypeFilter defaultValue) : Table(width, height, defaultValue) {
	}
	Aoe(int w, int h, TargetTypeFilter *arr) : Aoe(w, h) {
		for (int i = 0; i < w; i++) { // row
			for (int j = 0; j < h; j++) { // col
				set(i, j, arr[i * w + j]);
			}
		}
	}

	void setAoe(Aoe a) {
		int u = (width - a.width) / 2;
		int v = (height - a.height) / 2;
		setAoe(a, u, v);
	}
	void setAoe(Aoe a, int u, int v) {
		for (int i = 0; i < a.width; i++) { // row
			for (int j = 0; j < a.height; j++) { // col
				set(i + u, j + v, a.get(i, j));
			}
		}
	}
	void addAoe(Aoe a, int u, int v) {
		for (int i = 0; i < a.width; i++) { // row
			for (int j = 0; j < a.height; j++) { // col
				addAt(i + u, j + v, a.get(i, j));
			}
		}
	}
	void removeAoe(Aoe a, int u, int v) {
		for (int i = 0; i < a.width; i++) { // row
			for (int j = 0; j < a.height; j++) { // col
				removeAt(i + u, j + v, a.get(i, j));
			}
		}
	}
	void addAt(int x, int y, TargetTypeFilter f) {
		set(x, y, get(x, y) + f);
	}
	void removeAt(int x, int y, TargetTypeFilter f) {
		set(x, y, get(x, y) - f);
	}

	void print() {
		int i = 0;
		for (auto val : table) {
			i++;
			if (i == width) {
				i = 0;
				std::cout << to_string(val.value) << endl;
			} else {
				std::cout << to_string(val.value) + "\t";
			}
		}
	}
	void foreach(std::function<void(TargetTypeFilter)> f) {
		for (auto val : table) {
			f(val);
		}
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