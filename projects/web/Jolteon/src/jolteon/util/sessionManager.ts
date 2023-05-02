import { HttpClient } from '@aurelia/fetch-client';
import { newInstanceOf } from '@aurelia/kernel';
import { DI, ICustomElementViewModel, IHttpClient, Registration } from 'aurelia';
import { Cookies, getCookie } from 'typescript-cookie';
// import { FullRequestParams, HttpResponse, HttpClient as h2 } from '../services/api/http-client';


export class SessionManager {

    constructor(//@newInstanceOf(IHttpClient) 
                readonly http: IHttpClient) {
        let getter = this.getBearerToken;
        http.configure(config =>
            config
                .withInterceptor({
                    request(request) {
                        request.headers.append('Authorization', 'Bearer ' + getter);
                        return request;
                    }
                })
        );
    }

    public getBearerToken() {
        // return sessionStorage.getItem("jwt")
        // return Cookies.get("bearer");
        return localStorage.get("bearer");
    }
    public setBearerToken(token) {
        // sessionStorage.setItem("jwt", token)
        // Cookies.set("bearer", token);
        localStorage.set("bearer", token);
    }

    // Longer duration refresh token (30-60 min)
    // public getRefreshToken() {
    //     return sessionStorage.getItem("refreshToken")
    // }
    // public setRefreshToken(token) {
    //     sessionStorage.setItem("refreshToken", token)
    // }
}

const container = DI.createContainer();
container.register(
  Registration.singleton(SessionManager, SessionManager),
//   Registration.instance(fetch, fakeFetch)
);
