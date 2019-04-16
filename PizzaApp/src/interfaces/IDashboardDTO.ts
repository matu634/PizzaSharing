import {ILoanGivenDTO} from "./ILoanGivenDTO";
import {IReceiptDTO} from "./IReceiptDTO";
import {ILoanTakenDTO} from "./ILoanTakenDTO";

export interface IDashboardDTO {
  openReceipts : IReceiptDTO[]
  closedReceipts : IReceiptDTO[]
  loans: ILoanGivenDTO[] 
  debts: ILoanTakenDTO[] 
}
