package espeon.game.net.handlers;

import java.util.ArrayList;
import java.util.List;

import com.souchy.randd.commons.net.netty.bytebuf.BBMessageHandler;

import espeon.game.controllers.Board;
import espeon.game.controllers.Conditions;
import espeon.game.controllers.Diamonds;
import espeon.game.controllers.actionpipeline.NewPipeline;
import espeon.game.jade.SpellModel;
import espeon.game.jade.Statement;
// import espeon.game.jade.SpellModel.Statement;
import espeon.game.net.messages.GameAction;
import espeon.game.red.Action;
import espeon.game.red.Creature;
import espeon.game.red.Entity;
import espeon.game.red.Spell;
import espeon.game.red.compiledeffects.*;
import io.netty.channel.ChannelHandlerContext;
import espeon.game.jade.SpellModel.*;
import espeon.game.jade.effects.moves.MoveEffect.MoveType;

public class GameActionHandler implements BBMessageHandler<GameAction> {

    @Override
    public Class<GameAction> getMessageClass() {
        return GameAction.class;
    }

    @Override
    public void handle(ChannelHandlerContext client, GameAction message) {
        int currentPlaying = Entity.noid; // f.timeline.getCurrentPlayingCreature();
        
        // check if creature is owned by the client
        // otherwise refuse the message
        if(false) {
            return;
        }

        switch(message.type) {
            case move:
                break;
            case spell:
                playSpell(client, message, currentPlaying);
                break;
            case pass:
                break;
            case forfeit:
                break;
        }
    }

    private void playMove(ChannelHandlerContext client, GameAction message, int currentPlaying) {
        List<Integer> cells = new ArrayList<>();
        // check path for obstacles / plausibility
        for(var cellid : cells) {
            // trigger cell.onmove but not onend
        }
    }

    private void playSpell(ChannelHandlerContext client, GameAction message, int currentPlaying) {
        NewPipeline p = new NewPipeline();
        Spell s = Diamonds.getSpell(message.spellid);

        p.start(null, currentPlaying, message.cellid);
    }

    /*/
    public void castSpell(ChannelHandlerContext client, NewPipeline p) {
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
    */

}
