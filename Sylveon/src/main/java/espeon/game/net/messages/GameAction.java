package espeon.game.net.messages;

import com.souchy.randd.annotationprocessor.ID;
import com.souchy.randd.commons.net.netty.bytebuf.BBMessage;
import com.souchy.randd.commons.tealwaters.commons.Deserializer;

import espeon.game.types.GameActionType;
import io.netty.buffer.ByteBuf;

@ID(id = 1001)
public class GameAction implements BBMessage {
    
	public int client; // pas besoin pour client->server mais besoin pour server->clients
	public GameActionType type;
	// spell id
    public int spellid;
	public int cellid;
	

	public GameAction() {}
	public GameAction(GameActionType type, int client, int actionid, int cellid) { 
		this.client = client;
		this.type = type;
        this.spellid = actionid;
		this.cellid = cellid;
	}

	@Override
	public ByteBuf serialize(ByteBuf out) {
		writeInt(out, client);
		writeInt(out, type.ordinal());

		writeInt(out, spellid);
		writeInt(out, cellid);
		return out;
	}

	@Override
	public BBMessage deserialize(ByteBuf in) {
		int typeid = readInt(in);
		type = GameActionType.values()[typeid];

		spellid = readInt(in);
		cellid = readInt(in);
		return this;
	}

	@Override
	public Deserializer<ByteBuf, BBMessage> create() {
		return new GameAction();
	}

	@Override
	public int getBufferCapacity() {
		return 0;
	}

}
