import {AppConfig} from '../app-config';
import {PLATFORM, LogManager, autoinject} from "aurelia-framework";
import {HttpClient} from "aurelia-fetch-client";
import {IParticipantDTO} from "../interfaces/IParticipantDTO";
import {LoanStatus} from "../interfaces/LoanStatus";

export var log = LogManager.getLogger('LoanService');

@autoinject()
export class LoanService {


  constructor(
    private httpClient: HttpClient,
    private appConfig: AppConfig) {
    log.debug('constructor');
  }

  changeLoanStatus(loanId: number, newLoanStatus: LoanStatus) {
    let url = this.appConfig.apiUrl + "Loans/ChangeLoanStatus";

    let body = JSON.stringify({loanId: loanId, status: newLoanStatus});
    log.debug("changeLoanStatus request body: ", body);

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
      .catch(reason => log.debug("removeRowChange error:", reason));
  }
}
