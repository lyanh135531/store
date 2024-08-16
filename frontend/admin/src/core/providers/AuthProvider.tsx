import useAuthStore from '@/stores/authStore';
import React, { useEffect } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';

interface AuthProviderProps {
  children: React.ReactNode;
}

const AuthProvider: React.FC<AuthProviderProps> = ({ children }) => {
  const { isAuthenticated } = useAuthStore();
  const location = useLocation();
  const navigate = useNavigate();

  useEffect(() => {
    if (!isAuthenticated && location.pathname !== '/login') {
      navigate('/login', { state: { from: location } });
    }
  }, [isAuthenticated, location.pathname, navigate]);

  return <>{children}</>;
};

export default AuthProvider;
