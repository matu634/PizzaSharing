import { LogManager, autoinject } from "aurelia-framework";
import { HttpClient } from 'aurelia-fetch-client';
import { IBaseEntity } from "../interfaces/IBaseEntity";
import { AppConfig } from "../app-config";

export var log = LogManager.getLogger('IdentityService');

//https://www.typescriptlang.org/docs/handbook/generics.html

@autoinject
export class IdentityService {

  constructor(
    private httpClient: HttpClient,
    private appConfig: AppConfig,
    private endPoint: string,
  ) {
    log.debug('constructor');
  }

  login(email: String, password: String): Promise<any> {
    let url = this.appConfig.apiUrl + "account/login";
    let loginDTO = {
      email: email,
      password: password
    };

    return this.httpClient.post(url, JSON.stringify(loginDTO), { cache: 'no-store' }).then(
      response => {
        log.debug('response', response);
        return response.json();
      }
    ).catch(
      reason => {
        log.debug('catch reason', reason);
      }
    );
  }

  register(): void {

  }
}
