import { enUS, viVN } from '@/locales';
import { Locale } from 'antd/es/locale';
import { create } from 'zustand';

interface LocaleState {
    locale: Locale;
    setLocale: (locale: LocaleKey) => void;
}

export enum LocaleKey {
    en = 'en',
    vi = 'vi'
}

const getStoredLocale = (): Locale => {
    const storedLocale = localStorage.getItem('locale');
    switch (storedLocale) {
        case LocaleKey.en:
            return enUS;
        case LocaleKey.vi:
            return viVN;
        default:
            return enUS;
    }
};

const useLocaleStore = create<LocaleState>((set) => ({
    locale: getStoredLocale(),
    setLocale: (localeKey: LocaleKey) => {
        let newLocale: Locale;

        switch (localeKey) {
            case LocaleKey.en:
                newLocale = enUS;
                break;
            case LocaleKey.vi:
                newLocale = viVN;
                break;
            default:
                newLocale = enUS;
                break;
        }

        localStorage.setItem('locale', localeKey);
        set({ locale: newLocale });
    }
}));

export default useLocaleStore;
