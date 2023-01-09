package espeon.game.jade.effects;

import espeon.game.jade.EffectModel;
import espeon.game.types.EffectType;

/**
 * Prend un effet et l'applique à partir de conditions et positions d'origine supplémentaires. <br>
 * Compile dans le type d'effet choisi. (ex: CompiledDamage...)
 */
public class RebaseEffect  extends EffectModel {
    
	public EffectModel effect;

	public RebaseEffect(EffectModel e) {
		this.effect = e;
	}

    @Override
    public EffectType type() {
		return null;
    }

}
