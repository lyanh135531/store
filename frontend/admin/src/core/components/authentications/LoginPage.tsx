import Loading from '@/core/components/common/Loading';
import BaseForm from '@/core/components/forms/BaseForm';
import BaseFormButton from '@/core/components/forms/BaseFormButton';
import CheckboxField from '@/core/components/forms/CheckBoxField';
import InputField from '@/core/components/forms/InputField';
import { Icons } from '@/core/icons';
import { useLogin } from '@/hooks/authQuery';
import useAuthStore from '@/stores/authStore';
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
    const { mutate: login, isPending } = useLogin();
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
                    className="m-auto content-center p-16 shadow"
                >
                    <InputField<LoginFormModel>
                        name="userName"
                        placeholder="Username"
                        required
                        icon={<Icons.User />}
                        autoComplete="username"
                    />
                    <InputField<LoginFormModel>
                        name="password"
                        placeholder="Password"
                        required
                        type="password"
                        icon={<Icons.Lock />}
                        autoComplete="current-password"
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
