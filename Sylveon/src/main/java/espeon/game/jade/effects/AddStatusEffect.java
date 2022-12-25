package espeon.game.jade.effects;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import espeon.game.jade.EffectModel;
import espeon.game.jade.Trigger;
import espeon.game.red.Status.MergeStrategy;
import espeon.game.red.Status.StatusMod;
import espeon.game.types.EffectType;

public class AddStatusEffect extends EffectModel {


    public static enum Keywords {
        cz, // number of creatures in the EffectModel's aoe
        sz  // sum of the results of a previous effect's aoe (ex: sum of damage, sum of ap stolen, sum of hp healed)
        ;
    }
    // = 10$cz : 10 * le nombre de créatures dans la zone de l'effet actuel
    // = 10$cz0 : 10 * le nombre de créatures dans la zone de l'effet #0 dans l'action actuelle

    // = 10$sz0 : 10 * la sum de l'effet #0 de l'action actuelle
    // = 10$sz1 : 10 * la sum de l'effet #1 de l'action actuelle

    public int spellModelSource; // to set when the action is applied, not from the db?
    public Map<Integer, String> stats; // put them by string so we can evaluate them
    public Map<StatusMod, String> mods = new HashMap<>(); // put them by string so we can evaluate them
    
    public MergeStrategy mergeStrategy;

    // triggers for the list of effects
    public List<Trigger> triggers;

    @Override
    public EffectType type() {
        return EffectType.status;
    }

}
