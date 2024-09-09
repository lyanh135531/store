import { ApiResponse } from '@/types/auth';
import { ComboOption } from '@/types/core';
import { ApiUtil } from '@/utils/apiUtil';
import { useQuery } from '@tanstack/react-query';
import { Form, Select, Spin } from 'antd';
import { FormItemProps } from 'antd/es/form';
import { SelectProps } from 'antd/es/select';
import { t } from 'i18next';
import React from 'react';

const { Option } = Select;

interface ApiProps {
    url?: string;
    params?: Record<string, string>;
}

interface SelectFieldProps<T> extends Omit<SelectProps, 'name' | 'onChange'> {
    name: keyof T;
    label?: string;
    required?: boolean;
    rules?: FormItemProps['rules'];
    placeholder?: string;
    onChange?: (value: string | number) => void;
    options?: ComboOption[];
    proxy?: ApiProps;
}

const SelectField = <T,>({
    name,
    label,
    required = false,
    rules = [],
    placeholder,
    onChange,
    options,
    proxy,
    ...rest
}: SelectFieldProps<T>): JSX.Element => {
    const fetchComboOptions = async () => {
        const response = await ApiUtil.Axios<ApiResponse<ComboOption[]>>(
            'get',
            proxy?.url || '',
            proxy?.params
        );
        return response?.data?.result;
    };

    const {
        data: apiOptions,
        isLoading,
        isError
    } = useQuery({
        queryKey: ['SelectField', proxy],
        queryFn: fetchComboOptions,
        enabled: !!proxy
    });

    if (isError) {
        return <div>Error loading options</div>;
    }

    const renderedOptions = options || apiOptions || [];

    return (
        <React.Fragment>
            <Form.Item
                name={String(name)}
                label={label}
                rules={[
                    { required: required, message: t('component.requiredMessage', { label }) },
                    ...rules
                ]}
            >
                <Select
                    placeholder={placeholder || t('component.selectPlaceholder', { label })}
                    loading={isLoading}
                    onChange={(value) => onChange && onChange(value)}
                    notFoundContent={isLoading ? <Spin size="small" /> : t('component.noOptions')}
                    {...rest}
                >
                    {renderedOptions?.map((option) => (
                        <Option key={option.value} value={option.value}>
                            {option.label}
                        </Option>
                    ))}
                </Select>
            </Form.Item>
        </React.Fragment>
    );
};

export default SelectField;
