import { create } from 'zustand';
import { AuthUser } from '~/types/auth';

interface AuthState {
  user: AuthUser | null;
  setUser: (user: AuthUser | null) => void;
}

const useAuthStore = create<AuthState>((set) => ({
  user: null,
  setUser: (user) => set({ user })
}));

export default useAuthStore;
