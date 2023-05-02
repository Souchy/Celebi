package sylveon;

import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.security.interfaces.ECKey;
import java.security.interfaces.ECPrivateKey;
import java.security.interfaces.ECPublicKey;
import java.security.interfaces.RSAKey;
import java.security.interfaces.RSAPrivateKey;
import java.security.interfaces.RSAPublicKey;
import java.util.logging.Level;
import java.util.logging.Logger;

import org.eclipse.jetty.server.Server;
import org.eclipse.jetty.servlet.ServletContextHandler;
import org.eclipse.jetty.servlet.ServletHolder;
import org.glassfish.jersey.server.ResourceConfig;
import org.glassfish.jersey.servlet.ServletContainer;

import com.google.api.client.util.store.AbstractDataStoreFactory;
import com.google.api.client.util.store.FileDataStoreFactory;
import com.google.gson.Gson;
import com.google.gson.JsonObject;
import com.souchy.randd.commons.tealwaters.io.files.JsonConfig;
import com.souchy.randd.commons.tealwaters.logging.Log;

import espeon.emerald.Emerald;
import espeon.util.PemUtils;
import jakarta.ws.rs.client.Client;
import jakarta.ws.rs.client.ClientBuilder;

public class Sylveon {

	public static final SylveonConfig conf;

	public static final String cliend_id;
	public static final String cliend_secret;
	public static final String redirect_uri_client;
	public static final String redirect_uri_authserver;
	

	public static final String REST_URI = "http://oauth2.googleapis.com/token";
	public static final String tokenUrl = "https://accounts.google.com/o/oauth2/token";
	public static final Client client = ClientBuilder.newClient();
	public static final AbstractDataStoreFactory dataStore;


	// espeon109
	static {
		try {
			// sylveon conf
			conf = JsonConfig.read(SylveonConfig.class);
			Log.info("Start Sylveon conf : " + conf);
			// data store
			dataStore = new FileDataStoreFactory(new File("./users.data"));
			// google api conf
			var json = Files.readString(Paths.get("./googleapi.json"));
			var gapijson = new Gson().fromJson(json, JsonObject.class);
			var web = gapijson.getAsJsonObject("web");
			cliend_id = web.get("client_id").getAsString();
			cliend_secret = web.get("client_secret").getAsString();
			redirect_uri_client = web.get("redirect_uris").getAsJsonArray().get(0).getAsString();
			redirect_uri_authserver = web.get("redirect_uris").getAsJsonArray().get(2).getAsString();


			// PemUtils.generateKeyPair("id_ecdsa", "EC", 256);
			// Log.info("Sylveon pub: " + PemUtils.readPub("./id_ecdsa.pub", "EC"));
			// Log.info("Sylveon priv: " + PemUtils.readPriv("./id_ecdsa", "EC"));
			// PemUtils.generateKeyPair("id_rsa", "RSA", 2048); // 3072);
			// Log.info("Sylveon pub: " + PemUtils.readPub("./id_rsa.pub", "RSA"));
			// Log.info("Sylveon priv: " + PemUtils.readPriv("./id_rsa", "RSA"));

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
		if (executing)
			Emerald.init(conf.mongo);

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
		// rc.register(CORSFilter.class);

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
