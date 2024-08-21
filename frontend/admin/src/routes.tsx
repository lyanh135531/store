import React, { lazy, Suspense } from 'react';
import { Navigate, Route, Routes } from 'react-router-dom';
import Loading from './core/components/common/Loading';
import NotFound from './core/components/common/NotFound';
import AuthProvider from './core/providers/AuthProvider';

const LoginPage = lazy(() => import('@/core/components/authentications/LoginPage'));
const HomePage = lazy(() => import('@/pages/home/HomePage'));
const UserPage = lazy(() => import('@/pages/user/UserPage'));
const BaseLayout = lazy(() => import('@/core/layouts/BaseLayout'));

const AppRoutes: React.FC = () => {
  return (
    <Suspense fallback={<Loading fullScreen />}>
      <Routes>
        <Route path="/login" element={<LoginPage />} />
        <Route
          path="/"
          element={
            <AuthProvider>
              <BaseLayout />
            </AuthProvider>
          }>
          <Route index element={<Navigate to="/home" replace />} />
          <Route path="home" element={<HomePage />} />
          <Route path="/product" element={<HomePage />} />
          <Route path="/category" element={<HomePage />} />
          <Route path="/user" element={<UserPage />} />
          <Route path="/setting" element={<HomePage />} />
        </Route>
        <Route path="*" element={<NotFound />} />
      </Routes>
    </Suspense>
  );
};

export default AppRoutes;
