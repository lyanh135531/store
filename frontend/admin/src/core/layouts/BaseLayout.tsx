import logoImg from '@/assets/images/logo.jpg';
import BaseBreadcrumb from '@/core/components/common/BaseBreadcrumb';
import { Icons } from '@/core/icons';
import LocaleHeader from '@/core/layouts/LocaleHeader';
import ThemeHeader from '@/core/layouts/ThemeHeader';
import UserHeader from '@/core/layouts/UserHeader';
import { useTheme } from '@/core/providers/ThemeProvider';
import useBreadcrumbMenu from '@/hooks/useBreadcrumb';
import { useMenu } from '@/menus';
import { Layout, Menu } from 'antd';
import { Content, Footer } from 'antd/es/layout/layout';
import Sider from 'antd/es/layout/Sider';
import React, { useState } from 'react';
import { Outlet, useNavigate } from 'react-router-dom';

const BaseLayout: React.FC = () => {
    const [collapsed, setCollapsed] = useState<boolean>(false);
    const { theme } = useTheme();
    const menus = useMenu();
    const navigate = useNavigate();
    const { breadcrumbItems, handleMenuClick } = useBreadcrumbMenu();

    const renderIconCollapse = () => {
        if (collapsed) return <Icons.Right />;
        return <Icons.Left />;
    };

    return (
        <Layout className="flex h-full" hasSider>
            <Sider
                className="relative group"
                width={250}
                trigger={null}
                collapsible
                collapsed={collapsed}
                theme={theme}
            >
                <div
                    className="h-16 content-center text-center cursor-pointer p-2"
                    onClick={() => navigate('/')}
                >
                    <img className="w-full h-full object-cover" src={logoImg} alt="logo" />
                </div>
                <Menu
                    className="h-full !border-none px-4 flex flex-col gap-2"
                    theme={theme}
                    mode="vertical"
                    selectedKeys={[location?.pathname?.split('/')?.[1]]}
                    items={menus}
                    onClick={(info) => handleMenuClick(info.key)}
                />
                <div
                    className="hidden group-hover:block cursor-pointer absolute -right-4 top-1/2 z-10 py-1 px-2 border rounded-full bg-gray-100"
                    onClick={() => setCollapsed(!collapsed)}
                >
                    {renderIconCollapse()}
                </div>
            </Sider>

            <div className="flex flex-col flex-1">
                <div className="p-4 flex justify-end items-center bg-white dark:bg-dark-main-primary">
                    <div className="flex items-center gap-4">
                        <LocaleHeader />
                        <ThemeHeader />
                        <UserHeader />
                    </div>
                </div>

                <Content className="relative flex-1 dark:bg-dark-main-surface-secondary">
                    <div className="absolute px-20 top-0 left-0 flex flex-col gap-4 w-full h-full pt-4 pb-0 shadow-main-inner">
                        <BaseBreadcrumb menuItems={breadcrumbItems} />
                        <div className="bg-white flex-1 w-full h-full rounded-lg shadow-main overflow-hidden dark:bg-dark-main-primary">
                            <Outlet />
                        </div>
                    </div>
                </Content>
                <Footer className="self-center px-0 py-2 w-full text-center dark:bg-dark-main-surface-secondary dark:text-dark-main-secondary">
                    Store ©2024 Created by Lian ❤️
                </Footer>
            </div>
        </Layout>
    );
};

export default BaseLayout;
