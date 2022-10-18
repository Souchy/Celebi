package sylveon.api;

import jakarta.ws.rs.GET;
import jakarta.ws.rs.Path;

@Path("/")
public class MainResource {
    
    @GET
    public String root() { 
    	System.out.println("root/");
        return "Hi root";
    }
    
}
