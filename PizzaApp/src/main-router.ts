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
        {route: 'prices/details', name: 'pricesDetails',  moduleId: PLATFORM.moduleName('prices/details'),  nav: true, title: 'Details'},
        {route: 'prices/create',  name: 'pricesCreate',   moduleId: PLATFORM.moduleName('prices/create'),   nav: true, title: 'Create'},
        {route: 'prices/edit',    name: 'pricesEdit',     moduleId: PLATFORM.moduleName('prices/edit'),     nav: true, title: 'Edit'},
        {route: 'prices/delete',  name: 'pricesDelete',   moduleId: PLATFORM.moduleName('prices/delete'),   nav: true, title: 'Delete'}
      ]
    );

  }

}
