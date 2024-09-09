import { DatePicker, Form } from 'antd';
import { DatePickerProps } from 'antd/es/date-picker';
import { FormItemProps } from 'antd/es/form';
import { Dayjs } from 'dayjs';
import { t } from 'i18next';
import React from 'react';
import dayjs from 'dayjs';

interface DatePickerFieldProps<T> extends Omit<DatePickerProps, 'name' | 'onChange'> {
    name: keyof T;
    label?: string;
    required?: boolean;
    rules?: FormItemProps['rules'];
    placeholder?: string;
    format?: string;
}

const DatePickerField = <T,>({
    name,
    label,
    required = false,
    rules = [],
    placeholder,
    format = 'DD/MM/YYYY',
    disabled = false,
    ...rest
}: DatePickerFieldProps<T>): JSX.Element => {
    return (
        <React.Fragment>
            <Form.Item
                name={String(name)}
                label={label}
                rules={[
                    { required: required, message: t('component.requiredMessage', { label }) },
                    ...rules
                ]}
                getValueProps={(value) => ({
                    value: value ? dayjs(value) : undefined,
                })}
                getValueFromEvent={(date: Dayjs | null) => date?.format('YYYY-MM-DD')}
            >
                <DatePicker
                    className="w-full"
                    format={format}
                    placeholder={placeholder || t('component.datePlaceholder')}
                    disabled={disabled}
                    {...rest}
                />
            </Form.Item>
        </React.Fragment>
    );
};

export default DatePickerField;
