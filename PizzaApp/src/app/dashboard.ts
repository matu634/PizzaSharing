import { AppConfig } from '../app-config';
import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IDashboardDTO} from "../interfaces/IDashboardDTO";
import {DashboardService} from "../services/dashboard-service";

export var log = LogManager.getLogger('Dashboard');

@autoinject
export class Dashboard {
  
  private dashboardDTO: IDashboardDTO;

  constructor(
    private service: DashboardService,
    private appConfig: AppConfig,
    private router: Router
  ) {
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
    if(this.appConfig.jwt == null) {
      return;
    }
    this.service.fetch().then(jsonData => {
      log.debug("jsonData", jsonData);
      this.dashboardDTO = jsonData;
      log.debug("DASHBOARD: ", this.dashboardDTO)
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
    if(this.appConfig.jwt == null) {
      this.router.navigateToRoute("identityLogin");
    }
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }

  //===================================View methods===================================

  newReceipt(){
    this.service.newReceipt()
      .then(value =>{
        this.router.navigateToRoute("receiptView",{id: value});
      })
  }
}
