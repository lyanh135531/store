import useAuthStore from '@/stores/authStore';
import { ApiResponse, AuthUser } from '@/types/auth';
import { ApiUtil } from '@/utils/apiUtil';
import React, { useEffect } from 'react';
import { API_CHECK_LOGIN } from '../apis';

interface AuthProviderProps {
  children: React.ReactNode;
}

const AuthProvider: React.FC<AuthProviderProps> = ({ children }) => {
  const { setUser } = useAuthStore();

  useEffect(() => {
    const checkAuth = async () => {
      const response = await ApiUtil.Axios<ApiResponse<AuthUser>>('get', API_CHECK_LOGIN);
      if (response.data?.success) {
        setUser(response?.data?.result);
      } else {
        setUser(null);
      }
    };
    checkAuth();
  }, []);

  return <>{children}</>;
};

export default AuthProvider;
