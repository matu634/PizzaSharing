import { AppConfig } from './../app-config';
import { IdentityService } from './../services/identity-service';
import { LogManager, View, autoinject } from "aurelia-framework";
import { RouteConfig, NavigationInstruction, Router } from "aurelia-router";

export var log = LogManager.getLogger('Identity.Login');

//-----------Dependency injection-----------
@autoinject()
export class Login {

  //TODO: remove fixed pw and email
  private email: string = "matu@sirg.com";
  private password: string = "Password";

  constructor(
    private IdentityService: IdentityService,
    private appConfig: AppConfig,
    private router: Router) {
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
    if(this.appConfig.jwt != null) {
      this.router.navigateToRoute("dashboard");
    }
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }

  //======================ViewMethods=========================
  submit() {
    log.debug("submit", this.email, this.password)
    this.IdentityService.login(this.email, this.password)
    .then(jwtDTO => {
      if (jwtDTO.token !== undefined) {
        log.debug("Submit token", jwtDTO.token);
        this.appConfig.jwt = jwtDTO.token;
        this.router.navigateToRoute("dashboard");
      }
    })
  }
}
