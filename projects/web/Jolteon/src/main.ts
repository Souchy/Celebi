import Aurelia from 'aurelia';
import { App } from './app';
import { RouterConfiguration } from '@aurelia/router';
import { I18nConfiguration } from '@aurelia/i18n';
import { StateDefaultConfiguration } from '@aurelia/state';
import { GlobalState, initialState } from './jolteon/initialstate';
import { LoginHandler, LogoutHandler } from './jolteon/action-handler';
import Fetch from 'i18next-fetch-backend';
import * as fr from './res/i18n/fr.json';
import * as en from './res/i18n/en.json';

Aurelia
	// .register(RouterConfiguration)
	// To use HTML5 pushState routes, replace previous line with the following
	// customized router config.
	.register(RouterConfiguration.customize({ useUrlFragmentHash: process.env.useUrlFragmentHash ? !!!process.env.useUrlFragmentHash : true }))
	.register(
		I18nConfiguration.customize((options) => {
			options.initOptions = {
				// plugins: [Fetch],
				// backend: {
				// 	loadPath: '/res/i18n/{{lng}}.json', // /{{ns}}.json
				// },
				resources: {
					en: { translation: en },
					fr: { translation: fr },
				},
				fallbackLng: "fr"
			};
		})
	)
	// .register(
	// 	StoreConfiguration.withInitialState(initialState)
	// )
	.register(
		StateDefaultConfiguration.init<GlobalState>(
			initialState,
			LoginHandler,
			LogoutHandler
		)
	)
	// .register(
	// 	StateDefaultConfiguration.init(
	// 	  initialState,
	// 	  keywordsHandler
	// 	)
	//   )
	.app(App)
	.start();
