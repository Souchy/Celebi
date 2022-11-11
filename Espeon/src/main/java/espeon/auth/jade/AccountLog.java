package espeon.auth.jade;

import java.util.Date;

/**
 * When something happens to an account: ex it's being updated with new values or they bought something from the shop, or they've been banned, etc
 * @author Blank
 */
public class AccountLog {

    public Date date = new Date();
    public User previous;
    public String event;
    
}
