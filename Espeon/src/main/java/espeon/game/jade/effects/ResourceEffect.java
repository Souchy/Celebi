package espeon.game.jade.effects;

import espeon.game.jade.EffectModel;
import espeon.game.types.EffectType;
import espeon.game.types.Mod;

public class ResourceEffect extends EffectModel {
	
	public Mod mod;
	public int value;
	
	@Override
	public EffectType type() {
		return EffectType.resource;
	}

}
