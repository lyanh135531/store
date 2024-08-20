import { MenuProps } from 'antd';
import React from 'react';
import BaseDropdown from '../components/common/BaseDropdown';
import { Icons } from '../icons/icon';
import { Theme, useTheme } from '../providers/ThemeProvider';

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
      onClick={({ key }) => setTheme(key as Theme)}>
      {theme === 'light' ? <Icons.Sun /> : <Icons.Moon />}
    </BaseDropdown>
  );
};

export default ThemeHeader;
