/* eslint-disable @typescript-eslint/no-explicit-any */
import { CREATE_SUCCESS, NOTIFY_TITLE, UPDATE_SUCCESS } from '@/core/constants/notify';
import { ApiResponse } from '@/types/auth';
import { BaseFormProps, RecursivePartial } from '@/types/core';
import { ApiUtil } from '@/utils/apiUtil';
import NotifyUtil from '@/utils/notifyUtil';
import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { FormInstance } from 'antd';
import { t } from 'i18next';
import { useEffect } from 'react';
import { Identifier } from 'typescript';

interface UseFormMutationProps<T> extends BaseFormProps {
    id?: Identifier;
    form: FormInstance<T>;
    createUrl: string;
    updateUrl: string;
    getUrl?: string;
    initialValues?: RecursivePartial<T>;
}

export const useFormMutation = <T,>({
    id,
    createUrl,
    updateUrl,
    getUrl,
    form,
    initialValues,
    onSuccess
}: UseFormMutationProps<T>) => {
    const queryClient = useQueryClient();

    const { data, isLoading, isError, error } = useQuery({
        queryKey: ['FormData', id],
        queryFn: async () => {
            if (id && getUrl) {
                const response = await ApiUtil.Axios<ApiResponse<T>>('get', getUrl, { id });
                return response?.data?.result;
            }
            return null;
        },
        enabled: Boolean(id && getUrl),
        retry: false
    });

    const mutation = useMutation({
        mutationFn: async (formData: T) => {
            const url = id ? updateUrl : createUrl;
            const method = id ? 'put' : 'post';
            return ApiUtil.Axios<ApiResponse<T>>(method, url, null, { ...formData, id });
        },
        onSuccess: (response) => {
            if (response.data.success) {
                onSuccess?.();
                queryClient.invalidateQueries({ queryKey: ['FormData', id] });
                form.resetFields();

                const successMessage = id ? UPDATE_SUCCESS : CREATE_SUCCESS;
                NotifyUtil.success(NOTIFY_TITLE, successMessage);
            } else {
                console.error('Error:', response.data);
            }
        },
        onError: (error) => {
            console.error('Submission error:', error);
        }
    });

    useEffect(() => {
        if (data) {
            form.setFieldsValue(data);
        } else if (initialValues) {
            form.setFieldsValue(initialValues);
        } else {
            form.resetFields();
        }
    }, [data, form]);

    const onFinish = async () => {
        try {
            const values = await form.validateFields();
            await mutation.mutateAsync(values);
        } catch (error) {
            console.error('Form validation failed:', error);
        }
    };

    if (isError) {
        NotifyUtil.error(NOTIFY_TITLE, t(error.message));
    }

    return {
        data,
        isLoading,
        onFinish,
        isPending: mutation.isPending,
        isSuccess: mutation.isSuccess,
        error: mutation.error
    };
};
