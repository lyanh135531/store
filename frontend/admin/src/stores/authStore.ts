import { AuthUser } from '@/types/auth';
import { create } from 'zustand';

interface AuthState {
    isAuthenticated: boolean;
    user: AuthUser | null;
    setUser: (user: AuthUser | null) => void;
}

const useAuthStore = create<AuthState>((set) => ({
    isAuthenticated: false,
    user: null,
    setUser: (user) => set({ user, isAuthenticated: !!user })
}));

export default useAuthStore;
