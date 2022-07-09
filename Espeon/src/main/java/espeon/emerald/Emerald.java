package espeon.emerald;

import java.time.Instant;
import java.time.ZoneId;
import java.time.ZonedDateTime;
import java.util.HashMap;
import java.util.Map;

import org.bson.BsonReader;
import org.bson.BsonWriter;
import org.bson.codecs.Codec;
import org.bson.codecs.DecoderContext;
import org.bson.codecs.EncoderContext;
import org.bson.codecs.configuration.CodecRegistries;
import org.bson.codecs.pojo.PojoCodecProvider;

import com.mongodb.ConnectionString;
import com.mongodb.MongoClientSettings;
// import com.mongodb.reactivestreams.client.MongoClient;
// import com.mongodb.reactivestreams.client.MongoClients;
// import com.mongodb.reactivestreams.client.MongoCollection;
import com.mongodb.client.MongoClient;
import com.mongodb.client.MongoClients;
import com.mongodb.client.MongoCollection;
import com.souchy.randd.commons.tealwaters.commons.Namespace.MongoNamespace;
import com.souchy.randd.commons.tealwaters.io.files.JsonConfig;
import com.souchy.randd.commons.tealwaters.logging.Log;
import com.souchy.randd.commons.tealwaters.logging.Logging;
// import com.souchy.randd.jade.matchmaking.Lobby;
// import com.souchy.randd.jade.matchmaking.QueueeBlind;
// import com.souchy.randd.jade.matchmaking.QueueeDraft;
// import com.souchy.randd.jade.meta.Deck;
// import com.souchy.randd.jade.meta.Match;
// import com.souchy.randd.jade.meta.New;
// import com.souchy.randd.jade.meta.User;

import espeon.auth.jade.User;
import espeon.game.jade.SpellModel;
import espeon.game.red.Action;

/**
 * MongoDB access
 *
 * @author Blank
 *
 */
public final class Emerald {


	//private
	static MongoClient client;

	private static final String root = "Celebi";
	
//	static {
//		init();
//	}
	
	public static void init() {
		var conf = JsonConfig.read(EmeraldConf.class);
		init(conf.ip, conf.port, conf.username, conf.password);
	}

	/**
	 * Allows re-initializing Emerald with different parameters
	 * @param ip - Default is "localhost"
	 * @param port - Default is 27017
	 * @param user - Default is ""
	 * @param pass - Default is ""
	 */
	public static void init(String ip, int port, String user, String pass) {
		if(client != null) client.close();
		var credentials = "";
		if(user != null && !user.trim().isEmpty()) credentials = user + ":" + pass + "@";
		var registry = CodecRegistries.fromRegistries(
				MongoClientSettings.getDefaultCodecRegistry(),
				CodecRegistries.fromCodecs(new ZonedDateTimeCodec()),
				CodecRegistries.fromProviders(PojoCodecProvider.builder().automatic(true).build())
		);
		var connectStr = "mongodb://" + credentials + ip + ":" + port;
		Log.info("emerald init: " + connectStr);
		
		var settings = MongoClientSettings.builder()
			//.credential(MongoCredential.createCredential("robyn", "admin", new char[] { 'z' }))
			.applyConnectionString(new ConnectionString(connectStr))
			.codecRegistry(registry)
		.build();
		client = MongoClients.create(settings);

		Logging.streams.add(l -> {
			try {
				Emerald.logs().insertOne(l);
			} catch (Exception e) {
//				System.out.println(e);
			}
		});
	}

	public static class ZonedDateTimeCodec implements Codec<ZonedDateTime> {
		@Override
		public void encode(BsonWriter writer, ZonedDateTime value, EncoderContext encoderContext) {
			writer.writeDateTime(value.toInstant().toEpochMilli());
		}
		@Override
		public Class<ZonedDateTime> getEncoderClass() {
			return ZonedDateTime.class;
		}
		@Override
		public ZonedDateTime decode(BsonReader reader, DecoderContext decoderContext) {
			return ZonedDateTime.ofInstant(Instant.ofEpochMilli(reader.readDateTime()), ZoneId.systemDefault());
		}
	}


	private static <T> MongoCollection<T> get(MongoNamespace space, Class<T> clazz) {
		return client.getDatabase(space.db).<T>getCollection(space.collection, clazz);
	}


	// -------------------------------------------------------------------  meta

	public static MongoCollection<Log> logs() {
		return collection(Log.class); // get(logs, Log.class);
	}
	public static MongoCollection<User> users() {
		return collection(User.class); // get(users, User.class);
	}

	public static MongoCollection<SpellModel> spells() {
		return collection(SpellModel.class);
	}
	public static MongoCollection<Action> actions() {
		return collection(Action.class);
	}

	/*
	public static MongoCollection<Deck> decks() {
		return collection(Deck.class); // get(decks, Deck.class);
	}

	public static MongoCollection<Match> matchs() {
		return collection(Match.class); // get(matches, Match.class);
	}
	public static MongoCollection<New> news() {
		return collection(New.class); // get(news, New.class);
	}
//	public static MongoCollection<QueuedUser> queue_simple_unranked() {
//		return get(queue_simple_unranked, QueuedUser.class);
//	}
//	public static MongoCollection<QueuedUser> queue_simple_ranked() {
//		return get(queue_simple_ranked, QueuedUser.class);
//	}
	public static MongoCollection<QueueeBlind> queue_simple_blind() {
		return collection(QueueeBlind.class); // get(queue_simple_blind, QueueeBlind.class);
	}
	public static MongoCollection<QueueeDraft> queue_simple_draft() {
		return collection(QueueeDraft.class); // get(queue_simple_draft, QueueeDraft.class);
	}
	public static MongoCollection<Lobby> lobbies(){
		return collection(Lobby.class); // get(lobbies, Lobby.class);
	}
*/

	public static <T> MongoCollection<T> collection(Class<T> clazz) {
		var fullpackag = clazz.getPackageName();
		var packag = fullpackag.substring(fullpackag.lastIndexOf('.') + 1);
		var collection = clazz.getSimpleName();
//		System.out.println("Emerald collection : " + root + "#" + packag);
		return client.getDatabase(root + "#" + packag).getCollection(collection, clazz);
	}

}