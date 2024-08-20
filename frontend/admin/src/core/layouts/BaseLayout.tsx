import { menus } from '@/menus';
import { Layout, Menu } from 'antd';
import { Content } from 'antd/es/layout/layout';
import Sider from 'antd/es/layout/Sider';
import React, { useState } from 'react';
import { Outlet, useNavigate } from 'react-router-dom';
import { Icons } from '../icons/icon';
import { useTheme } from '../providers/ThemeProvider';
import ThemeHeader from './ThemeHeader';
import UserHeader from './UserHeader';

const BaseLayout: React.FC = () => {
  const [collapsed, setCollapsed] = useState<boolean>(false);
  const { theme } = useTheme();
  const navigate = useNavigate();

  const renderIconCollapse = () => {
    if (collapsed) return <Icons.MenuUnfold />;
    return <Icons.MenuFold />;
  };

  return (
    <Layout className="flex h-full">
      <Sider trigger={null} collapsible collapsed={collapsed} theme="light">
        <div className="h-16 content-center text-center">LOGO</div>
        <Menu
          className="h-full !border-none px-4 flex flex-col gap-2"
          theme={theme}
          mode="vertical"
          defaultSelectedKeys={['home']}
          items={menus}
          onClick={(info) => navigate(info.key)}
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
          <div className="absolute top-0 left-0 w-full h-full p-4 bg-gray-50 shadow-main-inner">
            <div className="bg-white w-full h-full rounded-xl shadow-main">
              <Outlet />
            </div>
          </div>
        </Content>
      </div>
    </Layout>
  );
};

export default BaseLayout;
