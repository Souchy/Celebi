using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using static souchy.celebi.eevee.face.entity.IEntity;

namespace Espeon.souchy.celebi.espeon.eevee.impl.objects
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
     */

    public class Spell : ISpell
    {
        public IID entityUid { get; init; } = Eevee.RegisterIID();
        public IID fightUid { get; init; }
        public IID modelUid { get; set; }

        public int chargesRemaining { get; set; } = 1;
        public int cooldownRemaining { get; set; } = 0;
        public int numberOfCastsThisTurn { get; set; } = 0;
        public Dictionary<IID, int> numberOfCastPerEntityThisTurn { get; set; } = new Dictionary<IID, int>();

        public Spell(ScopeID scopeId)
        {
            this.fightUid = scopeId;
            //this.entityUid = Scopes.GetUIdGenerator(fightUid).next();
            //Scopes.GetRequiredScoped<IFight>(fightUid).spells.Add(entityUid, this);
            this.GetFight().spells.Add(entityUid, this);
        }

        public void Dispose()
        {
            Eevee.DisposeIID(this);
            //Scopes.DisposeIID(fightUid, entityUid);
        }
    }
}