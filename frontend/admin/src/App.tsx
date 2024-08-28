import '@/App.scss';
import { useTheme } from '@/core/providers/ThemeProvider';
import AppRoutes from '@/routes';
import useLocaleStore from '@/stores/localeStore';
import queryClient from '@/utils/queryClient';
import { QueryClientProvider } from '@tanstack/react-query';
import { ConfigProvider } from 'antd';
import React from 'react';
import { BrowserRouter } from 'react-router-dom';

const App: React.FC = () => {
    const { locale } = useLocaleStore();
    const { themeTokens } = useTheme();

    return (
        <BrowserRouter>
            <ConfigProvider locale={locale} theme={themeTokens}>
                <QueryClientProvider client={queryClient}>
                    <AppRoutes />
                </QueryClientProvider>
            </ConfigProvider>
        </BrowserRouter>
    );
};

export default App;
