import {DialogController} from 'aurelia-dialog';
import {autoinject} from 'aurelia-framework';
import {IUserDTO} from "../interfaces/IUserDTO";

@autoinject
export class AddParticipant {
  private users : IUserDTO[] = [];
  private maxInvolvement : number;
  private selectedValue: number = 0;
  
  constructor(private controller: DialogController){
    this.controller = controller;
  }

  activate(data: [IUserDTO[], number]){
    this.users = data[0];
    this.maxInvolvement = data[1];
  }
}
