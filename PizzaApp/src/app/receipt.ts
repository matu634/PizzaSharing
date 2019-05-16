import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {ReceiptService} from "../services/receipt-service";
import {IReceiptDTO} from "../interfaces/IReceiptDTO";
import {AppConfig} from "../app-config";
import {IOrganizationDTO} from "../interfaces/IOrganizationDTO";
import {ICategoryDTO} from "../interfaces/ICategoryDTO";
import {IProductDTO} from "../interfaces/IProductDTO";
import {IReceiptRowDTO} from "../interfaces/IReceiptRowDTO";
import {IReceiptRowMinDTO} from "../interfaces/IReceiptRowMinDTO";
import {DialogService} from "aurelia-dialog";
import {AddComponent} from "../dialogs/add-component";
import {IChangeDTO} from "../interfaces/IChangeDTO";

export var log = LogManager.getLogger('Receipt');

@autoinject
export class Receipt {

  private receiptDTO: IReceiptDTO;
  private organizations: IOrganizationDTO[];
  private selectedOrganization: IOrganizationDTO | null = null;
  private selectedCategory: ICategoryDTO | null = null;

  constructor(
    private receiptService: ReceiptService,
    private appConfig: AppConfig,
    private router: Router,
    private dialogService: DialogService
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
      return;
    }

    this.receiptService.fetch(params.id)
      .then(value => {
        this.receiptDTO = value;
        log.debug("receiptDTO object: ", this.receiptDTO)
      });

    this.receiptService.fetchOrganizations(params.id)
      .then(value => {
        this.organizations = value;
        log.debug("organizations: ", this.organizations)
      });
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }

  // ---------------------------View methods---------------------------
  organizationOnChange() {
    log.debug('Change. Organization: ', typeof this.selectedOrganization);
    if (this.selectedOrganization !== null) {
      $("#categorySelect").prop("disabled", false);

    } else {
      $("#categorySelect").prop("disabled", true)
    }
  }

  categoryOnChange() {
    log.debug('Change. Organization: ', typeof this.selectedOrganization);
    if (this.selectedOrganization !== null) {
      $("#products").prop("hidden", false);
    } else {
      $("#products").prop("hidden", true)
    }
  }

  addToCartClicked(product: IProductDTO) {
    log.debug("Product clicked: ", product);
    if (product === null || product.productId === null) return;

    let rowDTO: IReceiptRowMinDTO = {
      amount: 1,
      productId: product.productId,
      receiptId: this.receiptDTO.receiptId,
      discount: null
    };
    this.receiptService.addReceiptRow(rowDTO)
      .then(value => {
        log.debug("Returned row", value);
        this.receiptDTO.rows.push(value);
        this.updateTotalPrice();
      });
  }

  removeRowClicked(id: number) {
    this.receiptService.removeReceiptRow(id).then(value => {
      if (value !== -1) {
        this.receiptDTO.rows = this.receiptDTO.rows.filter(function (obj) {
          return obj.receiptRowId !== value;
        });
        this.updateTotalPrice()
      }
    })
  }

  updateTotalPrice() {
    this.receiptDTO.sumCost = 0;
    this.receiptDTO.rows.forEach(value =>
      this.receiptDTO.sumCost += value.currentCost !== null ? value.currentCost : 0)
  }

  changeRowAmountClicked(newAmount: number, rowDto: IReceiptRowDTO) {
    if (rowDto.receiptRowId === null) return;
    this.receiptService.changeReceiptRowAmount(newAmount, rowDto.receiptRowId)
      .then(updatedRow => {
        log.debug("Updated row:", updatedRow);
        let index = this.receiptDTO.rows.findIndex(row => row.receiptRowId === rowDto.receiptRowId);
        if (index < 0) {
          log.debug("Index not found. ");
          return;
        }

        // this.receiptDTO.rows[index] = updatedRow; Can't use this, aurelia doesn't detect changes by index
        this.receiptDTO.rows.splice(index, 1, updatedRow);
        this.updateTotalPrice();
        log.debug("Current rows: ", this.receiptDTO.rows)
      })
  }

  removeReceipt() {
    this.receiptService.removeReceipt(this.receiptDTO.receiptId)
      .then(value => {
        if (value) {
          this.router.navigateToRoute("dashboard");
        }
      })
  }


  addItemClicked(row: IReceiptRowDTO) {
    if (row.product.productId == null) return;
    
    this.receiptService.fetchAvailableChanges(row.product.productId)
      .then(changes => {
        this.dialogService.open({viewModel: AddComponent, model: changes, lock: false})
          .whenClosed(response => {
            if (!response.wasCancelled && row.receiptRowId != null) {
              this.addReceiptRowComponent(response.output.changeId, row.receiptRowId);
            } else {
              console.log('bad');
            }
          });
      });
  }

  addReceiptRowComponent(changeId: number, receiptRowId: number) {
    log.debug("Adding new component. ComponentId : " + changeId + " RowId:" + receiptRowId)
    this.receiptService.addChangeToRow(changeId, receiptRowId)
      .then(updatedRow => {
        let index = this.receiptDTO.rows.findIndex(row => row.receiptRowId === updatedRow.receiptRowId);
        if (index < 0) {
          log.debug("Index not found. ");
          return;
        }

        // this.receiptDTO.rows[index] = updatedRow; Can't use this, aurelia doesn't detect changes by index
        this.receiptDTO.rows.splice(index, 1, updatedRow);
        this.updateTotalPrice();
        log.debug("Current rows: ", this.receiptDTO.rows)
      })
  }
  
  removeReceiptRowComponent(changeId: number, receiptRowId: number) {
    this.receiptService.removeChangeFromRow(changeId, receiptRowId)
      .then(updatedRow => {
        let index = this.receiptDTO.rows.findIndex(row => row.receiptRowId === updatedRow.receiptRowId);
        if (index < 0) {
          log.debug("Index not found. ");
          return;
        }

        // this.receiptDTO.rows[index] = updatedRow; Can't use this, aurelia doesn't detect changes by index
        this.receiptDTO.rows.splice(index, 1, updatedRow);
        this.updateTotalPrice();
        log.debug("Current rows: ", this.receiptDTO.rows)
      })
  }
}
