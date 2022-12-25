#pragma once

#include <functional>

class Trigger {
public: 
	std::function<void()> lambda;
	Trigger() {}
	Trigger(std::function<void()> action) {
		lambda = action;
	}
	void trigger() {
		lambda();
	}
};


class OnEffect : public Trigger {
public:
	int effectTypeID;
	bool ifSource = false;
	bool ifTarget = false;
	OnEffect(void *action(), int effectTypeID) : Trigger(action)  {
		this->effectTypeID = effectTypeID;
	}
};

class OnTimeline : public Trigger {
public:
	bool onTurn = false;
	bool onRound = false;
	bool onStart = false;
	bool onEnd = false;
	OnTimeline(void* action()) : Trigger(action) {
	}
};
