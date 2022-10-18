package sylveon.auth;

import java.util.Base64;

import com.auth0.jwt.JWT;
import com.auth0.jwt.interfaces.DecodedJWT;
import com.google.gson.Gson;
import com.google.gson.JsonObject;
import com.mongodb.client.model.Filters;
import com.souchy.randd.commons.tealwaters.logging.Log;

import espeon.auth.jade.User;
import espeon.auth.jade.UserLevel;
import espeon.auth.jade.UserType;
import espeon.emerald.Emerald;
import jakarta.annotation.security.RolesAllowed;
import jakarta.servlet.http.Cookie;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;
import jakarta.ws.rs.GET;
import jakarta.ws.rs.POST;
import jakarta.ws.rs.Path;
import jakarta.ws.rs.QueryParam;
import jakarta.ws.rs.client.Entity;
import jakarta.ws.rs.core.Context;
import jakarta.ws.rs.core.Form;
import jakarta.ws.rs.core.MediaType;
import jakarta.ws.rs.core.Response;
import sylveon.Sylveon;

@Path("auth")
public class Auth {

	@POST
	@RolesAllowed("anonymous") // @PermitAll
	public void authenticate(@QueryParam("u") String usernameOrEmail, @QueryParam("p") String pass,
			@Context HttpServletRequest req, @Context HttpServletResponse res) {
		var str = String.format("Hi auth %s, %s\n", usernameOrEmail, pass);
		System.out.printf(str);

		var user = Emerald.users().find(Filters.or(Filters.eq(User.name_username, usernameOrEmail),
				Filters.eq(User.name_email, usernameOrEmail))).first();
		if (user == null || !user.password.equals(pass)) {
			res.setStatus(Response.Status.FORBIDDEN.getStatusCode());
		} else {
			// good!
			// generate a jwt access token to return

		}
	}

	@GET
	@Path("/google")
	@RolesAllowed("anonymous") // @PermitAll
	public void authGoogle(@Context HttpServletRequest req, @Context HttpServletResponse res) throws Exception {
		var code = req.getParameter("code");
		// System.out.println("Auth Google: " + code);

		// code = String.valueOf(Base64.getDecoder().decode(code));
		var gres = requestGoogleTokens(code);
		var json = gres.readEntity(String.class);
		System.out.println("Auth Google get token: " + json);
		var gson = new Gson().fromJson(json, JsonObject.class);
		var id_token = gson.get("id_token").getAsString();
		var access_token = gson.get("access_token").getAsString();
		var refresh_token = gson.get("refresh_token").getAsString();

		// decode to user
		var user = decodeGoogleToken(id_token, access_token);
		// save user
		insertUser(user);
		// return access token
		res.addCookie(new Cookie("access_token", access_token)); 
		res.addCookie(new Cookie("refresh_token", refresh_token)); 
		res.sendRedirect(Sylveon.redirect_uri_client); // redirect to client
	}


	/**
	 * Request tokens from google by using the google code they give us, then return the response
	 */
	private Response requestGoogleTokens(String code) {
		Form form = new Form();
		form.param("code", code);
		form.param("client_id", Sylveon.cliend_id);
		form.param("client_secret", Sylveon.cliend_secret);
		form.param("redirect_uri", Sylveon.redirect_uri_authserver);
		form.param("grant_type", "authorization_code");

		var res = Sylveon.client.target(Sylveon.tokenUrl)
				.request(MediaType.APPLICATION_FORM_URLENCODED)
				.accept(MediaType.APPLICATION_JSON).post(Entity.form(form));
		return res;
	}

	/**
	 * 
	 */
	private User decodeGoogleToken(String id_token, String access_token) throws Exception {
		// var pub = PemUtils.readPub("./id_rsa.pub", "RSA");
		// var priv = PemUtils.readPriv("./id_rsa", "RSA");
		// Algorithm algo = Algorithm.RSA256((RSAPublicKey) pub, (RSAPrivateKey) priv);
		// JWTVerifier verifier = JWT.require(algo).withIssuer("auth0").build(); // Reusable verifier instance
		// DecodedJWT jwt = verifier.verify(id_token);

		DecodedJWT jwt = JWT.decode(id_token);
		var payload = jwt.getPayload();
		var content = new String(Base64.getDecoder().decode(payload));
		Log.info("Token content: %s", content);

		return parseUser(content);
	}

	/*
	private User decodeCustomToken(String id_token, String access_token) throws Exception {
		var pub = PemUtils.readPub("./id_ecdsa.pub", "EC");
		var priv = PemUtils.readPriv("./id_ecdsa", "EC");
		Algorithm algo = Algorithm.ECDSA256((ECPublicKey) pub, (ECPrivateKey) priv);
		JWTVerifier verifier = JWT.require(algo).withIssuer("auth0").build(); // Reusable verifier instance
		DecodedJWT jwt = verifier.verify(id_token);

		// DecodedJWT jwt = JWT.decode(id_token);
		var payload = jwt.getPayload();
		var content = new String(Base64.getDecoder().decode(payload));
		Log.info("Token content: %s", content);

		return parseUser(content);
	}
	*/

	private User parseUser(String payload) {
		var gson = new Gson().fromJson(payload, JsonObject.class);
		var email = gson.get("email").getAsString();
		var name = gson.has("name") ? gson.get("name").getAsString() : null;
		var verifiedEmail = gson.get("email_verified").getAsBoolean();

		var user = new User();
		user.authLevel = UserLevel.normal;
		user.userType = UserType.externalService;
		user.email = email;
		user.verifiedEmail = verifiedEmail;
		user.pseudo = name;
		return user;
	}

	/**
	 * Save user
	 */
	private void insertUser(User user) {
		// Emerald.users().updateOne(Filters.eq("email", user.email), new
		// BsonDocument());
		var first = Emerald.users().find(Filters.eq("email", user.email)).first();
		if (first == null) {
			Emerald.users().insertOne(user);
		}
		// Emerald.users().findOneAndUpdate(
		// Filters.eq("email", user.email),
		// Updates.set("email", user.email),
		// new FindOneAndUpdateOptions().upsert(true)
		// );
	}

}
