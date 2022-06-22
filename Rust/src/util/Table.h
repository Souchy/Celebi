#pragma once

#include <vector>

#include "../main/Target.h"

class Table {
private:
    vector<TargetTypeFilter> table;

public:
	int width;
	int height;

	Table(int width, int height) {
		this->width = width;
		this->height = height;
	}

	TargetTypeFilter get(int row, int col) {
		return table[row * width + col];
	}

	void set(int row, int col, TargetTypeFilter val) {
		table[row * width + col] = val;
	}

	void addColumn(int colIndex) {
        // shift columns to the right
	}

	void addRow(int rowIndex) {
        // shift rows down
	}

	void resize(int width, int height) {
		table.resize(width * height);
	}

};
