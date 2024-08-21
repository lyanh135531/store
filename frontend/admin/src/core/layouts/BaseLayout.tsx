import logoImg from '@/assets/images/logo.jpg';
import useBreadcrumbMenu from '@/hooks/useBreadcrumb';
import { menus } from '@/menus';
import { Layout, Menu } from 'antd';
import { Content, Footer } from 'antd/es/layout/layout';
import Sider from 'antd/es/layout/Sider';
import React, { useState } from 'react';
import { Outlet, useNavigate } from 'react-router-dom';
import BaseBreadcrumb from '../components/common/BaseBreadcrumb';
import { Icons } from '../icons/icon';
import { useTheme } from '../providers/ThemeProvider';
import ThemeHeader from './ThemeHeader';
import UserHeader from './UserHeader';

const BaseLayout: React.FC = () => {
  const [collapsed, setCollapsed] = useState<boolean>(false);
  const { theme } = useTheme();
  const navigate = useNavigate();
  const { breadcrumbItems, handleMenuClick } = useBreadcrumbMenu();

  const renderIconCollapse = () => {
    if (collapsed) return <Icons.MenuUnfold />;
    return <Icons.MenuFold />;
  };

  return (
    <Layout className="flex h-full" hasSider>
      <Sider width={250} trigger={null} collapsible collapsed={collapsed} theme="light">
        <div
          className="h-16 content-center text-center cursor-pointer p-2"
          onClick={() => navigate('/')}>
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
      </Sider>

      <div className="flex flex-col flex-1">
        <div className="p-4 flex justify-between items-center bg-white">
          <div className="text-xl cursor-pointer" onClick={() => setCollapsed(!collapsed)}>
            {renderIconCollapse()}
          </div>
          <div className="flex items-center gap-4">
            <ThemeHeader />
            <UserHeader />
          </div>
        </div>

        <Content className="relative flex-1">
          <div className="absolute top-0 left-0 flex flex-col gap-4 w-full h-full p-4 pb-0 bg-gray-50 shadow-main-inner">
            <BaseBreadcrumb menuItems={breadcrumbItems} />
            <div className="bg-white w-full h-full rounded-xl shadow-main">
              <Outlet />
            </div>
          </div>
        </Content>
        <Footer className="self-center px-0 py-2">Store ©2024 Created by Lian</Footer>
      </div>
    </Layout>
  );
};

export default BaseLayout;
