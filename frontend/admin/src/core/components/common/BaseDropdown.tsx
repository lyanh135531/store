import React from 'react';
import { Dropdown, MenuProps } from 'antd';
import clsx from 'clsx';

interface DropdownBaseProps extends MenuProps {
    children: React.ReactNode;
    className?: string;
    overlayClassName?: string;
}

const BaseDropdown: React.FC<DropdownBaseProps> = ({
    children,
    items,
    className,
    overlayClassName,
    ...restProps
}) => {
    return (
        <Dropdown
            className={className}
            placement="bottomRight"
            overlayClassName={clsx('shadow-primary rounded', overlayClassName)}
            menu={{ items, selectable: true, ...restProps }}
            trigger={['click']}
        >
            {children}
        </Dropdown>
    );
};

export default BaseDropdown;
