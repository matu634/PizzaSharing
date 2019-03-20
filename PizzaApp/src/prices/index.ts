import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction} from "aurelia-router";
import {IPrice} from "../interfaces/IPrice";
import {PricesService} from "../services/prices-service";

export var log = LogManager.getLogger('Prices.Index');

//-----------Dependency injection-----------
@autoinject()
export class Index {
  
  private prices:  IPrice[] = [];

  constructor(private service : PricesService) {
    log.debug('constructor');
  }

  // ============ View LifeCycle events ==============
  created(owningView: View, myView: View) {
    log.debug('created');
  }

  bind(bindingContext: Object, overrideContext: Object) {
    log.debug('bind');
  }

  attached() {
    log.debug('attached');
    this.service.fetchAll().then(jsonData => {
      log.debug("jsonData", jsonData);
      this.prices = jsonData;  
    }).catch(reason => {
      log.debug('catch reason', reason)
    });
  }

  detached() {
    log.debug('detached');
  }

  unbind() {
    log.debug('unbind');
  }

  // ============= Router Events =============
  canActivate(params: any, routerConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
    log.debug('canActivate');
  }

  activate(params: any, routerConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
    log.debug('activate');
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}
