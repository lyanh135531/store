import { useLogout } from '@/hooks/authQuery';
import useAuthStore from '@/stores/authStore';
import { UserOutlined } from '@ant-design/icons';
import { Avatar, Dropdown, MenuProps, Space, Typography } from 'antd';
import React from 'react';
import { Icons } from '../icons/icon';

const UserHeader: React.FC = () => {
  const { user } = useAuthStore();
  const { mutate: logout } = useLogout();

  const handleLogout = () => {
    logout();
  };

  const items: MenuProps['items'] = [
    {
      key: 'userInfo',
      label: 'Information',
      icon: <Icons.User />
    },
    {
      key: 'changePassword',
      label: 'Change password',
      icon: <Icons.Key />
    },
    {
      type: 'divider'
    },
    {
      key: 'logout',
      label: 'Logout',
      icon: <Icons.Logout />,
      onClick: handleLogout
    }
  ];

  return (
    <Dropdown menu={{ items }} trigger={['click']}>
      <Space className="cursor-pointer">
        <Avatar icon={<UserOutlined />} />
        <Typography.Text className='select-none'>{user?.userName}</Typography.Text>
      </Space>
    </Dropdown>
  );
};

export default UserHeader;
