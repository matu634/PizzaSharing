import { AppConfig } from './../app-config';
import {PLATFORM, LogManager, autoinject} from "aurelia-framework";
import {HttpClient} from "aurelia-fetch-client";
import {RouterConfiguration, Router} from "aurelia-router";
import {IPrice} from "../interfaces/IPrice";

export var log = LogManager.getLogger('PricesService');

@autoinject()
export class PricesService {


  constructor(
    private httpClient: HttpClient,
    private serviceAppConfig : AppConfig ) {
    log.debug('constructor');
  }

  fetchAll(): Promise<IPrice[]> {
    let url =  this.serviceAppConfig.apiUrl + 'prices';

    return this.httpClient.fetch(url, {
      cache: "no-store",
      headers: {
        Authorization: 'Bearer ' + this.serviceAppConfig.jwt,
      }
    })
      .then(response => response.json())
      .then(jsonData => jsonData);
  }

  post(entity: IPrice): Promise<Response> {
    let url = this.serviceAppConfig.apiUrl + 'prices';

    return this.httpClient.post(url, JSON.stringify(entity), {cache: "no-store"})
      .then(response => response)
  }

  fetchSingle(id: number) : Promise<IPrice> {
    let url = this.serviceAppConfig.apiUrl + 'prices/' + id.toString();

    return this.httpClient.fetch(url, {cache: "no-store"}).then(response => response.json())
  }

  put(entity: IPrice) : Promise<Response>{
    let url = this.serviceAppConfig.apiUrl + 'prices/' + entity.id.toString();

    return this.httpClient.put(url, JSON.stringify(entity), {cache: "no-store"})
      .then(response => response)
  }

  delete(id: number) : Promise<Response> {
    let url = this.serviceAppConfig.apiUrl + 'prices/' + id.toString();

    return this.httpClient.delete(url, {cache: "no-store"}).then(response => response)
  }

}
