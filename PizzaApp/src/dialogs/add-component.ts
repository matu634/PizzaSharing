import {DialogController} from 'aurelia-dialog';
import {autoinject} from 'aurelia-framework';
import {IChangeDTO} from "../interfaces/IChangeDTO";

@autoinject
export class AddComponent {
  private changes : IChangeDTO[] = [
    // {categoryId:1, changeId:1, name:"pepperoni", organizationId: 1, price: 10, receiptRowId: 1},
    // {categoryId:1, changeId:1, name:"cheese", organizationId: 1, price: 5, receiptRowId: 1},
    // {categoryId:1, changeId:1, name:"tomato", organizationId: 1, price: 2, receiptRowId: 1}
    ];
  constructor(private controller: DialogController){
    this.controller = controller;
  }
  
  activate(changes: IChangeDTO[]){
    this.changes = changes;
  }
  
  changeButtonClicked(changeId: number) {
    
  }
}
