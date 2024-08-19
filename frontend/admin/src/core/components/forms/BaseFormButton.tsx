import { Button } from 'antd';
import React from 'react';

interface BaseFormButtonProps {
  disabled?: boolean;
  configs: {
    confirm?: boolean;
    cancel?: boolean;
    confirmText?: string;
    classNameConfirm?: string;
    classNameCancel?: string;
  };
}

const BaseFormButton: React.FC<BaseFormButtonProps> = ({ configs, disabled }) => {
  const defaultProps = { disabled };
  const { cancel, confirm, confirmText, classNameConfirm, classNameCancel } = configs;

  return (
    <div className="flex gap-2 justify-end">
      {confirm && (
        <Button type="primary" htmlType="submit" className={classNameConfirm} {...defaultProps}>
          {confirmText ?? 'Confirm'}
        </Button>
      )}
      {cancel && (
        <Button type="default" className={classNameCancel} {...defaultProps}>
          Cancel
        </Button>
      )}
    </div>
  );
};

export default BaseFormButton;
