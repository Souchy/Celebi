package sylveon;

import java.io.IOException;
import java.net.URI;
import java.nio.file.Files;
import java.nio.file.Paths;

import org.glassfish.jersey.jetty.JettyHttpContainerFactory;
import org.glassfish.jersey.netty.httpserver.NettyHttpContainerProvider;
import org.glassfish.jersey.server.ResourceConfig;
import org.glassfish.jersey.servlet.ServletContainer;

import com.google.gson.Gson;
import com.google.gson.JsonObject;
import com.souchy.randd.commons.tealwaters.io.files.JsonConfig;
import com.souchy.randd.commons.tealwaters.logging.Log;

import io.netty.channel.Channel;
import jakarta.servlet.http.HttpServlet;

import org.eclipse.jetty.server.Server;
import org.eclipse.jetty.servlet.DefaultServlet;
import org.eclipse.jetty.servlet.ServletContextHandler;
import org.eclipse.jetty.servlet.ServletHolder;
import org.eclipse.jetty.webapp.WebAppContext;

import java.util.HashMap;
import java.util.logging.Level;
import java.util.logging.Logger;

public class Sylveon {

	public static SylveonConfig conf;
	public static final String cliend_id;
	public static final String cliend_secret;

	static {
		try {
			// var path = Paths.get("./sylveon.conf");
			// if (!Files.exists(path)) {
			// 	conf = new SylveonConfig();
			// 	var json = new Gson().toJson(conf);
			// 	Files.writeString(path, json);
			// }
			// var json = Files.readString(path);
			// conf = new Gson().fromJson(json, SylveonConfig.class);

			conf = JsonConfig.read(SylveonConfig.class);
			Log.info("Start Sylveon conf : " + conf);

			var json = Files.readString(Paths.get("googleapi.json"));
			var gapijson = new Gson().fromJson(json, JsonObject.class);
			var web = gapijson.getAsJsonObject("web");
			cliend_id = web.get("cliend_id").getAsString();
			cliend_secret = web.get("cliend_secret").getAsString();
		} catch (Exception e) {
			throw new RuntimeException(e);
		}
	}

	public static void main(String[] args) throws Exception {
		new Sylveon();
	}

	private Sylveon() throws Exception {
		init();
		startJetty();
	}

	private void init() throws Exception {
		boolean executing = true;

		// execute api site
		// if(executing)
		// Emerald.db();

		// rc = new ResourceConfig().packages(getRootPackages());
		// Log.info("RC Classes: " + rc.getClasses());

		// or convert/upload data to mongo
		if (executing)
			return;

		System.exit(0);
	}

	private void startJetty() {
		String port = System.getenv("PORT");
		if (port == null || port.isBlank()) {
			port = Sylveon.conf.port + "";
		}

		Server server = new Server(Integer.valueOf(port));
		ServletContextHandler handler = new ServletContextHandler(server, "/", ServletContextHandler.SECURITY);

		var rc = new ResourceConfig();
		rc.register(CORSFilter.class);
		rc.packages(new String[] { "sylveon.api", "sylveon.auth" });
		rc.register(CORSFilter.class);

		ServletHolder jerseyServlet = new ServletHolder(new ServletContainer(rc));
		handler.addServlet(jerseyServlet, "/*");

		try {
			Log.info("Sylveon start: " + port);
			server.start();
			server.join();
		} catch (Exception ex) {
			Logger.getLogger(Sylveon.class.getName()).log(Level.SEVERE, null, ex);
		} finally {
			server.destroy();
		}
	}

}
