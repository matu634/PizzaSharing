import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {PricesService} from "../services/prices-service";
import {IPrice} from "../interfaces/IPrice";

export var log = LogManager.getLogger('Prices.Delete');

@autoinject()
export class Delete {
  
  private price: IPrice;

  constructor(
    private router: Router,
    private priceService: PricesService
  ) {
    log.debug('constructor');
  }
  
  submit(): void{
    this.priceService.delete(this.price.id).then(response =>{
      if (response.status == 200){
        this.router.navigateToRoute("pricesIndex");
      } else {
        log.error("response", response)
      }
    })
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
    this.priceService.fetchSingle(params.id).then(value => {
      log.debug("price", value);
      this.price = value;
      
    })
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}
