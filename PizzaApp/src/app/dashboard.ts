import {AppConfig} from '../app-config';
import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IDashboardDTO} from "../interfaces/IDashboardDTO";
import {DashboardService} from "../services/dashboard-service";
import {LoanStatus} from "../interfaces/LoanStatus";
import {ILoanGivenDTO} from "../interfaces/ILoanGivenDTO";
import {LoanService} from "../services/loan-service";
import {ILoanTakenDTO} from "../interfaces/ILoanTakenDTO";

export var log = LogManager.getLogger('Dashboard');

@autoinject
export class Dashboard {

  private dashboardDTO: IDashboardDTO;

  constructor(
    private service: DashboardService,
    private appConfig: AppConfig,
    private router: Router,
    private loanService: LoanService
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
    if (this.appConfig.jwt == null) {
      return;
    }
    this.service.fetch().then(jsonData => {
      log.debug("jsonData", jsonData);
      this.dashboardDTO = jsonData;
      log.debug("DASHBOARD: ", this.dashboardDTO);
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
    if (this.appConfig.jwt == null) {
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

  newReceipt() {
    this.service.newReceipt()
      .then(value => {
        this.router.navigateToRoute("receiptView", {id: value});
      })
  }

  removeReceipt(receiptId: number) {
    this.service.removeReceipt(receiptId)
      .then(value => {
        if (value) {
          let index = this.dashboardDTO.openReceipts.findIndex(receipt => receipt.receiptId === receiptId);
          if (index < 0) {
            log.debug("Index not found. ");
            return;
          }
          this.dashboardDTO.openReceipts.splice(index, 1);
        }
      })
  }

  viewReceiptButtonClicked(receiptId: number) {
    this.router.navigateToRoute("receiptView", {id: receiptId});
  }

  viewOldReceiptButtonClicked(receiptId: number) {
    this.router.navigateToRoute("oldReceiptView", {id: receiptId});
  }

  getStatusName(status: LoanStatus): string {
    switch (status) {
      case LoanStatus.AwaitingConfirmation:
        return "Awaiting Confirmation";
      case LoanStatus.NotPaid:
        return "Not Paid";
      case LoanStatus.Paid:
        return "Paid";
      case LoanStatus.Rejected:
        return "Rejected";
      default:
        return "STATUS ERROR"
    }
  }
  canShowMarkPaidButton(status: LoanStatus) : boolean{
    switch (status) {
      case LoanStatus.AwaitingConfirmation:
        return false;
      case LoanStatus.NotPaid:
        return true;
      case LoanStatus.Paid:
        return false;
      case LoanStatus.Rejected:
        return true;
      default:
        return false
    }
  }

  canShowConfirmPaidButton(status: LoanStatus) : boolean{
    switch (status) {
      case LoanStatus.AwaitingConfirmation:
        return true;
      case LoanStatus.NotPaid:
        return true;
      case LoanStatus.Paid:
        return false;
      case LoanStatus.Rejected:
        return true;
      default:
        return false
    }
  }

  canShowRejectPaidButton(status: LoanStatus) : boolean{
    switch (status) {
      case LoanStatus.AwaitingConfirmation:
        return true;
      case LoanStatus.NotPaid:
        return false;
      case LoanStatus.Paid:
        return false;
      case LoanStatus.Rejected:
        return false;
      default:
        return false
    }
  }
  
  confirmPaidClicked(loan: ILoanGivenDTO){
    this.loanService.changeLoanStatus(loan.loanId, LoanStatus.Paid)
      .then(value => {
        log.debug(value);
        loan.status = value
      })
  }
  
  rejectPaidClicked(loan: ILoanGivenDTO){
    this.loanService.changeLoanStatus(loan.loanId, LoanStatus.Rejected)
      .then(value => {
        log.debug(value);
        loan.status = value
      })
  }

  markPaidClicked(debt: ILoanTakenDTO){
    this.loanService.changeLoanStatus(debt.loanId, LoanStatus.AwaitingConfirmation)
      .then(value => {
        log.debug(value);
        debt.status = value
      })
  }
}
