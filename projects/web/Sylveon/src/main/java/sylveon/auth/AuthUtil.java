package sylveon.auth;

import java.io.IOException;
import java.security.NoSuchAlgorithmException;
import java.security.SecureRandom;
import java.security.interfaces.ECPrivateKey;
import java.security.interfaces.ECPublicKey;
import java.security.interfaces.RSAPrivateKey;
import java.security.interfaces.RSAPublicKey;
import java.security.spec.InvalidKeySpecException;
import java.security.spec.KeySpec;
import java.util.Base64;

import javax.crypto.SecretKeyFactory;
import javax.crypto.spec.PBEKeySpec;

import com.auth0.jwt.JWT;
import com.auth0.jwt.algorithms.Algorithm;
import com.auth0.jwt.exceptions.JWTCreationException;
import com.auth0.jwt.exceptions.JWTVerificationException;
import com.auth0.jwt.interfaces.DecodedJWT;
import com.auth0.jwt.interfaces.JWTVerifier;
import com.google.common.io.CharStreams;
import com.google.gson.Gson;
import com.google.gson.JsonObject;
import com.mongodb.client.model.Filters;
import com.mongodb.client.model.Updates;
import com.souchy.randd.commons.tealwaters.logging.Log;

import espeon.auth.jade.User;
import espeon.auth.jade.UserLevel;
import espeon.auth.jade.UserType;
import espeon.auth.jade.UserValidator;
import espeon.emerald.Emerald;
import espeon.util.PemUtils;
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

class AuthUtil {

    private static final RSAPublicKey keyPublic;
    private static final RSAPrivateKey keyPrivate;
    private static final Algorithm algorithm;
    private static final JWTVerifier verifier;
    static {
        try {
            keyPublic = (RSAPublicKey) PemUtils.readPub("./id_rsa.pub", "RSA");
            keyPrivate = (RSAPrivateKey) PemUtils.readPriv("./id_rsa", "RSA");
            algorithm = Algorithm.RSA256(keyPublic, keyPrivate);
            verifier = JWT.require(algorithm).withIssuer("auth0").build();
        } catch (Exception e) {
            throw new RuntimeException(e);
        }
    }

    static String hashPassword(String salt, String password) throws NoSuchAlgorithmException, InvalidKeySpecException {
        return hashPassword(Base64.getDecoder().decode(salt), password);
    }

    static String hashPassword(byte[] salt, String password) throws NoSuchAlgorithmException, InvalidKeySpecException {
        KeySpec spec = new PBEKeySpec(password.toCharArray(), salt, 65536, 128);
        SecretKeyFactory factory = SecretKeyFactory.getInstance("PBKDF2WithHmacSHA1");
        byte[] hash = factory.generateSecret(spec).getEncoded();
        return Base64.getEncoder().encodeToString(hash);
    }

    static String createToken() {
        return JWT.create().withIssuer("auth0").sign(algorithm); // withPaylod ?
    }

    static DecodedJWT verifyToken(String token) {
        DecodedJWT jwt = verifier.verify(token);
        return jwt;
    }

    static boolean checkUnique(HttpServletResponse res, String pseudo, String username, String email) throws IOException {
        var userByPseudo = Emerald.users().find(Filters.eq(User.name_pseudo, pseudo)).first();
        if (userByPseudo != null) {
            res.sendError(Response.Status.CONFLICT.getStatusCode(), "pseudo");
            return false;
        }
        var userByName = Emerald.users().find(Filters.eq(User.name_username, username)).first();
        if (userByName != null) {
            res.sendError(Response.Status.CONFLICT.getStatusCode(), "username");
            return false;
        }
        var userByEmail = Emerald.users().find(Filters.eq(User.name_email, email)).first();
        if (userByEmail != null) {
            res.sendError(Response.Status.CONFLICT.getStatusCode(), "email");
            return false;
        }
        return true;
    }

    /**
     * Request tokens from google by using the google code they give us, then return the response
     */
    static Response requestGoogleTokens(String code) {
        Form form = new Form();
        form.param("code", code);
        form.param("client_id", Sylveon.cliend_id);
        form.param("client_secret", Sylveon.cliend_secret);
        form.param("redirect_uri", Sylveon.redirect_uri_authserver);
        form.param("grant_type", "authorization_code");

        var res = Sylveon.client.target(Sylveon.tokenUrl)
            .request(MediaType.APPLICATION_FORM_URLENCODED)
            .accept(MediaType.APPLICATION_JSON)
            .post(Entity.form(form));
        return res;
    }

    /**
     * 
     */
    static User decodeGoogleToken(String id_token, String access_token) throws Exception {
//        var jwt = verifyToken(id_token);
        DecodedJWT jwt = JWT.decode(id_token);
        var payload = jwt.getPayload();
        var content = new String(Base64.getDecoder().decode(payload));
        Log.info("Token content: %s", content);

        return parseGoogleUser(content);
    }

    private User decodeCustomToken(String id_token, String access_token) throws Exception {
//      var jwt = verifyToken(id_token);
        DecodedJWT jwt = JWT.decode(id_token);
        var payload = jwt.getPayload();
        var content = new String(Base64.getDecoder().decode(payload));
        Log.info("Token content: %s", content);
        return parseCustomUser(content);
    }

    static User parseCustomUser(String payload) {
        var gson = new Gson().fromJson(payload, JsonObject.class);
        var email = gson.get("email").getAsString();
        var name = gson.has("name") ? gson.get("name").getAsString() : null;
        var verifiedEmail = gson.get("email_verified").getAsBoolean();
        
        var user = new User();
        user.authLevel = UserLevel.normal;
        user.userType = UserType.custom;
        user.pseudo = name;
        user.email = email;
        user.verifiedEmail = verifiedEmail;
        return user;
    }

    /**
     * 
     * @param payload from google
     * @return new or returning user
     */
    static User parseGoogleUser(String payload) {
        var gson = new Gson().fromJson(payload, JsonObject.class);
        var email = gson.get("email").getAsString();
        var name = gson.has("name") ? gson.get("name").getAsString() : null;
        var verifiedEmail = gson.get("email_verified").getAsBoolean();

        var user = new User();
        user.authLevel = UserLevel.normal;
        user.userType = UserType.externalService;
        user.pseudo = name;
        user.email = email;
        user.verifiedEmail = verifiedEmail;
        return user;
    }

    /**
     * Save user
     */
    static User insertUser(User user) {
        // Emerald.users().updateOne(Filters.eq("email", user.email), new
        // BsonDocument());
        var first = Emerald.users().find(Filters.eq("email", user.email)).first();
        if (first == null) {
            Emerald.users().insertOne(user);
            return user;
        } else {
            return first;
        }
        // Emerald.users().findOneAndUpdate(
        // Filters.eq("email", user.email),
        // Updates.set("email", user.email),
        // new FindOneAndUpdateOptions().upsert(true)
        // );
    }

}
