import Aurelia from 'aurelia';
import { RouterConfiguration } from '@aurelia/router';
import { App } from './app';

Aurelia
  // .register(RouterConfiguration)
  // To use HTML5 pushState routes, replace previous line with the following
  // customized router config.
  .register(RouterConfiguration.customize({ useUrlFragmentHash: false }))
  .app(App)
  .start();
