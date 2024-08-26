import { Identifier } from 'typescript';

export interface Entity {
    id: Identifier | string;
}

export enum Gender {
    Male = 'Male',
    Female = 'Female'
}
