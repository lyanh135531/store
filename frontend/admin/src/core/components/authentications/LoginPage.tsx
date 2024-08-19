import { Icons } from '@/core/icons/icon';
import { useLogin } from '@/hooks/authQuery';
import { useForm } from 'antd/es/form/Form';
import React from 'react';
import { Link } from 'react-router-dom';
import BaseForm from '../forms/BaseForm';
import BaseFormButton from '../forms/BaseFormButton';
import CheckboxField from '../forms/CheckBoxField';
import InputField from '../forms/InputField';

export interface LoginFormModel {
  userName: string;
  password: string;
  rememberMe: boolean;
}

const LoginPage: React.FC = () => {
  const { mutate: login, isPending } = useLogin();
  const [form] = useForm<LoginFormModel>();

  const onFinish = async (values: LoginFormModel) => {
    await form.validateFields();
    login(values);
  };

  return (
    <div className="container h-full">
      <BaseForm<LoginFormModel>
        form={form}
        initialValues={
          {
            rememberMe: true
          } as LoginFormModel
        }
        onFinish={onFinish}
        className="max-w-96 m-auto content-center">
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
  );
};

export default LoginPage;
