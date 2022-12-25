
import { DI, Registration } from 'aurelia';
import { HttpClient } from '@aurelia/fetch-client';

export class Api {
    
	public http = new HttpClient();
	public clientid = "850322629277-c9fu1umd1dlk7tjv325u6s33g32fb0ea.apps.googleusercontent.com";
	public redirect_uri = "http://localhost:7000/auth/google";
	public gapi_uri = "https://accounts.google.com/o/oauth2/v2/auth";


	constructor() {
        // this.http.interceptors.push({
        //     request: this.requestInterceptor,
		// 	response: this.responseInterceptor,
        // });
	}

    // public requestInterceptor(req: Request): Request | Response | Promise<Request | Response> {
    //     return null;
    // }
    // public responseInterceptor(response: Response, request?: Request): Response | Promise<Response> {
    //     return null;
    // }


	public getJwtToken() {

	}

    public getGoogleOAuthURL(): string {
        let options = {
            client_id: this.clientid,
            redirect_uri: this.redirect_uri, 
            response_type: "code",
            scope: [
                "https://www.googleapis.com/auth/userinfo.profile",
                "https://www.googleapis.com/auth/userinfo.email"
            ].join(" "),
            access_type: "offline",
            prompt: "select_account",
        }
        console.log({ options })
        let qs = new URLSearchParams(options);
        console.log(qs.toString())

        let url = this.gapi_uri + "?" + qs.toString();
        console.log(url)
        return url;
    }

	public getGoogleCode() {
		this.http.fetch(this.getGoogleOAuthURL()).then(res => {
			
		});
	}

}

const container = DI.createContainer();
container.register(
	Registration.singleton(Api, Api)
);
