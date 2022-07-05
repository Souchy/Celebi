package espeon.game.handlers;

import com.souchy.randd.commons.net.netty.bytebuf.BBMessageHandler;

import espeon.game.Conditions;
import espeon.game.Diamonds;
import espeon.game.jade.SpellModel;
// import espeon.game.jade.SpellModel.Statement;
import espeon.game.messages.game.*;
import espeon.game.red.ActionPipeline;
import espeon.game.red.Board;
import espeon.game.red.Creature;
import espeon.game.red.Spell;
import io.netty.channel.ChannelHandlerContext;
import espeon.game.red.ActionPipeline.EffectAction;
import espeon.game.red.compiledEffects.*;
import espeon.game.jade.SpellModel.*;

public class GameActionHandler implements BBMessageHandler<GameAction> {

    @Override
    public Class<GameAction> getMessageClass() {
        return GameAction.class;
    }

    @Override
    public void handle(ChannelHandlerContext client, GameAction message) {
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
                castSpell(client, message);
                break;
        }
    }


    private void castSpell(ChannelHandlerContext client, GameAction message) {
        Spell s = Diamonds.getSpellInstance(message.actionid);
        SpellModel sm = Diamonds.getSpellModel(s.modelid);
        Creature caster = Diamonds.getCreatureInstance(message.client);
        if(!caster.spells.contains(s.id)) {
            // caster does not know this spell ! 
            // return error message
            return;
        }

        ActionPipeline p = new ActionPipeline(message.client, message.actionid, message.cellid);

        for (var cost : sm.costs) {
            // new CostAction
        }
        
        for(var line : sm.lines) {
            processStatement(p, line);
        }
        // processStatement(p, sm.root);
    }

    private void processStatement(ActionPipeline p, SpellLine sl) {
        if(sl.isGroup()) {
            var group = sl.asGroup();
            if(Conditions.verify(group.condition, p)) {
                for (var line : group.children) {
                    processStatement(p, line);
                }
            } else {
                for (var line : group.childrenOtherwise) {
                    processStatement(p, line);
                }
            }
        } else {
            var s = sl.asStatement();
            Board b = null;
            var cells = b.filterCells(p.cellid, s.effect.aoe);
            for(var c : cells) { // for(int i = 0; i < s.effect.aoe.size(); i++) {
                p.push(s.effect, p.sourceid, c.id);
            }
        }
    }
    
}
