import BaseDropdown from '@/core/components/common/BaseDropdown';
import { Icons } from '@/core/icons';
import { Theme, useTheme } from '@/core/providers/ThemeProvider';
import { MenuProps } from 'antd';
import { t } from 'i18next';
import React from 'react';

const ThemeHeader: React.FC = () => {
    const { setTheme, theme } = useTheme();

    const items: MenuProps['items'] = [
        {
            key: 'light',
            label: t('theme.light'),
            icon: <Icons.Sun />
        },
        {
            key: 'dark',
            label: t('theme.dark'),
            icon: <Icons.Moon />
        }
    ];

    return (
        <BaseDropdown
            className="p-2 rounded-full hover:bg-main-fill-tertiary dark:hover:bg-dark-main-fill-tertiary dark:text-dark-main-secondary"
            defaultSelectedKeys={[theme]}
            items={items}
            onClick={({ key }) => setTheme(key as Theme)}
        >
            {theme === 'light' ? <Icons.Sun /> : <Icons.Moon />}
        </BaseDropdown>
    );
};

export default ThemeHeader;
