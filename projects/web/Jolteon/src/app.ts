import { IRouteableComponent, IRoute, IRouter } from '@aurelia/router';
import { IStore } from '@aurelia/state';
// import 'bootstrap'; // Import the Javascript
// import 'bootstrap/dist/css/bootstrap.css'; // Import the CSS
import '@fortawesome/fontawesome-free';
import '@fortawesome/fontawesome-free/css/all.css';
import { IHttpClient, inject } from 'aurelia';
import { ActionNames, LoginAction, LogoutAction } from './jolteon/action-handler';
import { GlobalState } from './jolteon/initialstate';
import { Constants } from './jolteon/constants';
import { AuthController } from './jolteon/services/api/AuthController';

@inject(IHttpClient, IStore, AuthController, IRouter)
export class App implements IRouteableComponent {
	static routes: IRoute[] = [
		{
			path: ['', 'home'],
			component: import('./pages/home/home'),
			title: 'Home',
		},
		{
			path: 'profile',
			component: import('./pages/profile/profile'),
			title: 'Profile',
			// data: {
			// 	requiresAuth: true
			// }
		},
		// {
		// 	path: 'profile/settings',
		// 	component: import('./pages/profile/profileSettings'),
		// 	title: 'Profile Settings',
		// 	data: {
		// 		requiresAuth: true
		// 	}
		// },
		{
			path: 'editor',
			component: import('./pages/admin/editor/editor'),
			title: 'Editor',
			data: {
				requiresAuth: true
			}
		},
		{
			path: 'editor/creature/:uid',
			component: import('./pages/encyclopedia/creatures/creature'),
			title: 'Creature',
			// data: {
			// 	requiresAuth: true
			// }
		}
	];

	constructor(
		private readonly http: IHttpClient, 
		private readonly store: IStore<GlobalState, LogoutAction>, 
		private readonly auth: AuthController,
		private readonly router: IRouter
	) {
		// use the server url as base url
		this.http.baseUrl = Constants.serverUrl;

		// automatically load account info from the server
		this.auth.getAccountInfo().then(res => {
			let action: LoginAction = { type: ActionNames.login, value: res.data };
			console.log("getAccountInfo dispatch account info: " + JSON.stringify(action));
			this.store.dispatch(action);
		}, rej => {
			console.log("rejected app.getAccount")
		})

		// this.store.subscribe({
		// 	handleStateChange(state, prevState) {
		// 		console.log("subscriber")
		// 		// localStorage.setItem("state", JSON.stringify(state));
		// 	},
		// })

		// Response interceptor: signout and redirect to login/home if we tried to access unauthorized content
		http.configure(config =>
			config
				.withInterceptor({
					// request(request) {
					//     request.headers.append('Authorization', 'Bearer ' + getter);
					//     return request;
					// }
					response: async (res, req) => {
						// console.log("on receive");
						// console.log(req);
						// console.log(res);

						if (res.status == 401) { // && res.headers.has("logout")) {
							// let body = await res.text();
							// if (body === "logout") {
								// dispatch logoutAction + redirect to home TODO we can still navigate to the restricted area, need authorization on client side
								this.store.dispatch(new LogoutAction());
								router.load("home");
							// }
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
