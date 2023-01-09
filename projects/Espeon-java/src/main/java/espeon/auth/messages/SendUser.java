package espeon.auth.messages;

import com.souchy.randd.annotationprocessor.ID;
import com.souchy.randd.commons.net.netty.bytebuf.BBMessage;
import com.souchy.randd.commons.tealwaters.commons.Deserializer;

import espeon.auth.jade.User;
import io.netty.buffer.ByteBuf;

@ID(id = 0005)
public class SendUser implements BBMessage {
	
	public User user;
	
	public SendUser() {}
	public SendUser(User user) {
		this.user = user;
	}

	@Override
	public ByteBuf serialize(ByteBuf out) {
		out.writeBoolean(user == null);
		if(user == null) return out;
		user.serialize(out);
		return out;
	}

	@Override
	public BBMessage deserialize(ByteBuf in) {
		var nul = in.readBoolean();
		if(nul) return this;
		user = new User();
		user.deserialize(in);
		return this;
	}

	@Override
	public Deserializer<ByteBuf, BBMessage> create() {
		return new SendUser();
	}

	@Override
	public int getBufferCapacity() {
		return 0;
	}
	
}
