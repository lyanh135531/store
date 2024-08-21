import React, { lazy, Suspense } from 'react';
import { Navigate, Route, Routes } from 'react-router-dom';
import AuthProvider from './core/providers/AuthProvider';
import { Result } from 'antd';

const LoginPage = lazy(() => import('@/core/components/authentications/LoginPage'));
const HomePage = lazy(() => import('./pages/home/HomePage'));
const BaseLayout = lazy(() => import('@/core/layouts/BaseLayout'));

const AppRoutes: React.FC = () => {
  return (
    <Suspense fallback={<div>Loading...</div>}>
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
          <Route path="/setting" element={<HomePage />} />
          <Route path="*" element={<Result />} />
        </Route>
      </Routes>
    </Suspense>
  );
};

export default AppRoutes;
