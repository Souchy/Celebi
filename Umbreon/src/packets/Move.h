#pragma once
#include "Packet.h"

class Move : public Packet
{
	int sourceid; // only the server sets this when broadcasting
	int cellid;
};

