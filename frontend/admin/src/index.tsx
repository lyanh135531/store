import App from '@/App';
import { ThemeProvider } from '@/core/providers/ThemeProvider';
import '@/index.scss';
import reportWebVitals from '@/reportWebVitals';
import React from 'react';
import ReactDOM from 'react-dom/client';
import './i18n';

const root = ReactDOM.createRoot(document.getElementById('root') as HTMLElement);
root.render(
    <React.Fragment>
        <ThemeProvider>
            <App />
        </ThemeProvider>
    </React.Fragment>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
