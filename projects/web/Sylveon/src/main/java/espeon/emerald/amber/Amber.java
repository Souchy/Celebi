package espeon.emerald.amber;

import com.souchy.randd.annotationprocessor.ID;

import espeon.game.red.Aoe;
import espeon.game.red.Entity;

public class Amber {
    
    static final String redPath = "celebi:amber:";
    public static final RedSpellModel spells = new RedSpellModel();
    public static final RedCreatureModel creatures = new RedCreatureModel();
    public static final RedAction actions = new RedAction();

    // public static abstract class RedSpace {
    //     public String url;
    //     public Set<String> keys() {
    //         return Red.jedis.keys(url + "?");
    //     }
    // }

    private static final class RedAoe {
        public Aoe getAoe() {
            return null;
        }
    }
    
}
