package espeon.game.net.messages;

import com.souchy.randd.commons.net.netty.bytebuf.BBMessage;
import com.souchy.randd.commons.tealwaters.commons.Deserializer;

import io.netty.buffer.ByteBuf;

public class Move implements BBMessage {

    @Override
    public Deserializer<ByteBuf, BBMessage> create() {
        // TODO Auto-generated method stub
        return null;
    }
    
}
