package espeon.auth.messages;

import com.souchy.randd.annotationprocessor.ID;
import com.souchy.randd.commons.net.netty.bytebuf.BBMessage;
import com.souchy.randd.commons.tealwaters.commons.Deserializer;

import io.netty.buffer.ByteBuf;

@ID(id = 0002)
public class GetSalt implements BBMessage {
	
	public String username;
	
	public GetSalt() {
		
	}
	public GetSalt(String username) {
		this.username = username;
	}

	@Override
	public ByteBuf serialize(ByteBuf out) {
		writeString(out, username);
		return out;
	}

	@Override
	public BBMessage deserialize(ByteBuf in) {
		username = readString(in);
		return this;
	}

	@Override
	public Deserializer<ByteBuf, BBMessage> create() {
		return new GetSalt();
	}

	@Override
	public int getBufferCapacity() {
		return 0;
	}
	
}