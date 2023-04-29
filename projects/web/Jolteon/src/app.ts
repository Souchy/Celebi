import { IRouteableComponent, IRoute } from '@aurelia/router';
// import 'bootstrap'; // Import the Javascript
// import 'bootstrap/dist/css/bootstrap.css'; // Import the CSS
import '@fortawesome/fontawesome-free';
import '@fortawesome/fontawesome-free/css/all.css';
import { Home } from './pages/home/home';

export class App implements IRouteableComponent {
	static routes: IRoute[] = [
		{
			path: ['', 'home'],
			component: Home,
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

}
