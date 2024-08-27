import { enUS, viVN } from '@/locales';
import { Locale } from 'antd/es/locale';
import { create } from 'zustand';

interface LocaleState {
    locale: Locale;
    setLocale: (locale: LocaleKey) => void;
}

export enum LocaleKey {
    enUS = 'enUS',
    viVN = 'viVN'
}

const useLocaleStore = create<LocaleState>((set) => ({
    locale: enUS,
    setLocale: (localeKey: LocaleKey) => {
        switch (localeKey) {
            case 'enUS':
                set({ locale: enUS });
                break;
            case 'viVN':
                set({ locale: viVN });
                break;
        }
    }
}));

export default useLocaleStore;
