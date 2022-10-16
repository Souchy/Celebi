package sylveon.api;

import java.net.URI;
import java.net.URISyntaxException;
import java.util.Optional;

import jakarta.servlet.http.HttpServletRequest;
import jakarta.ws.rs.GET;
import jakarta.ws.rs.Path;
import jakarta.ws.rs.core.Context;
import jakarta.ws.rs.core.Response;
import jakarta.ws.rs.core.SecurityContext;

@Path("news")
public class NewsService {

    @Context
    private HttpServletRequest httpRequest;

    @GET
    public String getNews() { //SecurityContext ctx) {
        // ctx.
        // httpRequest.log
    	System.out.println("News");
//    	
        return "Hi news";
    }
    
    @GET
    @Path("/1")
    public Response redirect() {
    	try {
//			Response.seeOther(new URI("google")).build();
			return Response.temporaryRedirect(new URI("https://accounts.google.com/o/oauth2/v2/auth")).build();
		} catch (URISyntaxException e) {
			e.printStackTrace();
			return null;
		}
    }

}
