import { Identifier } from 'typescript';

export interface Entity {
    id: Identifier;
}

export interface BaseFormProps {
    onSuccess?: () => void;
    onClose?: () => void;
}

export enum Gender {
    Male = 'Male',
    Female = 'Female'
}
