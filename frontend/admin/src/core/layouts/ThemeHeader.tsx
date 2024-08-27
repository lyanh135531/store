import BaseDropdown from '@/core/components/common/BaseDropdown';
import { Icons } from '@/core/icons';
import { Theme, useTheme } from '@/core/providers/ThemeProvider';
import { MenuProps } from 'antd';
import React from 'react';

const items: MenuProps['items'] = [
    {
        key: 'light',
        label: 'Light',
        icon: <Icons.Sun />
    },
    {
        key: 'dark',
        label: 'Dark',
        icon: <Icons.Moon />
    }
];

const ThemeHeader: React.FC = () => {
    const { setTheme, theme } = useTheme();

    return (
        <BaseDropdown
            className="p-2 rounded-full hover:bg-main-hover"
            defaultSelectedKeys={[theme]}
            items={items}
            onClick={({ key }) => setTheme(key as Theme)}
        >
            {theme === 'light' ? <Icons.Sun /> : <Icons.Moon />}
        </BaseDropdown>
    );
};

export default ThemeHeader;
