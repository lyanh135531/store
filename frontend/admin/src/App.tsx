import { QueryClientProvider } from '@tanstack/react-query';
import React from 'react';
import { BrowserRouter } from 'react-router-dom';
import './App.css';
import { ThemeProvider } from './core/providers/ThemeProvider';
import AppRoutes from './routes';
import queryClient from './utils/queryClient';

const App: React.FC = () => {
  return (
    <BrowserRouter>
      <ThemeProvider>
        <QueryClientProvider client={queryClient}>
          <AppRoutes />
        </QueryClientProvider>
      </ThemeProvider>
    </BrowserRouter>
  );
};

export default App;
