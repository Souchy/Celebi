package espeon.auth.jade;

import java.util.ArrayList;
import java.util.List;

import org.bson.types.ObjectId;

import com.souchy.randd.commons.net.netty.bytebuf.BBDeserializer;
import com.souchy.randd.commons.net.netty.bytebuf.BBMessage;
import com.souchy.randd.commons.net.netty.bytebuf.BBSerializer;

import io.netty.buffer.ByteBuf;

public class CelebiUser implements BBSerializer, BBDeserializer {
    
	/**
	 * Match making rating
	 */
	public int mmr;
	
	/** can automatically unlock free things at x levels */
	public int xp; 
	/** free currency to buy things */
	public int gold; 
	/** paid currency to buy things */
	public int gems; 
	
	/** friends ids */
	public List<ObjectId> friends = new ArrayList<>();
	/** jade decks/teams ids */
	public List<ObjectId> decks = new ArrayList<>();
	/** match ids */
	public List<ObjectId> matchHistory = new ArrayList<>();
	/** creatures ids */
	public List<Integer> unlockedCreatures = new ArrayList<>();
	/** spells ids */
	public List<Integer> unlockedSpells = new ArrayList<>();
	//public List<ObjectId> unlockedItems = new ArrayList<>();

    
	public static final String name_mmr = "mmr";
	public static final String name_xp = "xp";
	public static final String name_gold = "gold";
	public static final String name_gems = "gems";
	public static final String name_friends = "friends";
	public static final String name_decks = "decks";
	public static final String name_unlockedCreatures = "unlockedCreatures";
	public static final String name_unlockedItems = "unlockedItems";
	public static final String name_unlockedSpells = "unlockedSpells";
	public static final String name_matchHistory = "matchHistory";


	@Override
	public ByteBuf serialize(ByteBuf out) {
		out.writeInt(mmr);
		out.writeInt(xp);
		out.writeInt(gold);
		out.writeInt(gems);
//		Log.info("serialize user 6");
		writeListString(out, friends);
		writeListString(out, decks);
		writeListString(out, matchHistory);
//		Log.info("serialize user 7");
		writeListInt(out, unlockedCreatures);
		writeListInt(out, unlockedSpells);
		return out;
	}
	@Override
	public BBMessage deserialize(ByteBuf in) {
		mmr = in.readInt();
		xp = in.readInt();
		gold = in.readInt();
		gems = in.readInt();
		friends = readList(in, () -> new ObjectId(readString(in)));
		decks = readList(in, () -> new ObjectId(readString(in)));
		matchHistory = readList(in, () -> new ObjectId(readString(in)));
		unlockedCreatures = readList(in, in::readInt);
		unlockedSpells = readList(in, in::readInt);
		return null;
	}

}
