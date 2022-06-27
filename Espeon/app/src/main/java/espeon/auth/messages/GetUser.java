package espeon.auth.messages;

// import com.souchy.randd.annotationprocessor.ID;
import com.souchy.randd.commons.net.netty.bytebuf.BBMessage;
import com.souchy.randd.commons.tealwaters.commons.Deserializer;

import io.netty.buffer.ByteBuf;

// @ID(id = 0004)
public class GetUser implements BBMessage {
	
	public String username;
	public String hashedPassword;
	
	public GetUser() {}
	public GetUser(String username, String hashedPassword) {
		this.username = username;
		this.hashedPassword = hashedPassword;
	}
	
	@Override
	public ByteBuf serialize(ByteBuf out) {
		writeString(out, username);
		writeString(out, hashedPassword);
		return out;
	}

	@Override
	public BBMessage deserialize(ByteBuf in) {
		username = readString(in);
		hashedPassword = readString(in);
		return this;
	}

	@Override
	public Deserializer<ByteBuf, BBMessage> create() {
		return new GetUser();
	}

	@Override
	public int getBufferCapacity() {
		return 0;
	}
	
}