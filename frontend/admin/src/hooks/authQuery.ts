import { API_LOGIN, API_LOGOUT } from '@/core/apis';
import { LoginFormModel } from '@/core/components/authentications/LoginPage';
import useAuthStore from '@/stores/authStore';
import { AuthUser } from '@/types/auth';
import { ApiUtil } from '@/utils/apiUtil';
import axiosInstance from '@/utils/axiosConfig';
import { useMutation } from '@tanstack/react-query';
import { useNavigate } from 'react-router-dom';

export const useLogin = () => {
  const navigate = useNavigate();
  const { setUser } = useAuthStore();

  const mutation = useMutation<AuthUser, Error, LoginFormModel>({
    mutationFn: async (data) => await axiosInstance.post(API_LOGIN, data),
    onSuccess: (data: AuthUser) => {
      setUser(data);
      navigate('/');
    }
  });

  return mutation;
};

export const useLogout = () => {
  const navigate = useNavigate();
  const { setUser } = useAuthStore();

  return useMutation({
    mutationFn: async () => await ApiUtil.Axios('post', API_LOGOUT),
    onSuccess: () => {
      setUser(null);
      localStorage.removeItem('user');
      navigate('/login');
    }
  });
};
