import React from 'react';
import { MenuProps } from 'antd';
import { Icons } from '@/core/icons';
import { t } from 'i18next';

export type MenuItem = Required<MenuProps>['items'][number];

export const useMenu = (): MenuItem[] => {
    return [
        { key: 'home', icon: <Icons.Home />, label: t('menu.home') },
        { key: 'product', icon: <Icons.Skin />, label: t('menu.product') },
        { key: 'category', icon: <Icons.Shopping />, label: t('menu.category') },
        { key: 'user', icon: <Icons.Team />, label: t('menu.user') },
        { key: 'setting', icon: <Icons.Setting />, label: t('menu.setting') }
    ];
};
