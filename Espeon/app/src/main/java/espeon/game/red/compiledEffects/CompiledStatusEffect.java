package espeon.game.red.compiledEffects;

public class CompiledStatusEffect extends CompiledEffect {
    public int entityid; // cell or creature
    // add/moreve stack/duration ?
    public int statusid;
    
    @Override
    public String toString() {
        return String.format("CompiledDamage : { sourceid=%s, entityid=%s, statusid=%s }", sourceid, entityid, statusid);
    }
}
