import { Identifier } from 'typescript';

export interface Entity {
    id: Identifier;
}

export type RecursivePartial<T> =
    NonNullable<T> extends object
        ? {
              [P in keyof T]?: NonNullable<T[P]> extends (infer U)[]
                  ? RecursivePartial<U>[]
                  : NonNullable<T[P]> extends object
                    ? RecursivePartial<T[P]>
                    : T[P];
          }
        : T;

export interface BaseFormProps {
    onSuccess?: () => void;
    onClose?: () => void;
}

export interface ComboOption {
    label: string;
    value: string | number;
}

export enum Gender {
    Male = 'Male',
    Female = 'Female'
}

export const GenderOptions: ComboOption[] = [
    { label: Gender.Male, value: Gender.Male },
    { label: Gender.Female, value: Gender.Female }
];
