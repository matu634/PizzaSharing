import {AppConfig} from '../app-config';
import {PLATFORM, LogManager, autoinject} from "aurelia-framework";
import {HttpClient} from "aurelia-fetch-client";
import {IReceiptDTO} from "../interfaces/IReceiptDTO";
import {IOrganizationDTO} from "../interfaces/IOrganizationDTO";
import {IReceiptRowDTO} from "../interfaces/IReceiptRowDTO";

export var log = LogManager.getLogger('ReceiptService');

@autoinject()
export class ReceiptService {


  constructor(
    private httpClient: HttpClient,
    private appConfig: AppConfig) {
    log.debug('constructor');
  }

  fetch(id: number): Promise<IReceiptDTO> {
    let url = this.appConfig.apiUrl + "app/receipt/" + id.toString();

    return this.httpClient.fetch(url, {
      cache: "no-store",
      headers: {
        Authorization: 'Bearer ' + this.appConfig.jwt,
      }
    })
      .then(response => response.json())
      .then(jsonData => jsonData);
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
      .then(jsonData => jsonData);
  }

  addReceiptRow(receiptRowDTO: IReceiptRowDTO) {
    let url = this.appConfig.apiUrl + "app/AddReceiptRow";

    return this.httpClient.post(url, JSON.stringify(receiptRowDTO),
      {
        cache: "no-store",
        headers: {
          Authorization: 'Bearer ' + this.appConfig.jwt,
        }
      })
      .then(response => response.json())
      .then(jsonData => jsonData);
  }
}
