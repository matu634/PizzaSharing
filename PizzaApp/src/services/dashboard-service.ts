import { AppConfig } from './../app-config';
import {PLATFORM, LogManager, autoinject} from "aurelia-framework";
import {HttpClient} from "aurelia-fetch-client";
import {RouterConfiguration, Router} from "aurelia-router";
import {IPrice} from "../interfaces/IPrice";
import {IDashboardDTO} from "../interfaces/IDashboardDTO";

export var log = LogManager.getLogger('DashboardService');

@autoinject()
export class DashboardService {


  constructor(
    private httpClient: HttpClient,
    private appConfig : AppConfig ) {
    log.debug('constructor');
  }

  fetch() : Promise<IDashboardDTO> {
    let url = this.appConfig.apiUrl + "app/dashboard";

    return this.httpClient.fetch(url, {
      cache: "no-store",
      headers: {
        Authorization: 'Bearer ' + this.appConfig.jwt,
      }
    })
      .then(response => response.json())
      .then(jsonData => jsonData);
  }
  
  newReceipt(){
    let url = this.appConfig.apiUrl + "receipts/newReceipt";

    return this.httpClient.post(url, "",
      {
        cache: "no-store",
        headers: {
          Authorization: 'Bearer ' + this.appConfig.jwt,
        }
      })
      .then(response => response.json())
      .catch(reason => {
        log.debug("newReceipt error:", reason);
        return -1;
      });
  }

}
