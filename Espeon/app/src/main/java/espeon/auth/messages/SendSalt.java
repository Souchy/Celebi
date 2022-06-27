package espeon.auth.messages;

// import com.souchy.randd.annotationprocessor.ID;
import com.souchy.randd.commons.net.netty.bytebuf.BBMessage;
import com.souchy.randd.commons.tealwaters.commons.Deserializer;

import io.netty.buffer.ByteBuf;

// @ID(id = 0003)
public class SendSalt implements BBMessage {
	
	public String salt;
	
	public SendSalt() { }
	public SendSalt(String salt) {
		this.salt = salt;
	}

	@Override
	public ByteBuf serialize(ByteBuf out) {
		out.writeBoolean(salt == null);
		if(salt != null) 
			writeString(out, salt);
		return out;
	}

	@Override
	public BBMessage deserialize(ByteBuf in) {
		var nul = in.readBoolean();
		if(!nul) salt = readString(in);
		return this;
	}

	@Override
	public Deserializer<ByteBuf, BBMessage> create() {
		return new SendSalt();
	}

	@Override
	public int getBufferCapacity() {
		return 0;
	}
	
}
