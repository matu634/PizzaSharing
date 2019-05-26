import {LoanStatus} from "./LoanStatus";

export interface ILoanTakenDTO {
  loanId: number
  receiptId: number
  owedAmount : number
  loanGiverName: string
  status: LoanStatus
}
