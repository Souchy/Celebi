package espeon.game.net.handlers;

import java.util.ArrayList;
import java.util.List;

import com.souchy.randd.commons.net.netty.bytebuf.BBMessageHandler;

import espeon.game.net.messages.Move;
import espeon.game.red.Entity;
import io.netty.channel.ChannelHandlerContext;

public class MoveHandler implements BBMessageHandler<Move> {

    @Override
    public Class<Move> getMessageClass() {
        return Move.class;
    }

    @Override
    public void handle(ChannelHandlerContext client, Move message) {
        int currentPlaying = Entity.noid; // f.timeline.getCurrentPlayingCreature();

        // check if creature is owned by the client
        // otherwise refuse the message
        if(false) {
            return;
        }
        
        List<Integer> cells = new ArrayList<>();
        // check path for obstacles / plausibility
        for(var cellid : cells) {
            // trigger cell.onmove but not onend
        }
    }

    
}
