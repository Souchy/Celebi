package espeon.game.net.handlers;

import com.souchy.randd.commons.net.netty.bytebuf.BBMessageHandler;

import espeon.game.controllers.Diamonds;
import espeon.game.controllers.Fight;
import espeon.game.controllers.actionpipeline.NewPipeline;
import espeon.game.jade.SpellModel;
import espeon.game.net.ChannelAttributes;
import espeon.game.net.messages.CastSpell;
import espeon.game.red.Action;
import espeon.game.red.Creature;
import espeon.game.red.Spell;
import io.netty.channel.ChannelHandlerContext;

public class CastSpellHandler implements BBMessageHandler<CastSpell> {

    @Override
    public Class<CastSpell> getMessageClass() {
        return CastSpell.class;
    }

    @Override
    public void handle(ChannelHandlerContext client, CastSpell message) {
        String clientid = client.channel().id().asLongText();
        Fight f = client.channel().attr(ChannelAttributes.fightKey).get();
        castSpell(f, clientid, message);
    }

    public void castSpell(Fight f, String clientid, CastSpell message) {
        int currentPlaying = f.getCurrentPlayingCreature();
        Creature playingCreature = Diamonds.getCreatureInstance(f.id, currentPlaying);

        // check if creature is owned by the client
        // otherwise refuse the message
        if(!playingCreature.ownerid.equals(clientid)) {
            return;
        }
        
        NewPipeline p = new NewPipeline(f.id);
        Spell s = Diamonds.getSpell(f.id, message.spellid);
        SpellModel sm = Diamonds.getSpellModel(s.modelid);
        Action a = Diamonds.getAction(sm.actionid);

        p.start(a, currentPlaying, message.cellid);
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
