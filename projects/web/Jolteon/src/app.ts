import { IRouteableComponent, IRoute, IRouter } from '@aurelia/router';
import { IStore } from '@aurelia/state';
// import 'bootstrap'; // Import the Javascript
// import 'bootstrap/dist/css/bootstrap.css'; // Import the CSS
import '@fortawesome/fontawesome-free';
import '@fortawesome/fontawesome-free/css/all.css';
import { IEventAggregator, IHttpClient, inject } from 'aurelia';
import { ActionNames, LoginAction, LogoutAction } from './jolteon/action-handler';
import { GlobalState } from './jolteon/initialstate';
import { Constants, Effects } from './jolteon/constants';
import { AuthController } from './jolteon/services/api/AuthController';
import { TOAST_PLACEMENT, TOAST_STATUS, Toast, ToastConfigOptions, ToastOptions } from 'bootstrap-toaster';
import { PropertiesController } from './jolteon/services/api/PropertiesController';

@inject(IHttpClient, IStore, IEventAggregator, IRouter, AuthController)
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
			// data: {
			// 	requiresAuth: true
			// }
		},
		{
			path: 'editor/creature/:uid',
			component: import('./pages/encyclopedia/creatures/creature'),
			title: 'Creature',
		},
		{
			path: 'editor/spell/:uid',
			component: import('./pages/encyclopedia/spells/spell'),
			title: 'Spell',
		}
	];

	private readonly toastConfig: ToastConfigOptions = {
		enableQueue: false,
		placement: TOAST_PLACEMENT.TOP_CENTER,
		maxToasts: 2
	}

	constructor(
		private readonly http: IHttpClient,
		private readonly store: IStore<GlobalState, LogoutAction>,
		private readonly ea: IEventAggregator,
		private readonly router: IRouter,
		private readonly auth: AuthController,
		private readonly propertiesController: PropertiesController
	) {
		console.log("JOLTEON APP CTOR " + location.hostname)
		// toast on saved/failed post operations to server
		Toast.configure(this.toastConfig);
		ea.subscribe("operation:saved", this.toastSaved);
		ea.subscribe("operation:failed", this.toastFailed);

		// use the server url as base url
		if(location.hostname == 'localhost') {
			this.http.baseUrl = Constants.serverUrl;
		}

		// load server data
		this.loadServerData();

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
		this.configureInterceptor(http);
	}


	private async loadServerData() {
		// load effect schemas
		Effects.schemas = (await this.propertiesController.getEffectsSchemas()).data;
	}

	private configureInterceptor(http: IHttpClient) {
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

						// if unauthorized: logout and return to /home
						if (res.status == 401) { // && res.headers.has("logout")) {
							// let body = await res.text();
							// if (body === "logout") {
							// dispatch logoutAction + redirect to home TODO we can still navigate to the restricted area, need authorization on client side
							this.store.dispatch(new LogoutAction());
							this.router.load("home");
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

	public toastSaved() {
		// console.log("toast saved")
		let toast: ToastOptions = {
			title: "Operation",
			message: "Saved",
			status: TOAST_STATUS.SUCCESS,
			timeout: 2000
		}
		// Toast.configure(this.toastConfig);
		Toast.create(toast);
	}
	public toastFailed() {
		// console.log("toast failed")
		let toast = {
			title: "Operation",
			message: "Failed",
			status: TOAST_STATUS.WARNING,
			timeout: 2000
		}
		// Toast.configure(this.toastConfig);
		Toast.create(toast);
	}

}
