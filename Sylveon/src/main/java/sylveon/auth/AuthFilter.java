package sylveon.auth;

import java.io.IOException;
import java.lang.reflect.Method;
import java.util.Arrays;
import java.util.Base64;
import java.util.HashSet;
import java.util.List;
import java.util.Set;
import java.util.StringTokenizer;

import org.glassfish.jersey.server.wadl.processor.OptionsMethodProcessor;

import com.souchy.randd.commons.tealwaters.logging.Log;

import espeon.auth.jade.UserLevel;
import jakarta.annotation.security.DenyAll;
import jakarta.annotation.security.PermitAll;
import jakarta.annotation.security.RolesAllowed;
import jakarta.ws.rs.container.ContainerRequestContext;
import jakarta.ws.rs.container.ContainerRequestFilter;
import jakarta.ws.rs.container.ResourceInfo;
import jakarta.ws.rs.core.Context;
import jakarta.ws.rs.core.Cookie;
import jakarta.ws.rs.core.MultivaluedMap;
import jakarta.ws.rs.core.Response;
import jakarta.ws.rs.ext.Provider;

@Provider
public class AuthFilter implements ContainerRequestFilter {

    private static final String optionsClassName = "org.glassfish.jersey.server.wadl.processor.OptionsMethodProcessor$PlainTextOptionsInflector";
    private static final String AUTHORIZATION_PROPERTY = "Authorization";
    private static final String AUTHENTICATION_SCHEME = "Basic";

    @Context
    private ResourceInfo resourceInfo;


    @Override
    public void filter(ContainerRequestContext ctx) throws IOException {
        Method method = resourceInfo.getResourceMethod();
        Log.info("AuthFilter on method: " + resourceInfo.getResourceClass().getName() + "." + method.getName());
        if (resourceInfo.getResourceClass().getName().contentEquals(optionsClassName)) {
            Log.info("ignore options");
            return;
        }

        RolesAllowed rolesAnnotation = null;
        Set<String> rolesSet = null;
        if (method.isAnnotationPresent(RolesAllowed.class)) {
            rolesAnnotation = method.getAnnotation(RolesAllowed.class);
            rolesSet = new HashSet<String>(Arrays.asList(rolesAnnotation.value()));
        }
        Log.info("cookie access: " + ctx.getCookies().get("access_token"));
        Log.info("cookie refresh: " + ctx.getCookies().get("refresh_token"));

        // Allow all if PermitAll or no roles specified
        if (method.isAnnotationPresent(PermitAll.class) || rolesAnnotation == null || rolesSet == null) {
            // TODO: accept // reject(ctx);
            return;
        }

        Cookie access_token = null; //ctx.getCookies().get("access_token");
        Cookie refresh_token = null; //ctx.getCookies().get("refresh_token");

        // if only anonymous (ex: authentication) but the client already has an access token
        // if(rolesSet.contains(UserLevel.anonymous.name()) && (access_token == null || refresh_token == null)) {
        //     // reject(ctx);
        //     return;
        // }

        if (access_token == null || refresh_token == null) {
            if(!rolesSet.contains(UserLevel.anonymous.name())) reject(ctx);
            return;
        }

        // Get User from access token?

        for (var roleStr : rolesSet) {
            var role = UserLevel.valueOf(roleStr);
            // if(user.level.id() >= role.id())
            // accept
        }
        // reject(ctx); 
        return;
    }
    
    private void reject(ContainerRequestContext ctx) {
        ctx.abortWith(Response.status(Response.Status.UNAUTHORIZED)
        .entity("You cannot access this resource").build());
    }

    // https://accounts.google.com/o/oauth2/v2/auth
    /*
    @Override
    public void filter(ContainerRequestContext requestContext) throws IOException {
        Method method = resourceInfo.getResourceMethod();

        if (resourceInfo.getResourceClass().getName().contentEquals(
                "org.glassfish.jersey.server.wadl.processor.OptionsMethodProcessor$PlainTextOptionsInflector")) {
            Log.info("ignore options");
            return;
        }

        Log.info("AuthFilter on method: " + resourceInfo.getResourceClass().getName() + "." + method.getName());

        // Access allowed for all
        if (!method.isAnnotationPresent(PermitAll.class)) {
            // Access denied for all
            if (method.isAnnotationPresent(DenyAll.class)) {
                requestContext.abortWith(Response.status(Response.Status.FORBIDDEN)
                        .entity("Access blocked for all users !!").build());
                return;
            }

            // Get request headers
            final MultivaluedMap<String, String> headers = requestContext.getHeaders();

            // Fetch authorization header
            final List<String> authorization = headers.get(AUTHORIZATION_PROPERTY);

            // If no authorization information present; block access
            if (authorization == null || authorization.isEmpty()) {
                requestContext.abortWith(Response.status(Response.Status.UNAUTHORIZED)
                        .entity("You cannot access this resource").build());
                return;
            }

            // Get encoded username and password
            final String encodedUserPassword = authorization.get(0).replaceFirst(AUTHENTICATION_SCHEME + " ", "");

            // Decode username and password
            String usernameAndPassword = new String(Base64.getDecoder().decode(encodedUserPassword.getBytes()));

            // Split username and password tokens
            final StringTokenizer tokenizer = new StringTokenizer(usernameAndPassword, ":");
            final String username = tokenizer.nextToken();
            final String password = tokenizer.nextToken();

            // Verifying Username and password
            Log.info("AuthFilter: " + username + ", " + password);
            // Log.info(username);
            // Log.info(password);

            // Verify user access
            if (method.isAnnotationPresent(RolesAllowed.class)) {
                RolesAllowed rolesAnnotation = method.getAnnotation(RolesAllowed.class);
                Set<String> rolesSet = new HashSet<String>(Arrays.asList(rolesAnnotation.value()));

                // Is user valid?
                if (!isUserAllowed(username, password, rolesSet)) {
                    requestContext.abortWith(Response.status(Response.Status.UNAUTHORIZED)
                            .entity("You cannot access this resource").build());
                    return;
                }
            }
        }
    }
    */

    private boolean isUserAllowed(final String username, final String password, final Set<String> rolesSet) {
        boolean isAllowed = false;

        // Step 1. Fetch password from database and match with password in argument
        // If both match then get the defined role for user from database and continue;
        // else return isAllowed [false]
        // Access the database and do this part yourself
        // String userRole = userMgr.getUserRole(username);

        if (username.equals("souchy") && password.equals("z")) {
            String userRole = "ADMIN";
            // Step 2. Verify user role
            if (rolesSet.contains(userRole)) {
                isAllowed = true;
            }
        }
        return isAllowed;
    }

}
