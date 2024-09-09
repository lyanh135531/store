import { Form, FormInstance, FormProps } from 'antd';
import clsx from 'clsx';
import React from 'react';

interface BaseFormProps<T> extends FormProps {
    className?: string;
    onFinish: (values: T) => void;
    form?: FormInstance;
    children: React.ReactNode;
}

const BaseForm = <T,>({ onFinish, form, className, children, ...rest }: BaseFormProps<T>) => {
    return (
        <Form
            className={clsx('gap-x-4', className)}
            form={form}
            onFinish={onFinish}
            {...rest}
        >
            {children}
        </Form>
    );
};

export default BaseForm;
