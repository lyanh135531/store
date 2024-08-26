import React from 'react';
import { Form, Input } from 'antd';
import { Rule } from 'antd/es/form';
import { InputProps } from 'antd/es/input';

interface InputFieldProps<T> {
    id?: string;
    autoComplete?: string;
    name: keyof T;
    label?: string;
    rules?: Rule[];
    placeholder?: string;
    type?: InputProps['type'];
    icon?: React.ReactNode;
}

const InputField = <T,>({
    name,
    label,
    rules,
    placeholder,
    type = 'text',
    icon,
    ...props
}: InputFieldProps<T>) => {
    const renderField = () => {
        switch (type) {
            case 'password':
                return (
                    <Input.Password
                        type={type}
                        placeholder={placeholder ?? label}
                        prefix={icon}
                        {...props}
                    />
                );
            default:
                return (
                    <Input
                        type={type}
                        placeholder={placeholder ?? label}
                        prefix={icon}
                        {...props}
                    />
                );
        }
    };

    return (
        <Form.Item name={name as string} label={label} rules={rules}>
            {renderField()}
        </Form.Item>
    );
};

export default InputField;
