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

const useLocaleStore = create<LocaleState>((set) => ({
    locale: enUS,
    setLocale: (localeKey: LocaleKey) => {
        switch (localeKey) {
            case LocaleKey.en:
                set({ locale: enUS });
                break;
            case LocaleKey.vi:
                set({ locale: viVN });
                break;
        }
    }
}));

export default useLocaleStore;
