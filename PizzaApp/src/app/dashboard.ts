import { AppConfig } from '../app-config';
import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import * as jwt_decode from 'jwt-decode';
import {JWTPayload} from "../interfaces/JWTPayload";
import {IDashboardDTO} from "../interfaces/IDashboardDTO";
import {DashboardService} from "../services/dashboard-service";

export var log = LogManager.getLogger('Dashboard');

@autoinject
export class Dashboard {
  
  private dashboardDTO: IDashboardDTO;

  constructor(
    private service: DashboardService,
    public appConfig: AppConfig,
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

  getName(){
    if (this.appConfig.jwt == null) return 'ERROR'; 
    console.log(jwt_decode(this.appConfig.jwt));
    let t : JWTPayload = jwt_decode(this.appConfig.jwt);
    return t["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]
  }
}
