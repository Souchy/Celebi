package espeon.auth.jade;

import java.util.ArrayList;
import java.util.Date;
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
	 * Mongo id
	 */
	public ObjectId _id;

	/**
	 * The authorization level of the user
	 */
	public UserLevel authLevel = UserLevel.normal;
	/**
	 * If the user was created here or provided by external services
	 */
	public UserType userType;
	/**
	 * The date this account was created
	 */
	public Date creationDate = new Date();
	/**
	 * Everything that happened to this account
	 */
	public List<AccountLog> logs = new ArrayList<>();
	/**
	 * The user's email
	 */
	public String email;
	/**
	 * If the email has been verified
	 */
	public boolean verifiedEmail;
	/**
	 * The user's phone number
	 */
	public String phone;
	/**
	 * If the phone number has been verified (2-FA)
	 */
	public boolean verifiedPhone;

	/**
	 * The publicly shown Pseudonym
	 */
	public String pseudo;
	/**
	 * The username to log in <br>
	 * Null by default (users provided from a 3rd party like google use only an email)
	 */
	public String username;
	/**
	 * The hashed password <br>
	 * Null by default (users provided from a 3rd party like google use only an email)
	 */
	public String password;
	/**
	 * The salt to hash the password <br>
	 * Null by default (users provided from a 3rd party like google use only an email)
	 */
	public String salt;

	public static final String name__id = "_id";
	public static final String name_authLevel = "authLevel";
	public static final String name_username = "username";
	public static final String name_password = "password";
	public static final String name_pseudo = "pseudo";
	public static final String name_email = "email";
	public static final String name_verifiedEmail = "verifiedEmail";
	public static final String name_phone = "phone";
	public static final String name_verifiedPhone = "verifiedPhone";

	@Override
	public ByteBuf serialize(ByteBuf out) {
		writeString(out, _id.toString());
		out.writeByte(authLevel.ordinal());
		writeString(out, username);
		writeString(out, password);
		writeString(out, salt);
		writeString(out, pseudo);
		writeString(out, email);
		writeString(out, phone);
		out.writeBoolean(verifiedEmail);
		out.writeBoolean(verifiedPhone);
		return out;
	}

	@Override
	public BBMessage deserialize(ByteBuf in) {
		_id = new ObjectId(readString(in));
		authLevel = UserLevel.values()[in.readByte()];
		username = readString(in);
		password = readString(in);
		salt = readString(in);
		pseudo = readString(in);
		email = readString(in);
		phone = readString(in);
		verifiedEmail = in.readBoolean();
		verifiedPhone = in.readBoolean();
		return null;
	}

	@Override
	public String toString() {
		return String.format("{ %s, %s, %s, %s }", _id, pseudo, username, password);
	}

}
