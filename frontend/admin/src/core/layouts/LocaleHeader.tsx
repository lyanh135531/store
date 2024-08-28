import BaseDropdown from '@/core/components/common/BaseDropdown';
import { Icons } from '@/core/icons';
import i18n from '@/i18n';
import { enUS } from '@/locales';
import useLocaleStore, { LocaleKey } from '@/stores/localeStore';
import { MenuProps } from 'antd';
import { t } from 'i18next';
import React from 'react';

const LocaleHeader: React.FC = () => {
    const { locale, setLocale } = useLocaleStore();

    const items: MenuProps['items'] = [
        {
            key: LocaleKey.en,
            label: t('locale.en')
        },
        {
            key: LocaleKey.vi,
            label: t('locale.vi')
        }
    ];

    const changeLanguage = (lang: string) => {
        i18n.changeLanguage(lang);
    };

    return (
        <BaseDropdown
            className="p-2 rounded-full hover:bg-main-fill-tertiary dark:hover:bg-dark-main-fill-tertiary dark:text-dark-main-secondary"
            defaultSelectedKeys={[locale === enUS ? LocaleKey.en : LocaleKey.vi]}
            items={items}
            onClick={({ key }) => {
                if (key in LocaleKey) {
                    changeLanguage(key);
                    setLocale(key as LocaleKey);
                }
            }}
        >
            <Icons.Global />
        </BaseDropdown>
    );
};

export default LocaleHeader;
