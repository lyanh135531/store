import Loading from '@/core/components/common/Loading';
import BaseForm from '@/core/components/forms/BaseForm';
import BaseFormButton from '@/core/components/forms/BaseFormButton';
import CheckboxField from '@/core/components/forms/CheckBoxField';
import InputField from '@/core/components/forms/InputField';
import { NOTIFY_TITLE } from '@/core/constants/notify';
import { Icons } from '@/core/icons/icon';
import { useLogin } from '@/hooks/authQuery';
import useAuthStore from '@/stores/authStore';
import NotifyUtil from '@/utils/notifyUtil';
import { useForm } from 'antd/es/form/Form';
import React, { useEffect } from 'react';
import { Link, useNavigate } from 'react-router-dom';

export interface LoginFormModel {
    userName: string;
    password: string;
    rememberMe: boolean;
}

const LoginPage: React.FC = () => {
    const { setUser } = useAuthStore();
    const navigate = useNavigate();
    const { mutate: login, isPending, error, isError } = useLogin();
    const [form] = useForm<LoginFormModel>();

    useEffect(() => {
        const storedUser = localStorage.getItem('user');
        if (storedUser) {
            setUser(JSON.parse(storedUser));
            navigate('/');
        }
    }, []);

    const onFinish = async (values: LoginFormModel) => {
        await form.validateFields();
        login(values);
    };

    if (isError) {
        NotifyUtil.error(NOTIFY_TITLE, error?.message);
    }

    if (isPending) return <Loading />;

    return (
        <div className="container h-full content-center">
            <div className="max-w-lg m-auto">
                <BaseForm<LoginFormModel>
                    form={form}
                    initialValues={
                        {
                            rememberMe: true
                        } as LoginFormModel
                    }
                    onFinish={onFinish}
                    className="m-auto content-center p-16 shadow">
                    <InputField<LoginFormModel>
                        name="userName"
                        placeholder="Username"
                        rules={[{ required: true, message: 'Please input your Username!' }]}
                        icon={<Icons.User />}
                    />
                    <InputField<LoginFormModel>
                        name="password"
                        placeholder="Password"
                        rules={[{ required: true, message: 'Please input your Password!' }]}
                        type="password"
                        icon={<Icons.Lock />}
                    />
                    <div className="flex justify-between mb-6">
                        <CheckboxField<LoginFormModel> name={'rememberMe'} noStyle>
                            Remember me
                        </CheckboxField>
                        <Link to={'/forgot-password'}>Forgot Password</Link>
                    </div>
                    <BaseFormButton
                        disabled={isPending}
                        configs={{
                            confirm: true,
                            confirmText: 'Login',
                            classNameConfirm: 'w-full'
                        }}
                    />
                </BaseForm>
            </div>
        </div>
    );
};

export default LoginPage;
