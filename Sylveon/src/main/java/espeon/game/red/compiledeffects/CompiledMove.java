package espeon.game.red.compiledeffects;

public class CompiledMove extends CompiledEffect {
    
    public int creatureid;
    public int cellid;

    @Override
    public String toString() {
        return String.format("CompiledMove : { sourceid=%s, creatureid=%s, cellid=%s }", sourceid, creatureid, cellid);
    }
}
