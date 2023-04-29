import { JwtUtil } from "../../../util/JWT";

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

    constructor() {
		window.signinGoogleCallback = (token) => this.googleCallback(token);
		window.signinTwitterCallback = (token) => this.twitterCallback(token);
		window.signinFacebookCallback = (token) => this.facebookCallback(token);
    }

    public clickSignup() {

    }

    public submitSignup() {
        
    }

    // public clickGoogle() {}
    // public clickTwitter() {}
    // public clickFacebook() {}

	public googleCallback(token) {
		console.log("hi callback")
		// decodeJwtResponse() is a custom function defined by you
		// to decode the credential response.
		let responsePayload = JwtUtil.decodeJwtResponse(token.credential)

		console.log("ID: " + responsePayload.sub);
		console.log('Full Name: ' + responsePayload.name);
		console.log('Given Name: ' + responsePayload.given_name);
		console.log('Family Name: ' + responsePayload.family_name);
		console.log("Image URL: " + responsePayload.picture);
		console.log("Email: " + responsePayload.email);
		
		console.log("set cookie: " + JSON.stringify(token));
		document.cookie = token;
	}
	public twitterCallback(token) {

	}
	public facebookCallback(token) {
		
	}

}
