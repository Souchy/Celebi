package espeon.auth.jade;

import java.util.ArrayList;
import java.util.List;

import org.bson.types.ObjectId;

import com.souchy.randd.commons.net.netty.bytebuf.BBDeserializer;
import com.souchy.randd.commons.net.netty.bytebuf.BBMessage;
import com.souchy.randd.commons.net.netty.bytebuf.BBSerializer;

import io.netty.buffer.ByteBuf;
import io.netty.util.AttributeKey;


public class User implements BBSerializer, BBDeserializer {

	public static final AttributeKey<User> attrkey = AttributeKey.newInstance("jade.meta.user");
	public static final AttributeKey<Integer> triesKey = AttributeKey.newInstance("jade.meta.user.tries");

	/**
	 * mongo id
	 */
	public ObjectId _id;
	
	/**
	 * This is the authorization level of the user
	 */
	public UserLevel level = UserLevel.User;
	/**
	 * This is the username to log in
	 */
	public String username;
	/**
	 * This is the hashed password
	 */
	public String password;
	/**
	 * This is the salt to hash the password
	 */
	public String salt;
	/**
	 * This is the publicly shown Pseudo
	 */
	public String pseudo;
	/**
	 * This is the user's email
	 */
	public String email;
	/**
	 * This is if the email has been verified (user creates account -> server sends email for confirmation -> user click confirmation link)
	 */
	public boolean verifiedEmail;
	/**
	 * This is the user's phone number
	 */
	public String phone;
	/**
	 * This is if the phone number has been verified (user asks for 2fa-> server sends SMS for confirmation -> user enters the code from the SMS into Amethyst)
	 */
	public boolean verifiedPhone;
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


	public static final String name__id = "_id";
	public static final String name_username = "username";
	public static final String name_password = "password";
	public static final String name_pseudo = "pseudo";
	public static final String name_email = "email";
	public static final String name_xp = "xp";
	public static final String name_gold = "gold";
	public static final String name_gems = "gems";
	public static final String name_friends = "friends";
	public static final String name_decks = "decks";
	public static final String name_unlockedCreatures = "unlockedCreatures";
	public static final String name_unlockedItems = "unlockedItems";
	public static final String name_unlockedSpells = "unlockedSpells";
	public static final String name_matchHistory = "matchHistory";
	public static final String name_level = "level";
	public static final String name_mmr = "mmr";
	
	
	@Override
	public ByteBuf serialize(ByteBuf out) {
//		Log.info("serialize user 1");
		writeString(out, _id.toString());
//		Log.info("serialize user 2");
		out.writeByte(level.ordinal());
//		Log.info("serialize user 3");
		writeString(out, username);
		writeString(out, password);
		writeString(out, salt);
		writeString(out, pseudo);
		writeString(out, email);
		writeString(out, phone);
//		Log.info("serialize user 4");
		out.writeBoolean(verifiedEmail);
		out.writeBoolean(verifiedPhone);
//		Log.info("serialize user 5");
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
//		Log.info("serialize user 8");
		return out;
	}
	@Override
	public BBMessage deserialize(ByteBuf in) {
		_id = new ObjectId(readString(in));
		level = UserLevel.values()[in.readByte()];
		username = readString(in);
		password = readString(in);
		salt = readString(in);
		pseudo = readString(in);
		email = readString(in);
		phone = readString(in);
		verifiedEmail = in.readBoolean();
		verifiedPhone = in.readBoolean();
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
	
	@Override
	public String toString() {
		return String.format("{ %s, %s, %s, %s }", _id, pseudo, username, password);
	}

}