import React from 'react';
import { Checkbox, Form } from 'antd';
import { CheckboxChangeEvent, CheckboxProps } from 'antd/es/checkbox';

interface CheckboxFieldProps<T> extends Omit<CheckboxProps, 'name'> {
    name: keyof T;
    label?: string;
    checked?: boolean;
    children?: React.ReactNode;
    noStyle?: boolean;
    onChange?: (e: CheckboxChangeEvent) => void;
}

const CheckboxField = <T,>({
    name,
    label,
    children,
    checked = false,
    noStyle = false,
    onChange,
    ...props
}: CheckboxFieldProps<T>) => {
    return (
        <Form.Item name={name as string} valuePropName="checked" label={label} noStyle={noStyle}>
            <Checkbox checked={checked} onChange={onChange} {...props}>
                {children}
            </Checkbox>
        </Form.Item>
    );
};

export default CheckboxField;
