import {ILoanGivenDTO} from "./ILoanGivenDTO";
import {IReceiptMinDTO} from "./IReceiptMinDTO";
import {ILoanTakenDTO} from "./ILoanTakenDTO";

export interface IDashboardDTO {
  openReceipts : IReceiptMinDTO[]
  closedReceipts : IReceiptMinDTO[]
  loans: ILoanGivenDTO[] 
  debts: ILoanTakenDTO[] 
}
