export interface IChangeDTO {
  changeId: number,
  name: string,
  price: number,
  receiptRowId: number | null,
  organizationId: number | null,
  categoryId: number | null
}
