#pragma once

#include "Packet.h"

class CastSpell : public Packet {
public:
	int sourceid; // only the server sets this when broadcasting
	int spellid;
	int cellid;

};

