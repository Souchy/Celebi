#pragma once

#include <vector>
#include "Creature.h";

class Fight {
public:
	vector<Creature> creatures;
	vector<int> timeline;

	Fight();
	~Fight();

	Creature* getCreature(int id) {
		for (auto c : creatures)
			if (c.id == id)
				return &c;
		throw nullptr;
	}

};

