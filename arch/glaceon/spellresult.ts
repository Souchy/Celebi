import { EffectType } from "../common/effects";
import { Cell, Creature } from "../common/entity";
import { Cost, Spell } from "../common/spells";


export class spellresult {
    // public spell: Spell;
    public source: Creature
    public cellCast: Cell;
    public finalCosts: Cost[];
    public children: effectresult[];
}

export class effectresult {
    public type: EffectType;
    public parent: any;
    public children: effectresult[];
    public data: any;
}


