import React from 'react';
import { Switch, Form } from 'antd';
import { SwitchProps } from 'antd/es/switch';
import { FormItemProps } from 'antd/es/form';
import { t } from 'i18next';

interface SwitchFieldProps<T> extends Omit<SwitchProps, 'name'> {
    name: keyof T;
    label?: string;
    required?: boolean;
    rules?: FormItemProps['rules'];
    checkedChildren?: React.ReactNode;
    unCheckedChildren?: React.ReactNode;
    disabled?: boolean;
}

const SwitchField = <T,>({
    name,
    label,
    required = false,
    rules = [],
    checkedChildren,
    unCheckedChildren,
    disabled = false,
    ...rest
}: SwitchFieldProps<T>): JSX.Element => {
    return (
        <React.Fragment>
            <Form.Item
                name={String(name)}
                label={label}
                valuePropName="checked"
                rules={[
                    { required: required, message: t('component.requiredMessage', { label }) },
                    ...rules
                ]}
            >
                <Switch
                    disabled={disabled}
                    checkedChildren={checkedChildren}
                    unCheckedChildren={unCheckedChildren}
                    {...rest}
                />
            </Form.Item>
        </React.Fragment>
    );
};

export default SwitchField;
