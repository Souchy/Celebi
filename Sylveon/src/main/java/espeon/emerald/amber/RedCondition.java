package espeon.emerald.amber;

import espeon.emerald.Red;
import espeon.game.jade.Condition.Actor;
import espeon.game.jade.Condition.ConditionLink;

public class RedCondition {
    

    public Actor actor(int id) {
        return Actor.valueOf(Red.jedis.get("celebi:amber:condition:" + id + ":actor"));
    }
    
    public ConditionLink link(int id) {
        return ConditionLink.valueOf(Red.jedis.get("celebi:amber:condition:" + id + ":link"));
    }

}
