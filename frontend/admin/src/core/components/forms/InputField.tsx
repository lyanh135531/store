import React from 'react';
import { Form, Input } from 'antd';
import { Rule } from 'antd/es/form';
import { InputProps } from 'antd/es/input';

interface InputFieldProps<T> {
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
  icon
}: InputFieldProps<T>) => {
  const renderField = () => {
    switch (type) {
      case 'password':
        return <Input.Password type={type} placeholder={placeholder ?? label} prefix={icon} />;
      default:
        return <Input type={type} placeholder={placeholder ?? label} prefix={icon} />;
    }
  };

  return (
    <Form.Item name={name as string} label={label} rules={rules}>
      {renderField()}
    </Form.Item>
  );
};

export default InputField;
