import {IReceiptRowDTO} from "./IReceiptRowDTO";

export interface IReceiptDTO {
  receiptId: number,
  createdTime: string,
  isFinalized: boolean,
  managerNickname: string
  sumCost: number,
  rows : IReceiptRowDTO[]
}
