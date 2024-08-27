import React from 'react';
import { MenuProps } from 'antd';
import { Icons } from '@/core/icons';

export type MenuItem = Required<MenuProps>['items'][number];

export const menus: MenuItem[] = [
    { key: 'home', icon: <Icons.Home />, label: 'Home Page' },
    { key: 'product', icon: <Icons.Skin />, label: 'Product' },
    { key: 'category', icon: <Icons.Shopping />, label: 'Category' },
    { key: 'user', icon: <Icons.Team />, label: 'User' },
    { key: 'setting', icon: <Icons.Setting />, label: 'Setting' }
];
