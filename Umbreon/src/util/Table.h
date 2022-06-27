#pragma once

#include <vector>
#include <iostream>
#include <string>
using namespace std;


template <typename T>
class Table {
protected:
public:
	std::vector<T> table;
	int width;
	int height;
	T defaultValue;

	Table(int width, int height, T defaultValue) {
		this->width = width;
		this->height = height;
		this->defaultValue = defaultValue;

		//resize(width, height);
		table.resize(width * height);
		fill(defaultValue);
	}
	~Table() { }

	T get(int row, int col) {
		return table[row * width + col];
	}
	void set(int row, int col, T val) {
		if (row < 0 || row >= height) return;
		if (col < 0 || col >= width) return;
		table[row * width + col] = val;
	}
	int size() {
		return table.size(); // ou table.size() ou width * height
	}
	void fill(T val) {
		for (int i = 0; i < size(); i++) {
			table[i] = val;
		}
	}
	void resize(int width, int height) {
		//this->width = width;
		//this->height = height;
		//table.resize(width * height);
		while (this->width < width) {

		}
	}
	void addColumn(int colIndex) {
		width++;
		for (int row = 0; row < height; row++) {
			table.insert(table.begin() + colIndex + width * row, defaultValue);
		}
	}
	void removeColumn(int colIndex) {
		for (int row = height - 1; row >= 0; row--) {
			table.erase(table.begin() + colIndex + width * row);
		}
		width--;
	}
	void addRow(int rowIndex) {
		height++;
		for (int col = 0; col < width; col++) {
			table.insert(table.begin() + rowIndex * width + col, defaultValue);
		}
	}
	void removeRow(int rowIndex) {
		height--;
		for (int col = 0; col < width; col++) {
			table.erase(table.begin() + rowIndex * width + 0);
		}
	}
	void move(int x, int y) {
		while (x > 0) {
			addColumn(0);
			removeColumn(width - 1);
			x--;
		}
		while (x < 0) {
			removeColumn(0);
			addColumn(width);
			x++;
		}
		while (y > 0) {
			addRow(0);
			removeRow(height - 1);
			y--;
		}
		while (y < 0) {
			removeRow(0);
			addRow(height);
			y++;
		}
	}
};