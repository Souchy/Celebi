import { AuthController } from "../../../services/api/AuthController";
import { JwtUtil } from "../../../util/JWT";
import { SessionManager } from "../../../util/sessionManager";

declare global {
	interface Window {
		signupGoogleCallback: (response: any) => void;
		signupTwitterCallback: (response: any) => void;
		signupFacebookCallback: (response: any) => void;
	}
}

export class Signup {
    /**
     * 
     */
    public email: string;
    /**
     * 
     */
    public displayName: string;
    /**
     * pass
     */ 
    public password: string;

    constructor() { //private readonly session: SessionManager) {
		window.signupGoogleCallback = (token) => this.googleCallback(token);
		window.signupTwitterCallback = (token) => this.twitterCallback(token);
		window.signupFacebookCallback = (token) => this.facebookCallback(token);
    }

    public clickSignup() {

    }

    public submitSignup() {
        
    }

    // public clickGoogle() {}
    // public clickTwitter() {}
    // public clickFacebook() {}

	public googleCallback(token) {
		console.log("hi signup callback")
		console.log(token);
		// decodeJwtResponse() is a custom function defined by you
		// to decode the credential response.
		let responsePayload = JwtUtil.decodeJwtResponse(token.credential)

		console.log("ID: " + responsePayload.sub);
		console.log('Full Name: ' + responsePayload.name);
		console.log('Given Name: ' + responsePayload.given_name);
		console.log('Family Name: ' + responsePayload.family_name);
		console.log("Image URL: " + responsePayload.picture);
		console.log("Email: " + responsePayload.email);
		
		document.cookie = token;
		// location.href = "home";

		// new AuthController().postSignUp({
		// 	id: responsePayload.sub,
		// 	email: responsePayload.email,
		// });
	}
	public twitterCallback(token) {

	}
	public facebookCallback(token) {
		
	}

}
