import '@/App.scss';
import { ThemeProvider } from '@/core/providers/ThemeProvider';
import AppRoutes from '@/routes';
import useLocaleStore from '@/stores/localeStore';
import queryClient from '@/utils/queryClient';
import { QueryClientProvider } from '@tanstack/react-query';
import { ConfigProvider } from 'antd';
import React from 'react';
import { BrowserRouter } from 'react-router-dom';

const App: React.FC = () => {
    const { locale } = useLocaleStore();

    return (
        <BrowserRouter>
            <ThemeProvider>
                <ConfigProvider locale={locale}>
                    <QueryClientProvider client={queryClient}>
                        <AppRoutes />
                    </QueryClientProvider>
                </ConfigProvider>
            </ThemeProvider>
        </BrowserRouter>
    );
};

export default App;
