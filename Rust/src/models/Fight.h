#pragma once

#include <vector>
#include "Creature.h";

class Fight {
public:
	vector<Creature> creatures;
	vector<int> timeline;

	Creature getCreature(int id) {
		for (auto c : creatures)
			if (c.id == id)
				return c;
		return nullptr;
	}

};

