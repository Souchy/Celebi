﻿using souchy.celebi.eevee.face.conditions;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.interfaces;

namespace souchy.celebi.eevee.impl.objects
{
    /*
     * Normal Spell
     * Instant Spell
     * 
     * 
     * MTG Phases: 
     *      - Starting turn phase: 
     *              A reset resources, 
     *              A can cast instant spells
     *              B can cast instant spells to counter you
     *      - Normal turn: 
     *              A can cast normal spells
     *      - Ending turn phase: 
     *              A can cast instant spells
     *              B can cast instant spells to counter you
     * 
     * 
     * 
     */


    public class Spell : ISpell
    {
        public IID fightUid { get; init; }
        public IID modelId { get; set; }
        public IID entityUid { get; init; }

        public ICondition sourceCondition { get; set; }
        public ICondition targetFilter { get; set; }
        
        public ISpellProperties properties { get; set; }
        public List<ICost> costs { get; set; }

        public List<IID> effectIds { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}