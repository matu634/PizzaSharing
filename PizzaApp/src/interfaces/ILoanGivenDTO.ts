import {LoanStatus} from "./LoanStatus";

export interface ILoanGivenDTO {
  loanId : number
  receiptId: number
  owedAmount: number
  loanTakerName : string
  status: LoanStatus
}
