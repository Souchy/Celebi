package espeon.game.handlers;

import javax.swing.Action;

import com.souchy.randd.commons.net.netty.bytebuf.BBMessageHandler;

import espeon.game.controllers.ActionPipeline;
import espeon.game.controllers.Conditions;
import espeon.game.controllers.Diamonds;
import espeon.game.controllers.ActionPipeline.EffectAction;
import espeon.game.jade.SpellModel;
import espeon.game.jade.Statement;
// import espeon.game.jade.SpellModel.Statement;
import espeon.game.messages.game.*;
import espeon.game.red.Board;
import espeon.game.red.Creature;
import espeon.game.red.Spell;
import espeon.game.red.compiledeffects.*;
import io.netty.channel.ChannelHandlerContext;
import espeon.game.jade.SpellModel.*;

public class GameActionHandler implements BBMessageHandler<GameAction> {

    @Override
    public Class<GameAction> getMessageClass() {
        return GameAction.class;
    }

    @Override
    public void handle(ChannelHandlerContext client, GameAction message) {
        ActionPipeline p = new ActionPipeline(message.client, message.actionid, message.cellid);
        switch(message.type()) {
            case move:
                break;
            case pass:
                break;
            case quit: 
                break;
            case forfeit:
                break;
            case spell:
                castSpell(client, p);
                break;
        }
    }


    public void castSpell(ChannelHandlerContext client, ActionPipeline p) {
        Spell s = Diamonds.getSpell(p.actionid);
        SpellModel sm = Diamonds.getSpellModel(s.modelid);
        Creature caster = Diamonds.getCreatureInstance(p.sourceid);
        if(!caster.spells.contains(s.id)) {
            // caster does not know this spell ! 
            // return error message
            return;
        }

        for (var cost : sm.costs) {
            // new CostAction
        }
        // check spell memory conditions
        
        for(var line : sm.statements) {
            // System.out.println("Cast spell statement: " + line.hashCode());
            p.processStatement(line);
        }
        // processStatement(p, sm.root);
    }
    
}
