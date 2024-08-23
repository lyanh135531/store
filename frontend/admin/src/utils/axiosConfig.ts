import axios from 'axios';

const axiosInstance = axios.create({
    baseURL: import.meta.env.VITE_BASE_URL,
    timeout: 10000,
    headers: {
        'Content-Type': 'application/json'
    }
});

axiosInstance.defaults.withCredentials = true;

const handleRedirectLogin = () => {
    localStorage.removeItem('user');
};

axiosInstance.interceptors.response.use(
    (response) => response,
    (error) => {
        if (error.response && error.response.status === 401) {
            handleRedirectLogin();
        } else if (error.request) {
            handleRedirectLogin();
        }
        return Promise.reject(error);
    }
);

export default axiosInstance;
