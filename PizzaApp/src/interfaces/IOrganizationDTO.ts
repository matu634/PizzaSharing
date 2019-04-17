import {ICategoryDTO} from "./ICategoryDTO";

export interface IOrganizationDTO {
  id: number,
  name: string,
  categories: ICategoryDTO[]
}
