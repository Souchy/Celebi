package espeon.game.jade;

import espeon.game.jade.aoes.Zone;
import espeon.game.red.Aoe;
import espeon.game.types.EffectType;

public abstract class EffectModel {

    // public int id;
    // public Aoe aoe = new Aoe();
    public Zone zone = new Zone();
    // public TargetTypeFilter filter;

    public abstract EffectType type(); // {
        // return EffectType.valueOf(id);
    // }

    
    /**
     * How many entities the effect can apply to in a tower of entities
     */
    public int towerHeightMax = 1;
    /**
     * If the effect orders from the bottom to the top of the tower or inversely
     */
    public boolean towerFromBottom = true;
    /**
     * If the effect applies to cells
     */
    public boolean appliesCells;
    /**
     * If the effect applies to creatures
     */
    public boolean appliesCreatures;


    public int depthMax = 0;
    public boolean appliesFlying;
    public boolean appliesUnderground;
    public boolean appliesWet;
    public boolean appliesGrounded;


    public int height;
    public int directionUpDown;
    public int length;

    // states
    {
        // underground
        // wet
        // flying
        // carried
        // carrying

        // heavy (cant be carried)
        // stable (can't be teleported)
        // rooted (can't be translated)
        // invisible

    }

}
