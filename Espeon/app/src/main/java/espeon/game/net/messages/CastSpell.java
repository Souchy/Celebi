package espeon.game.net.messages;

import com.souchy.randd.annotationprocessor.ID;
import com.souchy.randd.commons.net.netty.bytebuf.BBMessage;
import com.souchy.randd.commons.tealwaters.commons.Deserializer;

import io.netty.buffer.ByteBuf;

@ID(id = 1002)
public class CastSpell implements BBMessage {

    public int spellid;
    public int cellid;

    @Override
    public Deserializer<ByteBuf, BBMessage> create() {
        return new CastSpell();
    }
    
}
