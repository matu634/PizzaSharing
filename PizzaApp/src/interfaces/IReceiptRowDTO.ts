import {IParticipantDTO} from "./IParticipantDTO";
import {IProductDTO} from "./IProductDTO";
import {IChangeDTO} from "./IChangeDTO";

export interface IReceiptRowDTO {
  receiptRowId: number | null;
  receiptId: number;
  amount: number;
  currentCost: number | null;
  discount: number | null;
  product: IProductDTO;
  changes: IChangeDTO[] | null;
  participants: IParticipantDTO[] | null;
}
