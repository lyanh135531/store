import { useMutation } from '@tanstack/react-query';
import { API_LOGIN, API_LOGOUT } from '~/core/apis';
import useAuthStore from '~/stores/authStore';
import { AuthUser } from '~/types/auth';
import { ApiUtil } from '~/utils/apiUtil';

export const useLogin = () => {
  return useMutation<AuthUser, Error, { userName: string; password: string }>({
    mutationFn: async ({ userName, password }) => {
      const response = await ApiUtil.Axios<AuthUser>('post', API_LOGIN, null, {
        userName,
        password
      });
      return response.data;
    },
    onSuccess: (data) => {
      useAuthStore.getState().setUser(data);
    }
  });
};

export const useLogout = () => {
  return useMutation({
    mutationFn: async () => await ApiUtil.Axios('post', API_LOGOUT),
    onSuccess: () => {
      useAuthStore.getState().setUser(null);
    }
  });
};
