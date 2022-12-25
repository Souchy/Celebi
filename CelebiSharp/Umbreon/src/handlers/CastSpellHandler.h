#pragma once

#include "PacketHandler.h"
#include "src/packets/CastSpell.h"
#include "src/models/Fight.h"
#include "src/models/Effect.h"
#include "src/models/Spell.h"
#include "src/main/ActionPipeline.h"

class CastSpellHandler : public PacketHandler {

public:

	void handle(Fight f, CastSpell packet) {
		Creature c = f.getCreature(packet.sourceid);
		ActionPipeline p;

	}

};

