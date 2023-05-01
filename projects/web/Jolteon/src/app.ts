import { IRouteableComponent, IRoute } from '@aurelia/router';
// import 'bootstrap'; // Import the Javascript
// import 'bootstrap/dist/css/bootstrap.css'; // Import the CSS
import '@fortawesome/fontawesome-free';
import '@fortawesome/fontawesome-free/css/all.css';
import { IHttpClient, inject } from 'aurelia';
import { LogoutAction } from './jolteon/action-handler';
import { IStore } from '@aurelia/state';

@inject(IHttpClient)
export class App implements IRouteableComponent {
	static routes: IRoute[] = [
		{
			path: ['', 'home'],
			component: import('./pages/home/home'),
			title: 'Home',
		},
		{
			path: 'profile/settings',
			component: import('./pages/profile/profileSettings'),
			title: 'Profile Settings',
			data: {
				requiresAuth: true
			}
		}
	]

	constructor(private readonly http: IHttpClient, private readonly store: IStore<{}, LogoutAction>) {
        http.configure(config =>
            config
                .withInterceptor({
                    // request(request) {
                    //     request.headers.append('Authorization', 'Bearer ' + getter);
                    //     return request;
                    // }
					response: (res, req) => {
						console.log("on receive");
						if(res.status == 401 && res.headers.has("logout")) {
							// dispatch logoutAction
							this.store.dispatch({}); 
						}
						return res;
					},
					// responseError: (err, req, client) => {
					// 	return err;
					// }
                })
        );
	}

}
