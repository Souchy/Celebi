package espeon.auth.jade;

import java.util.regex.Pattern;

public class UserValidator {

	
	private static final String pseudoRegex = "^[A-Z]'?[- a-zA-Z]( [a-zA-Z])*$";
	private static final String usernameRegex = "^[a-zA-Z0-9]([._-](?![._-])|[a-zA-Z0-9])[a-zA-Z0-9]$";
	private static final String emailRegex = "^[a-zA-Z0-9_!#$%&'*+/=?`{|}~^.-]+@[a-zA-Z0-9.-]+$";
	
	public static boolean validate(String pseudo, String username, String password, String email) {
		return validatePseudo(pseudo) && validateUsername(username) && validatePassword(password) && validateEmail(email);
	}
	
	public static boolean validateUsername(String username) {
		if(username.length() < 3 || username.length() > 20) return false;
		if(username.contains(";")) return false;
		return Pattern.compile(usernameRegex)
			      .matcher(username)
			      .matches();
	}
	
	public static boolean validatePassword(String password) {
		if(password.length() < 6 || password.length() > 20) return false;
		return true;
	}
	
	public static boolean validateEmail(String email) {
		if(email.contains(";")) return false;
		if(email.length() < 5 || email.length() > 30) return false;
		return Pattern.compile(emailRegex)
	      .matcher(email)
	      .matches();
	}

	public static boolean validatePseudo(String pseudo) {
		if(pseudo.length() < 3 || pseudo.length() > 20) return false;
		return Pattern.compile(pseudoRegex)
	      .matcher(pseudo)
	      .matches();
	}
	
}