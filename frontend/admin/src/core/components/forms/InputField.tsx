import { Form, Input } from 'antd';
import { FormItemProps } from 'antd/es/form';
import { FormItemLayout } from 'antd/es/form/Form';
import { InputProps } from 'antd/es/input';
import clsx from 'clsx';
import React from 'react';
import { useTranslation } from 'react-i18next';

interface InputFieldProps<T> extends Omit<InputProps, 'name'> {
    id?: string;
    autoComplete?: string;
    name: keyof T;
    label?: string;
    rules?: FormItemProps['rules'];
    placeholder?: string;
    type?: InputProps['type'];
    icon?: React.ReactNode;
    layout?: FormItemLayout;
    required?: boolean;
}

const PHONE_NUMBER_REGEX = /^(0?)(3[2-9]|5[6|8|9]|7[0|6-9]|8[1-9]|9[0-9])[0-9]{7}$/;

const InputField = <T,>({
    name,
    label,
    rules = [],
    placeholder,
    type = 'text',
    icon,
    layout,
    required,
    ...props
}: InputFieldProps<T>) => {
    const { t } = useTranslation();

    const defaultClassName = clsx({ 'cursor-default': props.readOnly });

    const getInputComponent = (inputType: InputProps['type']) => {
        switch (inputType) {
            case 'password':
                return Input.Password;
            default:
                return Input;
        }
    };

    const InputComponent = getInputComponent(type);

    const inputProps = {
        className: defaultClassName,
        type,
        placeholder: placeholder ?? label,
        prefix: icon,
        maxLength: type === 'tel' && 10,
        ...props
    } as InputProps;

    const formItemRules = [
        { required, message: t('component.requiredMessage', { label }) },
        ...(type === 'tel'
            ? [{ pattern: PHONE_NUMBER_REGEX, message: t('component.invalidPhoneNumber') }]
            : []),
        ...rules
    ];

    return (
        <Form.Item name={String(name)} label={label} rules={formItemRules} layout={layout}>
            <InputComponent {...inputProps} />
        </Form.Item>
    );
};

export default InputField;
