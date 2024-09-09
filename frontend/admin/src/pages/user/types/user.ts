import { Entity, Gender } from '@/types/core';

export interface User extends Entity {
    userName: string;
    email: string;
    fullName?: string;
    phoneNumber: string;
    dateOfBirth: Date;
    status: boolean;
    gender: Gender;
    password: string;
}

export type UserDto = User;
