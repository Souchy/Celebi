package espeon.game.controllers;

import java.util.HashMap;
import java.util.Map;

import espeon.game.jade.SpellModel;
import espeon.game.red.Action;
import espeon.game.red.Creature;
import espeon.game.red.Entity;
import espeon.game.red.Spell;

public class Diamonds {

    public static Fight getFight(int id) {
        return fights.get(id);
    }
    public static SpellModel getSpellModel(String id) {
        return spellModels.get(id);
    }
    public static Action getAction(String actionid) {
        return actions.get(actionid);
    }
    
    public static Entity getEntity(int fightid, int id) {
        return creatures.get(id);
    }
    public static Creature getCreatureInstance(int fightid, int id) {
        return creatures.get(id);
    }
    public static Spell getSpell(int fightid, int id) {
        return spells.get(id);
    }
    // public static Fight getFightByClient(String clientid) {
    //     // get the fight by client.fightid
    //     return fights.get(clientid);
    // }
    // public static SpellModel getStatusModel(int id) {
    //     return statusModels.get(id);
    // }

    
    public static void setFight(int id, Fight f) {
        fights.put(id, f);
    }
    public static void setSpellModel(String id, SpellModel s) {
        spellModels.put(id, s);
    }
    public static void setAction(String id, Action a) {
        actions.put(id, a);
    }

    public static void setCreature(int id, Creature c) {
        creatures.put(id, c);
    }
    public static void setSpell(int id, Spell s) {
        spells.put(id, s);
    }

    // public static void setFightClient(int id, Fight f) {
    //     // set fight id in the client's memory
    //     fights.put(id, f);
    // }


    // Secret database simulation before we set up redis
    private static Map<Integer, Spell> spells = new HashMap<>();
    private static Map<String, SpellModel> spellModels = new HashMap<>();
    private static Map<Integer, Creature> creatures = new HashMap<>();
    // private static Map<String, Integer> fights = new HashMap<>();
    private static Map<Integer, Fight> fights = new HashMap<>();
    private static Map<String, Action> actions = new HashMap<>();

}
