import { API_CHECK_LOGIN } from '@/core/apis';
import Loading from '@/core/components/common/Loading';
import useAuthStore from '@/stores/authStore';
import { ApiResponse, AuthUser } from '@/types/auth';
import { ApiUtil } from '@/utils/apiUtil';
import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';

interface AuthProviderProps {
    children: React.ReactNode;
}

const AuthProvider: React.FC<AuthProviderProps> = ({ children }) => {
    const { setUser } = useAuthStore();
    const navigate = useNavigate();

    const [isLoading, setIsLoading] = useState<boolean>(true);

    useEffect(() => {
        const checkAuth = async () => {
            const response = await ApiUtil.Axios<ApiResponse<AuthUser>>('get', API_CHECK_LOGIN);
            if (response.data?.success) {
                const userInfo = response?.data?.result;
                setUser(userInfo);
                localStorage.setItem('user', JSON.stringify(userInfo));
            } else {
                navigate('/login');
                setUser(null);
            }
            setIsLoading(false);
        };
        checkAuth();
    }, []);

    if (isLoading) return <Loading fullScreen />;

    return <>{children}</>;
};

export default AuthProvider;
