import { Entity, Gender } from './core';

export interface AuthUser extends Entity {
  userName: string;
  email: string;
  gender: Gender;
  fullName?: string;
}
