import {IParticipantDTO} from "./IParticipantDTO";
import {IProductDTO} from "./IProductDTO";
import {IChangeDTO} from "./IChangeDTO";

export interface IReceiptRowDTO {
  receiptRowId: number,
  receiptId: number,
  amount: number,
  currentCost: number,
  discount: number,
  product: IProductDTO
  changes: IChangeDTO[]
  participants: IParticipantDTO[]
}
