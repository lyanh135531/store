import { Identifier } from 'typescript';

export interface Entity {
  id: Identifier;
}

export enum Gender {
  Male = 'Male',
  Fermale = 'Female'
}
