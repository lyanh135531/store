import { Entity, Gender } from '@/types/core';

export interface AuthUser extends Entity {
    userName: string;
    email: string;
    gender: Gender;
    fullName?: string;
}

export interface ApiResponse<T> {
    success: boolean;
    result: T;
}
