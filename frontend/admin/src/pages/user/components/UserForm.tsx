import Loading from '@/core/components/common/Loading';
import BaseForm from '@/core/components/forms/BaseForm';
import DatePickerField from '@/core/components/forms/DatePickerField';
import InputField from '@/core/components/forms/InputField';
import SelectField from '@/core/components/forms/SelectField';
import SwitchField from '@/core/components/forms/SwitchField';
import DrawerContainer from '@/core/layouts/DrawerContainer';
import { useFormMutation } from '@/hooks/useFormMutation';
import { CREATE_USER_API, GET_USER_API, UPDATE_USER_API } from '@/pages/user/apis';
import { User } from '@/pages/user/types/user';
import { BaseFormProps, Gender, GenderOptions } from '@/types/core';
import { useForm } from 'antd/es/form/Form';
import { t } from 'i18next';
import React from 'react';
import { Identifier } from 'typescript';

interface Props extends BaseFormProps {
    id?: Identifier;
}

export type UserFormModel = User & {
    confirmPassword: string;
};

const UserForm: React.FC<Props> = ({ id, onClose, onSuccess }) => {
    const [form] = useForm<UserFormModel>();
    const { onFinish, isLoading, isPending } = useFormMutation<UserFormModel>({
        form,
        id,
        initialValues: {
            gender: Gender.Male,
            status: true
        },
        createUrl: CREATE_USER_API,
        updateUrl: UPDATE_USER_API,
        getUrl: GET_USER_API,
        onSuccess,
        onClose
    });

    if (isPending) return <Loading />;

    return (
        <DrawerContainer
            title={id ? t('user.edit') : t('user.create')}
            loading={isLoading}
            onSuccess={onFinish}
            onClose={onClose}
        >
            <BaseForm<UserFormModel>
                form={form}
                layout="vertical"
                className="grid grid-cols-2"
                onFinish={onFinish}
            >
                <InputField<UserFormModel>
                    name="userName"
                    label={t('user.userName')}
                    required
                    readOnly={!!id}
                />
                <InputField<UserFormModel> name="email" label={t('user.email')} required />
                <InputField<UserFormModel>
                    name="phoneNumber"
                    label={t('user.phone')}
                    required
                    type="tel"
                />
                <InputField<UserFormModel> name="fullName" label={t('user.fullName')} />
                <DatePickerField<UserFormModel> name="dateOfBirth" label={t('user.dateOfBirth')} />
                <SelectField<UserFormModel>
                    name="gender"
                    label={t('user.gender')}
                    options={GenderOptions}
                />
                {!id && (
                    <>
                        <InputField<UserFormModel>
                            name="password"
                            label={t('user.password')}
                            type="password"
                            required
                        />
                        <InputField<UserFormModel>
                            name="confirmPassword"
                            label={t('user.confirmPassword')}
                            type="password"
                            required
                        />
                    </>
                )}
                <SwitchField<UserFormModel> name={'status'} label={t('user.status')} />
            </BaseForm>
        </DrawerContainer>
    );
};

export default UserForm;
