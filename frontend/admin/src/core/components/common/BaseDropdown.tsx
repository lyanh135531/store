import React from 'react';
import { Dropdown, MenuProps } from 'antd';

interface DropdownBaseProps extends MenuProps {
  children: React.ReactNode;
  className?: string;
}

const BaseDropdown: React.FC<DropdownBaseProps> = ({
  children,
  items,
  className,
  ...restProps
}) => {
  return (
    <Dropdown
      className={className}
      placement="bottomRight"
      menu={{ items, selectable: true, ...restProps }}
      trigger={['click']}>
      {children}
    </Dropdown>
  );
};

export default BaseDropdown;
