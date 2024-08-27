import { Button, ButtonProps } from 'antd';
import clsx from 'clsx';
import React from 'react';

interface BaseButtonProps extends ButtonProps {
    variants?: ButtonProps['type'];
}

const BaseButton: React.FC<BaseButtonProps> = ({ className, variants = 'default', ...props }) => {
    return (
        <Button
            className={clsx(
                'h-8 px-4',
                {
                    '!w-8': !props.children
                },
                className
            )}
            type={variants}
            {...props}
        />
    );
};

export default BaseButton;
