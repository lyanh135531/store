import React from 'react';
import { Button, ButtonProps } from 'antd';
import clsx from 'clsx';

interface BaseButtonProps extends ButtonProps {
  variants?: '';
}

const BaseButton: React.FC<BaseButtonProps> = ({ className, ...props }) => {
  return <Button className={clsx('', className)} {...props} />;
};

export default BaseButton;
