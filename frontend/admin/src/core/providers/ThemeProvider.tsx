import { darkTheme } from '@/themes/darkTheme';
import { lightTheme } from '@/themes/lightTheme';
import { ThemeConfig } from 'antd';
import React, { createContext, useContext, useEffect, useMemo, useState } from 'react';

export type Theme = 'dark' | 'light';

type ThemeProviderProps = {
    children: React.ReactNode;
    defaultTheme?: Theme;
    storageKey?: string;
};

type ThemeProviderState = {
    theme: Theme;
    setTheme: (theme: Theme) => void;
    themeTokens: ThemeConfig;
};

const initialState: ThemeProviderState = {
    theme: 'light',
    setTheme: () => null,
    themeTokens: lightTheme
};

const ThemeProviderContext = createContext<ThemeProviderState>(initialState);

export function ThemeProvider({
    children,
    defaultTheme = 'light',
    storageKey = 'theme',
    ...props
}: ThemeProviderProps) {
    const [theme, setTheme] = useState<Theme>(
        () => (localStorage.getItem(storageKey) as Theme) || defaultTheme
    );
    console.log('theme:', theme);

    const themeTokens = theme === 'dark' ? darkTheme : lightTheme;

    useEffect(() => {
        const root = window.document.documentElement;
        root.classList.remove('light', 'dark');

        root.classList.add(theme);
    }, [theme]);

    const value = useMemo(
        () => ({
            theme,
            setTheme: (newTheme: Theme) => {
                localStorage.setItem(storageKey, newTheme);
                setTheme(newTheme);
            },
            themeTokens
        }),
        [theme, themeTokens]
    );

    return (
        <ThemeProviderContext.Provider {...props} value={value}>
            {children}
        </ThemeProviderContext.Provider>
    );
}

export const useTheme = () => {
    const context = useContext(ThemeProviderContext);

    if (context === undefined) throw new Error('useTheme must be used within a ThemeProvider');

    return context;
};
