import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IPrice} from "../interfaces/IPrice";
import {PricesService} from "../services/prices-service";

export var log = LogManager.getLogger('Prices.Create');

@autoinject()
export class Create {

  private price: IPrice;

  constructor(
    private router: Router,
    private pricesService: PricesService
  ) {
    log.debug('constructor');
  }

  // ================================================= View methods ====================================================

  submit(): void {
    log.debug('price:', this.price);
    // this.price.productId = 1;
    // this.pricesService.post(this.price).then(response => {
    //   if (response.status == 201){
    //     this.router.navigateToRoute("pricesIndex")
    //   } else {
    //     log.error('Error in response!', response)
    //   }
    // });
  }


  // ============================================== View LifeCycle events ==============================================
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
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}
