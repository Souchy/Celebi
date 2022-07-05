package espeon.game.red.compiledEffects;

public class CompiledDamage extends CompiledEffect {
    public int creatureid;
    public int damage;

    @Override
    public String toString() {
        return String.format("CompiledDamage : { sourceid=%s, creatureid=%s, damage=%s }", sourceid, creatureid, damage);
    }
}
