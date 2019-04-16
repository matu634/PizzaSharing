import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {AppConfig} from "../app-config";
import {IdentityService} from "../services/identity-service";

export var log = LogManager.getLogger('Identity.Register');

//-----------Dependency injection-----------
@autoinject()
export class Index {
  private email: string = "anka@gmail.com";
  private password: string = "Password";
  private confirmPassword : string = "Password";
  private firstName : string = "Anka";
  private lastName : string = "EiÜtle";
  private nickname : string = "Anka123";

  constructor(
    private appConfig: AppConfig,
    private router: Router,
    private identityService : IdentityService
  ) {
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
    log.debug("submit", this.email, this.password, this.confirmPassword, this.nickname, this.firstName, this.lastName);
    this.identityService.register(this.email, this.nickname, this.password, this.confirmPassword, this.firstName, this.lastName)
      .then(jwtDTO => {
        if (jwtDTO.token !== undefined) {
          log.debug("Submit token", jwtDTO.token);
          this.appConfig.jwt = jwtDTO.token;
          this.router.navigateToRoute("dashboard");
        }
        else {
          //TODO: display error
        }
      })
  }
}
