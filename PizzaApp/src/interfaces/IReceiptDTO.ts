import {IReceiptRowDTO} from "./IReceiptRowDTO";

export interface IReceiptDTO {
  receiptId: number,
  createdTime: string,
  isFinalized: boolean,
  sumCost: number,
  rows : IReceiptRowDTO[]
}
