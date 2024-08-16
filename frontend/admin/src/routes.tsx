import React, { lazy, Suspense } from 'react';
import { Route, Routes } from 'react-router-dom';

const LoginPage = lazy(() => import('@/core/components/authentications/LoginPage'));
const HomePage = lazy(() => import('./pages/home/HomePage'));

const AppRoutes: React.FC = () => {
  return (
    <Suspense fallback={<div>Loading...</div>}>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/login" element={<LoginPage />} />
      </Routes>
    </Suspense>
  );
};

export default AppRoutes;
