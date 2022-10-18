package espeon.auth.jade;

public enum UserType {
    /**
     * If the user was created in-house with a username and password
     */
    custom,
    /**
     * If the user was provided by an external service like Google, Facebook...
     */
    externalService,
}
