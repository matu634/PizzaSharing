import {AppConfig} from '../app-config';
import {PLATFORM, LogManager, autoinject} from "aurelia-framework";
import {HttpClient} from "aurelia-fetch-client";
import {IReceiptDTO} from "../interfaces/IReceiptDTO";
import {IOrganizationDTO} from "../interfaces/IOrganizationDTO";
import {IReceiptRowDTO} from "../interfaces/IReceiptRowDTO";
import {IReceiptRowMinDTO} from "../interfaces/IReceiptRowMinDTO";
import {IChangeDTO} from "../interfaces/IChangeDTO";

export var log = LogManager.getLogger('ReceiptService');

@autoinject()
export class ReceiptService {


  constructor(
    private httpClient: HttpClient,
    private appConfig: AppConfig) {
    log.debug('constructor');
  }

  fetch(id: number): Promise<IReceiptDTO> {
    let url = this.appConfig.apiUrl + "receipts/get/" + id.toString();

    return this.httpClient.fetch(url, {
      cache: "no-store",
      headers: {
        Authorization: 'Bearer ' + this.appConfig.jwt,
      }
    })
      .then(response => response.json())
      .then(jsonData => jsonData)
      .catch(reason => log.debug("fetch error:", reason));
  }

  fetchOrganizations(receiptId: number): Promise<IOrganizationDTO[]> {
    let url = this.appConfig.apiUrl + "app/Organizations/" + receiptId.toString();

    return this.httpClient.fetch(url, {
      cache: "no-store",
      headers: {
        Authorization: 'Bearer ' + this.appConfig.jwt,
      }
    })
      .then(response => response.json())
      .then(jsonData => jsonData)
      .catch(reason => log.debug("fetchOrganizations error:", reason));
    
  }

  addReceiptRow(receiptRowDTO: IReceiptRowMinDTO) {
    let url = this.appConfig.apiUrl + "receipts/AddRow";

    return this.httpClient.post(url, JSON.stringify(receiptRowDTO),
      {
        cache: "no-store",
        headers: {
          Authorization: 'Bearer ' + this.appConfig.jwt,
        }
      })
      .then(response => response.json())
      .then(jsonData => jsonData)
      .catch(reason => log.debug("addReceiptRow error:", reason));
  }

  removeReceiptRow(receiptRowId: number) : Promise<number> {
    let url = this.appConfig.apiUrl + "receipts/removeRow/" + receiptRowId.toString();

    return this.httpClient.post(url, "",
      {
        cache: "no-store",
        headers: {
          Authorization: 'Bearer ' + this.appConfig.jwt,
        }
      })
      .then(response => receiptRowId)
      .catch(reason => {
        log.debug("addReceiptRow error:", reason);
        return -1;
      });
  }
  
  changeReceiptRowAmount(newAmount: number, receiptRowId: number) {
    let url = this.appConfig.apiUrl + "receipts/UpdateRowAmount" ;

    let body = JSON.stringify({ReceiptRowId: receiptRowId, NewAmount: newAmount});
    log.debug("changeReceiptRowAmount request body: ", body);
    
    return this.httpClient.post(
      url, 
      body,
      {
        cache: "no-store",
        headers: {
          Authorization: 'Bearer ' + this.appConfig.jwt,
        }
      })
      .then(updatedReceiptRow => updatedReceiptRow.json())
      .catch(reason => log.debug("changeReceiptRowAmount error:", reason));
  }

  removeReceipt(receiptId: number) : Promise<boolean> {
    let url = this.appConfig.apiUrl + "receipts/removeReceipt/" + receiptId.toString();

    return this.httpClient.post(url, "",
      {
        cache: "no-store",
        headers: {
          Authorization: 'Bearer ' + this.appConfig.jwt,
        }
      })
      .then(response => true)
      .catch(reason => {
        log.debug("removeReceipt error:", reason);
        return false;
      });
  }
  
  fetchAvailableChanges(productId: number) : Promise<IChangeDTO[]>{
    let url = this.appConfig.apiUrl + "receipts/ProductChanges/" + productId.toString();
    
    return this.httpClient.fetch(url, {
      cache: "no-store",
      headers: {
        Authorization: 'Bearer ' + this.appConfig.jwt,
      }
    })
      .then(response => response.json())
      .then(jsonData => jsonData)
      .catch(reason => log.debug("fetchOrganizations error:", reason));
  }
}
