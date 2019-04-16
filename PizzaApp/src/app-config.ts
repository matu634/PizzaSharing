import {LogManager, autoinject} from "aurelia-framework";
import * as jwt_decode from 'jwt-decode';
import {JWTPayload} from "./interfaces/JWTPayload";

export var log = LogManager.getLogger('AppConfig');

@autoinject
export class AppConfig {
  
  public apiUrl = 'https://localhost:5001/api/';
  public jwt: string | null = null;

  constructor() {
    log.debug('constructor');
  }

  getName(){
    if (this.jwt == null) return 'ERROR';
    console.log(jwt_decode(this.jwt));
    let t : JWTPayload = jwt_decode(this.jwt);
    return t["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]
  }

}
