import {IProductDTO} from "./IProductDTO";

export interface ICategoryDTO {
  id: number,
  name: string,
  products: IProductDTO[]
}
