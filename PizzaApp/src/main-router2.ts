import 'admin-lte/plugins/jquery/jquery.min.js'
import 'admin-lte/plugins/bootstrap/js/bootstrap.bundle.min.js'
import 'admin-lte/dist/js/adminlte.js'


import 'admin-lte/plugins/chart.js/Chart.min.js'
import 'admin-lte/dist/js/demo.js'
import 'admin-lte/dist/js/pages/dashboard3.js'

import { AppConfig } from './app-config';
import {PLATFORM, LogManager, autoinject} from "aurelia-framework";
import {RouterConfiguration, Router} from "aurelia-router";

export var log = LogManager.getLogger('MainRouter');

@autoinject
export class MainRouter {

  public router: Router;

  constructor(private appConfig: AppConfig) {
    log.debug('constructor');
  }

  configureRouter(config: RouterConfiguration, router: Router): void {
    log.debug('configureRouter');

    this.router = router;
    config.title = "Contact App - Aurelia";
    config.map(
      [
        {route: ['', 'dashboard'], name: 'dashboard', moduleId: PLATFORM.moduleName('app/dashboard'), nav: true, title: 'Home'},
        
        // {route: '', name: '', moduleId: PLATFORM.moduleName(''), nav: true, title: ''}
        {route: ['prices/index', 'prices'],   name: 'pricesIndex',    moduleId: PLATFORM.moduleName('prices/index'),    nav: false, title: 'Prices'},
        {route: 'prices/details/:id', name: 'pricesDetails',  moduleId: PLATFORM.moduleName('prices/details'),  nav: false, title: 'Details'},
        {route: 'prices/create',  name: 'pricesCreate',   moduleId: PLATFORM.moduleName('prices/create'),   nav: false, title: 'Create'},
        {route: 'prices/edit/:id',    name: 'pricesEdit',     moduleId: PLATFORM.moduleName('prices/edit'),     nav: false, title: 'Edit'},
        {route: 'prices/delete/:id',  name: 'pricesDelete',   moduleId: PLATFORM.moduleName('prices/delete'),   nav: false, title: 'Delete'},

        {route: 'identity/login', name: 'identityLogin', moduleId: PLATFORM.moduleName('identity/login'), nav: false, title: 'Login'},
        {route: 'identity/register', name: 'identityRegister', moduleId: PLATFORM.moduleName('identity/register'), nav: false, title: 'Register'},
        {route: 'identity/logout', name: 'identityLogout', moduleId: PLATFORM.moduleName('identity/logout'), nav: false, title: 'Logout'}
      ]
    );

  }

}
