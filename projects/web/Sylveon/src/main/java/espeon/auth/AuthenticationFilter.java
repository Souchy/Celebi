package espeon.auth;

import org.bson.types.ObjectId;
import static com.mongodb.client.model.Filters.and;
import static com.mongodb.client.model.Filters.eq;

import java.util.HashMap;
import java.util.Map;

import com.google.common.eventbus.EventBus;
import com.souchy.randd.commons.tealwaters.logging.Log;

import espeon.auth.jade.User;
import espeon.auth.jade.UserLevel;
import espeon.auth.messages.GetSalt;
import espeon.auth.messages.GetUser;
import espeon.auth.messages.SendSalt;
import espeon.auth.messages.SendUser;
import espeon.emerald.Emerald;
//import com.souchy.randd.commons.nettyauth.Authentication;
import io.netty.channel.Channel;
import io.netty.channel.ChannelHandler.Sharable;
import io.netty.channel.ChannelHandlerContext;
import io.netty.channel.ChannelInboundHandlerAdapter;

@Sharable
public class AuthenticationFilter extends ChannelInboundHandlerAdapter { //implements NettyMessageFilter { 

	/**
	 * Notifies of newly authenticated users and disconnected users
	 */
	public final EventBus bus = new EventBus();
	
	
	public final Map<ObjectId, Channel> userChannels = new HashMap<>();
	/**
	 * Minimum user level to be authorized to go through.
	 */
	public UserLevel minLevel = UserLevel.User;
	
	/** 
	 * Filter messages so only channels with a User attribute can go through 
	 */
	@Override
	public void channelRead(ChannelHandlerContext ctx, Object msg) throws Exception {
//		Log.info("AuthenticationFilter read");
		// si cest un message getsalt
		if(msg instanceof GetSalt) 
			getSalt(ctx, (GetSalt) msg);
		else
		// si cest un msg getuser
		if(msg instanceof GetUser) 
			getUser(ctx, (GetUser) msg);
		else 
		// sinon c'est qu'il doit déjà avoir un user attribute
		if(ctx.channel().hasAttr(User.attrkey)) 
			super.channelRead(ctx, msg); // if ctx has user attr, pass the message to the next handlers
		else
			// sinon c'est qu'il n'a pas été accepté donc close le channel
			ctx.channel().close();
	} 
	
	
	/** remove disconnecting users */
	@Override
	public void channelInactive(ChannelHandlerContext ctx) throws Exception {
		Log.info("AuthenticationFilter inactive");
		
		var user = ctx.channel().attr(User.attrkey).get();
		if(user != null)
			userChannels.remove(user._id);

		// send out an event 
		var event = new UserInactiveEvent();
		event.ctx = ctx;
		event.user = user;
		bus.post(event);
		
		super.channelInactive(ctx);
	}
	
	
	/** get and send a salt */
	public void getSalt(ChannelHandlerContext ctx, GetSalt msg) {
		Log.info("AuthenticationFilter getSalt for user: " + msg.username);
		// trouve le salt associé au username et renvoi le
		var user = Emerald.users().find(eq(User.name_username, msg.username)).first();
		if(user != null) 
			ctx.writeAndFlush(new SendSalt(user.salt));
		else 
			ctx.writeAndFlush(new SendSalt(null)); //SendSaltNull()); // username doesnt exist
	}
	
	/** get and send a user */
	public void getUser(ChannelHandlerContext ctx, GetUser msg) {
		// trouve un user avec le username + hashedpassword et renvoi le 
		// + set l'attribute dans le channel 
		// + ajoute le channel aux users connectés
		var user = Emerald.users().find(and(eq(User.name_username, msg.username), eq(User.name_password, msg.hashedPassword))).first();
		Log.info("AuthenticationFilter getUser ("+msg.username+", " +msg.hashedPassword+ ") " + user + " level " + (user == null ? "null" : user.level));
		if(user != null && user.level.ordinal() >= minLevel.ordinal()) {
			// set the user attribute on the channel
			ctx.channel().attr(User.attrkey).set(user);
			// add the channel to the list
			userChannels.put(user._id, ctx.channel());
			// send out an event 
			var event = new UserActiveEvent();
			event.ctx = ctx;
			event.user = user;
			bus.post(event);
			// send the user object to the client
			ctx.writeAndFlush(new SendUser(user));
		} else {
			ctx.writeAndFlush(new SendUser(null)); // username + password miscombination not unauthorized user level
			ctx.channel().close(); 
		}
	}

	/**
	 * Event for when a new user is authenticated
	 * @author Blank
	 * @date 25 juin 2019
	 */
	public static final class UserActiveEvent {
		public ChannelHandlerContext ctx;
		public User user;
	}
	/**
	 * Event for when a user disconnected
	 * @author Blank
	 * @date 25 juin 2019
	 */
	public static final class UserInactiveEvent {
		public ChannelHandlerContext ctx;
		public User user;
	}
	

}
