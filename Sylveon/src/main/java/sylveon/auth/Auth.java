package sylveon.auth;

import jakarta.ws.rs.POST;
import jakarta.ws.rs.Path;
import jakarta.ws.rs.QueryParam;

@Path("auth")
public class Auth {
    
    @POST
    public String authenticate(@QueryParam("u") String user, @QueryParam("p") String pass) {
        var str = String.format("Hi auth %s, %s\n", user, pass);
        System.out.printf(str);
        return str;
    }

}
