import { enUS } from '@/locales';
import { AuthUser } from '@/types/auth';
import { Locale } from 'antd/es/locale';
import { create } from 'zustand';

interface AuthState {
    isAuthenticated: boolean;
    user: AuthUser | null;
    locale: Locale;
    setUser: (user: AuthUser | null) => void;
    setLocale: (locale: Locale) => void;
}

const useAuthStore = create<AuthState>((set) => ({
    isAuthenticated: false,
    user: null,
    locale: enUS,
    setUser: (user) => set({ user, isAuthenticated: !!user }),
    setLocale: (locale) => set({ locale })
}));

export default useAuthStore;
