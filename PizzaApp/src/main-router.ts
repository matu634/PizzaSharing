import {PLATFORM, LogManager} from "aurelia-framework";
import {RouterConfiguration, Router} from "aurelia-router";

export var log = LogManager.getLogger('MainRouter');

export class MainRouter {

  public router: Router;

  constructor() {
    log.debug('constructor');
  }

  configureRouter(config: RouterConfiguration, router: Router): void {
    log.debug('configureRouter');

    this.router = router;
    config.title = "Contact App - Aurelia";
    config.map(
      [
        {route: ['', 'home'], name: 'home', moduleId: PLATFORM.moduleName('home'), nav: true, title: 'Home'},
        
        // {route: '', name: '', moduleId: PLATFORM.moduleName(''), nav: true, title: ''}
        {route: ['prices/index', 'prices'],   name: 'pricesIndex',    moduleId: PLATFORM.moduleName('prices/index'),    nav: true, title: 'Prices'},
        {route: 'prices/details/:id', name: 'pricesDetails',  moduleId: PLATFORM.moduleName('prices/details'),  nav: false, title: 'Details'},
        {route: 'prices/create',  name: 'pricesCreate',   moduleId: PLATFORM.moduleName('prices/create'),   nav: false, title: 'Create'},
        {route: 'prices/edit/:id',    name: 'pricesEdit',     moduleId: PLATFORM.moduleName('prices/edit'),     nav: false, title: 'Edit'},
        {route: 'prices/delete/:id',  name: 'pricesDelete',   moduleId: PLATFORM.moduleName('prices/delete'),   nav: false, title: 'Delete'}
      ]
    );

  }

}
