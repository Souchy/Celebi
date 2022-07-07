package espeon.game.net.handlers;

import com.souchy.randd.commons.net.netty.bytebuf.BBMessageHandler;

import espeon.game.net.messages.Move;
import io.netty.channel.ChannelHandlerContext;

public class MoveHandler implements BBMessageHandler<Move> {

    @Override
    public Class<Move> getMessageClass() {
        return Move.class;
    }

    @Override
    public void handle(ChannelHandlerContext client, Move message) {
        // TODO Auto-generated method stub
        
    }


}
