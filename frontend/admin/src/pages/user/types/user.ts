import { Entity } from '@/types/core';

export interface User extends Entity {
    userName: string;
    email: string;
    fullName: string;
    phoneNumber: string;
    status: boolean;
}

export type UserDto = User;
