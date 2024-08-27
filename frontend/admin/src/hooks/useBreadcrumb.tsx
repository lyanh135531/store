import { Icons } from '@/core/icons';
import { useMenu } from '@/menus';
import useLocaleStore from '@/stores/localeStore';
import _ from 'lodash';
import React, { useEffect, useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';

export interface BreadcrumbItem {
    title: React.ReactNode;
    href?: string;
}

interface Menu {
    key: string;
    icon: React.ReactNode;
    label: string;
}

const useBreadcrumbMenu = () => {
    const navigate = useNavigate();
    const menus = useMenu();
    const location = useLocation();
    const { locale } = useLocaleStore();
    const [breadcrumbItems, setBreadcrumbItems] = useState<BreadcrumbItem[]>([]);

    useEffect(() => {
        const updateBreadcrumb = () => {
            const currentMenu = (menus.find((menu) => _.includes(location.pathname, menu?.key)) ||
                menus[0]) as Menu;

            const newBreadcrumbItems: BreadcrumbItem[] = [
                { title: <Icons.Home /> },
                { title: currentMenu.label, href: '/' + currentMenu.key }
            ];

            setBreadcrumbItems(newBreadcrumbItems);
        };

        updateBreadcrumb();
    }, [location.pathname, locale]);

    const handleMenuClick = (key: string) => {
        const currentMenu = menus.find((menu) => menu?.key === key) as Menu;

        if (currentMenu) {
            const newBreadcrumbItems: BreadcrumbItem[] = [
                { title: <Icons.Home /> },
                { title: currentMenu.label, href: '/' + currentMenu.key }
            ];

            setBreadcrumbItems(newBreadcrumbItems);
            navigate(key);
        }
    };

    return {
        breadcrumbItems,
        handleMenuClick
    };
};

export default useBreadcrumbMenu;
