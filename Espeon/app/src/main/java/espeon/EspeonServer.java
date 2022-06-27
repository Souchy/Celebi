package espeon;

import java.util.ArrayList;
import java.util.List;

import com.google.common.eventbus.EventBus;
import com.souchy.randd.commons.net.netty.bytebuf.BBMessage;
import com.souchy.randd.commons.net.netty.bytebuf.BBMessageHandler;
import com.souchy.randd.commons.net.netty.bytebuf.multihandlers.BBMessageFactories;
import com.souchy.randd.commons.net.netty.bytebuf.multihandlers.BBMessageHandlers;
import com.souchy.randd.commons.net.netty.bytebuf.reflect.BBMessageDiscoverer;
import com.souchy.randd.commons.net.netty.bytebuf.reflect.BBMessageHandlerDiscoverer;
import com.souchy.randd.commons.net.netty.server.NettyServer;
import com.souchy.randd.commons.tealwaters.commons.ClassInstanciator;
import com.souchy.randd.commons.tealwaters.commons.Factory;
import com.souchy.randd.commons.tealwaters.logging.Log;
import com.souchy.randd.commons.tealwaters.logging.Logging;

import espeon.game.handlers.AuthenticationFilter;

import org.bson.types.ObjectId;
import java.util.Map;

import com.google.common.eventbus.EventBus;
import com.souchy.randd.commons.net.netty.bytebuf.pipehandlers.BBMessageDecoder;
import com.souchy.randd.commons.net.netty.bytebuf.pipehandlers.BBMessageEncoder;
import com.souchy.randd.commons.net.netty.server.NettyHandler;
import com.souchy.randd.commons.net.netty.server.NettyServer;

import io.netty.channel.Channel;
import io.netty.channel.ChannelPipeline;
import io.netty.handler.codec.ByteToMessageDecoder;

public class EspeonServer extends NettyServer {
    
	public AuthenticationFilter auth = new AuthenticationFilter();
	public final Map<ObjectId, Channel> users = auth.userChannels;
	
	/**
	 * Create & Start the server. Should call .block() after to wait for server closure before exiting the application <br>
	 * Example : <br>
	 * <code>
	 * var server = new DeathShadowTCP(port, this); <br>
	 * server.block();
	 * </code>
	 *
	 */
	// public DeathShadowTCP(int port, DeathShadowCore core) throws Exception {
	// 	super(port, false, new BBMessageEncoder(), () -> new BBMessageDecoder(core.getMessages()), new NettyHandler(core.getHandlers()));
	// }


	/**
	 * Core events like packets are thrown here. <br>
	 * Classes can listen to them without being a full blown BBMessageHandler class <br>
	 * Useful for UI updates for example
	 */
	public static final EventBus bus = new EventBus();
	/**
	 * Message factories used by both netty and rabbitmq communication
	 */
	protected static BBMessageFactories msgFactories;
	/**
	 * Message handlers contains both netty handlers and rabbitmq handlers
	 */
	protected static BBMessageHandlers msgHandlers = new BBMessageHandlers(bus);
    
	private static Factory<ByteToMessageDecoder> decoderFactory =  () -> new BBMessageDecoder(msgFactories);

	static {
		discoverMesssages();
	}

	/**
	 * Use this constructor to initialize your microservices (servers and clients)
	 */
	public EspeonServer(int port, boolean ssl) throws Exception {
		super(port, ssl, new BBMessageEncoder(), decoderFactory, new NettyHandler(msgHandlers));
		// load smooth rivers conf (has ip, port, and enable bool)
		// init smooth rivers on localhost:port from conf if smoothrivers is enabled in the conf
		// init microservices with their messages and handlers
		// start application
	}
	
	@Override
	public void initPipeline(ChannelPipeline pipe) {
		pipe.addLast(connectionHandler);
		
		pipe.addLast(lengthEncoder); 
		pipe.addLast(encoder); 

		pipe.addLast(lengthDecoder.create()); 
		pipe.addLast(decoder.create()); 
		pipe.addLast(auth); // auth avant le packet handler
		pipe.addLast(handler); 
	}

	/**
	 * Initialize messages. This includes Smooth River messages for inter-server communication and communication with Pearl manager
	 */
	private static void discoverMesssages() {
		// Init msg factories and handlers
		msgFactories = new BBMessageFactories();
		msgHandlers = new BBMessageHandlers(bus);
		
		// Search for Message and MessageHandler classes
		List<Class<BBMessage>> msgClasses = new ArrayList<>();
		List<Class<BBMessageHandler<BBMessage>>> handlerClasses = new ArrayList<>();
		for(String path : getRootPackages()) {
			msgClasses.addAll(new BBMessageDiscoverer().explore(path));
			handlerClasses.addAll(new BBMessageHandlerDiscoverer().explore(path)); 
		}
		// Instantiate all Messages and MessageHandlers and add them to their respective manager
		msgFactories.populate(ClassInstanciator.list(msgClasses)); 
		msgHandlers.populate(ClassInstanciator.list(handlerClasses));
		// Log check what handler handles what message
		Log.verbose("--");
		msgFactories.foreach(e -> Log.verbose("Microservice init message ["+e.getKey()+"] ["+e.getValue().getClass().getSimpleName()+"]"));
		Log.verbose("--");
		msgHandlers.foreach(h -> Log.verbose("Microservice init handler ["+h.getID()+"] ["+h.getClass().getSimpleName()+"] handles ["+h.getMessageClass().getSimpleName()+"]"));
		Log.verbose("--");
	}
	
	/**
	 * Get packages paths for messages and handlers classes
	 */
	private static String[] getRootPackages() {
        return new String[] { 
			"espeon.auth.messages", 
			"espeon.auth.handlers",
			"espeon.game.messages", 
			"espeon.game.handlers" 
		};
    }
	
}
