#pragma once
#include "../Effect.h"
#include "../Status.h"

class AddStatus : public Effect
{
	Status s;
};

class RemoveStatus : public Effect
{
	int statusId;
	int durationRemoved;
	int stacksRemoved;
};

class RemoveAllStatus : public Effect
{
	int statusId;
	int stacksRemoved;
};
