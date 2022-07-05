package espeon.game.messages.game;

import com.souchy.randd.annotationprocessor.ID;
import com.souchy.randd.commons.net.netty.bytebuf.BBMessage;
import com.souchy.randd.commons.tealwaters.commons.Deserializer;

import espeon.game.types.GameActionType;
import io.netty.buffer.ByteBuf;

@ID(id = 1001)
public class GameAction implements BBMessage {
    
	public int client; // pas besoin pour client->server mais besoin pour server->clients
    public int actionid;
	// public int cellx;
    // public int celly;
	public int cellid;

	public GameActionType type() {
		return GameActionType.valueOf(actionid);
	}

	public GameAction() {}
	public GameAction(int client, int actionid, int cellid) { //int cellx, int celly) {
		this.client = client;
        this.actionid = actionid;
		this.cellid = cellid;
		// this.cellx = cellx;
		// this.celly = celly;
	}

	@Override
	public ByteBuf serialize(ByteBuf out) {
		writeInt(out, client);
		writeInt(out, actionid);
		writeInt(out, cellid);
		// writeInt(out, cellx);
		// writeInt(out, celly);
		return out;
	}

	@Override
	public BBMessage deserialize(ByteBuf in) {
		actionid = readInt(in);
		cellid = readInt(in);
		// cellx = readInt(in);
		// celly = readInt(in);
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
