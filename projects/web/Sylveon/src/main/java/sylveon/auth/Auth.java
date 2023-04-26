package sylveon.auth;

import static sylveon.auth.AuthUtil.checkUnique;
import static sylveon.auth.AuthUtil.decodeGoogleToken;
import static sylveon.auth.AuthUtil.hashPassword;
import static sylveon.auth.AuthUtil.insertUser;
import static sylveon.auth.AuthUtil.requestGoogleTokens;

import java.security.NoSuchAlgorithmException;
import java.security.SecureRandom;
import java.security.spec.InvalidKeySpecException;
import java.util.Base64;

import org.checkerframework.checker.units.qual.Acceleration;

import com.google.common.io.CharStreams;
import com.google.gson.Gson;
import com.google.gson.JsonObject;
import com.mongodb.client.model.Filters;
import com.mongodb.client.model.Updates;

import espeon.auth.jade.User;
import espeon.auth.jade.UserValidator;
import espeon.emerald.Emerald;
import jakarta.annotation.security.RolesAllowed;
import jakarta.servlet.http.Cookie;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;
import jakarta.ws.rs.GET;
import jakarta.ws.rs.POST;
import jakarta.ws.rs.Path;
import jakarta.ws.rs.PathParam;
import jakarta.ws.rs.Consumes;
import jakarta.ws.rs.core.Context;
import jakarta.ws.rs.core.MediaType;
import jakarta.ws.rs.core.Response;
import sylveon.Sylveon;

@Path("auth")
public class Auth {

	/**
	 * 
	 */
	@POST
	@Path("/signup")
	@RolesAllowed("anonymous")
	public void signUp(@Context HttpServletRequest req, @Context HttpServletResponse res) throws Exception {
		var body = CharStreams.toString(req.getReader());
		body = new String(Base64.getDecoder().decode(body));
		var gson = new Gson().fromJson(body, JsonObject.class);

		String pseudo = gson.get("email").getAsString();
		String username = gson.get("username").getAsString();
		String email = gson.get("email").getAsString();
		String password = gson.get("password").getAsString();

		if (!checkUnique(res, pseudo, username, email)) {
			return;
		}
		if (!UserValidator.validate(pseudo, username, password, email)) {
			res.setStatus(Response.Status.FORBIDDEN.getStatusCode());
			return;
		}

		SecureRandom random = new SecureRandom();
		byte[] salt = new byte[16];
		random.nextBytes(salt);

		var user = new User();
		user.pseudo = pseudo;
		user.username = username;
		user.email = email;
		user.salt = Base64.getEncoder().encodeToString(salt);
		user.password = hashPassword(salt, password);

		// save user
		insertUser(user);
		// TODO return access token
		// res.addCookie(new Cookie("access_token", access_token));
		// res.addCookie(new Cookie("refresh_token", refresh_token));
		// res.addHeader("access_token", access_token);
		// res.addHeader("refresh_token", refresh_token);
		res.setStatus(Response.Status.ACCEPTED.getStatusCode());
		res.sendRedirect(Sylveon.redirect_uri_client); // redirect to client

	}

	/**
	 * 
	 */
	@POST
	@Path("/signin")
	@RolesAllowed("anonymous")
	public void signin(@Context HttpServletRequest req, @Context HttpServletResponse res) throws Exception {
		var body = CharStreams.toString(req.getReader());
		body = new String(Base64.getDecoder().decode(body));
		var gson = new Gson().fromJson(body, JsonObject.class);

		String usernameOrEmail = gson.get("usernameOrEmail").getAsString();
		String password = gson.get("password").getAsString();
		// Log.info("Hi auth %s, %s\n", usernameOrEmail, pass);

		var filter = Filters.or(Filters.eq(User.name_username, usernameOrEmail), Filters.eq(User.name_email, usernameOrEmail));
		var user = Emerald.users().find(filter).first();

		if (user != null) {
			var hash = hashPassword(user.salt, password); // hashPassword(Base64.getDecoder().decode(user.salt), pass);
			if (user.password.contentEquals(hash)) {
				res.setStatus(Response.Status.FORBIDDEN.getStatusCode());
				// good!
				// generate a jwt access token to return
				// TODO return access token
				res.setStatus(Response.Status.ACCEPTED.getStatusCode());
				return;
			}
		}
		res.setStatus(Response.Status.FORBIDDEN.getStatusCode());
	}

	/**
	 * 
	 */
	@POST
	@Path("/update/{oldEmail}")
	@RolesAllowed("normal")
	@Consumes(MediaType.APPLICATION_JSON)
	public void updateAccount(@PathParam("oldEmail") String oldEmail, User user) throws NoSuchAlgorithmException, InvalidKeySpecException {
		// var filter = Filters.or(Filters.eq(User.name_username, usernameOrEmail), Filters.eq(User.name_email, usernameOrEmail));
		var filter = Filters.eq(User.name_email, oldEmail);
		var oldUser = Emerald.users().find(filter).first();
		if (oldUser != null) {
			var updates = Updates.combine(Updates.set(User.name_username, user.username), Updates.set(User.name_email, user.email), Updates.set(User.name_pseudo, user.pseudo),
					Updates.set(User.name_password, hashPassword(oldUser.salt, user.password)));
			// if the email changed, unverify it
			if (!oldEmail.contentEquals(user.email)) {
				Updates.combine(updates, Updates.set(User.name_verifiedEmail, false));
			}
			Emerald.users().updateOne(filter, updates);
		}
	}

	/**
	 * 
	 */
	@GET
	@Path("/google")
	@RolesAllowed("anonymous")
	public HttpServletResponse authGoogle(@Context HttpServletRequest req, @Context HttpServletResponse res) throws Exception {
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
		res.addHeader("access_token", access_token);
		res.addHeader("refresh_token", refresh_token);
		res.sendRedirect(Sylveon.redirect_uri_client); // redirect to client
		return res;
	}

}
