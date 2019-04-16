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
    private serviceAppConfig : AppConfig ) {
    log.debug('constructor');
  }

  fetch() : Promise<IDashboardDTO> {
    let url = 'https://localhost:5001/api/app/dashboard';

    return this.httpClient.fetch(url, {
      cache: "no-store",
      headers: {
        Authorization: 'Bearer ' + this.serviceAppConfig.jwt,
      }
    })
      .then(response => response.json())
      .then(jsonData => jsonData);
  }

}
